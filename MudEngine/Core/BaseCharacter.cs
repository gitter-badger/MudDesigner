//Microsoft .NET Framework
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

//MUD Engine
using MudEngine.FileSystem;
using MudEngine.Commands;
using MudEngine.GameManagement;
using MudEngine.GameObjects;
using MudEngine.GameObjects.Environment;
using MudEngine.Items;
using MudEngine.Core;

using System.Net;
using System.Net.Sockets;

namespace MudEngine.GameObjects.Characters
{
    public class BaseCharacter : BaseObject
    {
        /// <summary>
        /// Password for the player's account.
        /// </summary>
        private String Password { get; set; }

        /// <summary>
        /// The last time that the AI (if IsControlled=false) performed any major changes like traversing Rooms etc.
        /// </summary>
        private GameTime.Time _LastUpdate;

        /// <summary>
        /// Gets or Sets if the AI (if IsControlled=false) cannot move from it's CurrentRoom.
        /// This is used by Shop keeper type NPC's
        /// </summary>
        public Boolean IsStatic { get; set; }

        /// <summary>
        /// The current Room this Character is located at.
        /// </summary>
        public Room CurrentRoom { get; set; }

        /// <summary>
        /// Gets the complete path to the Characters current location, including Realm, Zone and Room.
        /// </summary>
        public String CurrentWorldLocation
        {
            get
            {
                return CurrentRoom.Realm + "." + CurrentRoom.Zone + "." + CurrentRoom.Filename;
            }
        }

        protected override string SavePath
        {
            get
            {
                return ActiveGame.DataPaths.Players;
            }
        }
        /// <summary>
        /// Gets or Sets if this Character is controlled by the user. If not user controlled then it will be AI controlled.
        /// </summary>
        public Boolean IsControlled
        {
            get
            {
                return _IsControlled;
            }
            set
            {
                if (value)
                {
                    //TODO: Begin AI initialization
                }
                _IsControlled = value;
            }
        }
        private Boolean _IsControlled;

        /// <summary>
        /// Gets if this user has Admin privileges or not.
        /// </summary>
        public SecurityRoles Role { get; private set; }

        /// <summary>
        /// Gets or if this character is active.
        /// </summary>
        public Boolean IsActive { get; internal set; }

        /// <summary>
        /// Gets the current inventory of the character
        /// </summary>
        public Bag Inventory { get; private set; }

        /// <summary>
        /// Gets the characters Dialog Branch.
        /// If the Character contains Quests, then the Dialog will only be visible when the Quests are not available 
        /// to the users.
        /// </summary>
        public DialogChat Dialog { get; internal set; }

        /// <summary>
        /// Gets a working copy of the CommandEngine used by the player.
        /// </summary>
        public CommandEngine CommandSystem { get; internal set; }

        /// <summary>
        /// Gets or Sets the characters stats.
        /// Note that this will more and likely become Read-Only in the future.
        /// </summary>
        public BaseStats Stats { get; set; }

        public BaseCharacter(Game game)
            : base(game)
        {
            ActiveGame = game;
            IsActive = false;

            if ((game.InitialRealm == null) || (game.InitialRealm.InitialZone == null) || (game.InitialRealm.InitialZone.InitialRoom == null))
            {
                CurrentRoom = new Room(game);
                CurrentRoom.Name = "Abyss";
                CurrentRoom.Description = "You are currently in the abyss.";
            }
            else
                CurrentRoom = game.InitialRealm.InitialZone.InitialRoom;
            Inventory = new Bag(game);
            CommandSystem = new CommandEngine();
        }

        public override void Load(String filename)
        {
            base.Load(filename);

            Password = FileManager.GetData(filename, "Password");
            this.IsControlled = Convert.ToBoolean(FileManager.GetData(filename, "IsControlled"));

            //Need to re-assign the enumerator value that was previously assigned to the Role property
            Array values = Enum.GetValues(typeof(SecurityRoles));
            foreach (Int32 value in values)
            {
                //Since enum values are not strings, we can't simply just assign the String to the enum
                String displayName = Enum.GetName(typeof(SecurityRoles), value);

                //If the value = the String saved, then perform the needed conversion to get our data back
                if (displayName.ToLower() == FileManager.GetData(filename, "Role").ToLower())
                {
                    Role = (SecurityRoles)Enum.Parse(typeof(SecurityRoles), displayName);
                    break;
                }
            }

            //Restore the users current Room.
            Realm realm = (Realm)ActiveGame.World.GetRealm(FileManager.GetData(filename, "CurrentRealm"));

            if (realm == null)
            {
                realm = new Realm(ActiveGame);
                return;
            }

            List<Zone> zones = realm.GetZone(FileManager.GetData(filename, "CurrentZone"));
            Zone zone = new Zone(ActiveGame);
            if (zones.Count == 0)
            {
                Log.Write("Error: Failed to find " + FileManager.GetData(filename, "CurrentZone") + " zone.");
                return;
            }
            else if (zones.Count == 1)
            {
                zone = zones[0];
            }
            else
            {
                //TODO: determin which zone is the appropriate zone to assign if more than one exists.
                foreach (Zone z in zones)
                {
                    if (z.GetRoom(FileManager.GetData(filename, "CurrentRoom")) != null)
                    {
                        zone = z;
                        break;
                    }
                }
            }

            List<Room> rooms = zone.GetRoom(FileManager.GetData(filename, "CurrentRoom"));

            if (rooms.Count == 0)
            {
                Log.Write("Error: Failed to find " + FileManager.GetData(filename, "CurrentRoom") + " room.");
                return;
            }
            else if (rooms.Count == 1)
            {
                CurrentRoom = rooms[0];
            }
            else
            {
                //TODO: Loop through each found room and determin in some manor what should be the correct Room to assign.
            }

            if (CurrentRoom == null)
            {
                CurrentRoom = new Room(ActiveGame);
                CurrentRoom.Name = "Abyss";
                CurrentRoom.Description = "You are in the Aybss. It is void of all life.";
                return;
            }

            //TODO: Load player Inventory.
            /* Due to private accessor Inv needs to be restored via
             * foreach (Item in Inventory)
             *      this.AddItem(Item);
             */
        }

        public override void Save()
        {
            base.Save();

            String path = Path.Combine(SavePath, Filename);

            FileManager.WriteLine(path, this.Password, "Password");
            FileManager.WriteLine(path, this.IsControlled.ToString(), "IsControlled");
            FileManager.WriteLine(path, this.Role.ToString(), "Role");
            FileManager.WriteLine(path, this.CurrentRoom.Filename, "CurrentRoom");
            FileManager.WriteLine(path, this.CurrentRoom.Zone, "CurrentZone");
            FileManager.WriteLine(path, this.CurrentRoom.Realm, "CurrentRealm");
        }

        public virtual void Update()
        {
            //TODO: Update AI logic.

            Log.Write("BaseCharacter.Update Called!", true);
            //TODO: AI Logic: Don't attack anything if CurrentRoom.IsSafe
            //TODO: Add Stat modifiers for Zone and Rooms.
        }

        /// <summary>
        /// Moves the player from one Room to another if the supplied direction contains a doorway.
        /// Returns false if no doorway is available.
        /// </summary>
        /// <param name="travelDirection"></param>
        /// <returns></returns>
        public Boolean Move(AvailableTravelDirections travelDirection)
        {
            //Check if the current room has a doorway in the supplied direction of travel.
            if (!CurrentRoom.DoorwayExist(travelDirection))
            {
                return false;
            }

            //Let other players know that the user walked out.
            for (Int32 i = 0; i != ActiveGame.GetPlayerCollection().Length; i++)
            {
                if (ActiveGame.GetPlayerCollection()[i].Name == Name)
                    continue;

                String room = ActiveGame.GetPlayerCollection()[i].CurrentRoom.Filename;
                String realm = ActiveGame.GetPlayerCollection()[i].CurrentRoom.Realm;
                String zone = ActiveGame.GetPlayerCollection()[i].CurrentRoom.Zone;

                if ((room == CurrentRoom.Filename) && (realm == CurrentRoom.Realm) && (zone == CurrentRoom.Zone))
                {
                    ActiveGame.GetPlayerCollection()[i].Send(Name + " walked out towards the " + travelDirection.ToString());
                }
            }

            //We have a doorway, lets move to the next room.
            CurrentRoom = CurrentRoom.GetDoor(travelDirection).ArrivalRoom;

            OnTravel(travelDirection);

            return true;
        }

        public virtual void OnTravel(AvailableTravelDirections travelDirection)
        {
            //TODO: Check the Room/Zone/Realm to see if anything needs to occure during travel.
            //Let other players know that the user walked in.
            for (Int32 i = 0; i != ActiveGame.GetPlayerCollection().Length; i++)
            {
                if (ActiveGame.GetPlayerCollection()[i].Name == Name)
                    continue;

                String room = ActiveGame.GetPlayerCollection()[i].CurrentRoom.Name;
                String realm = ActiveGame.GetPlayerCollection()[i].CurrentRoom.Realm;
                String zone = ActiveGame.GetPlayerCollection()[i].CurrentRoom.Zone;

                if ((room == CurrentRoom.Name) && (realm == CurrentRoom.Realm) && (zone == CurrentRoom.Zone))
                {
                    ActiveGame.GetPlayerCollection()[i].Send(Name + " walked in from the " + TravelDirections.GetReverseDirection(travelDirection));
                }
            }
        }

        public virtual void OnTalk(String message, BaseCharacter instigator)
        {
            //If the instigator is not sending a message to this character, then the
            //AI can ignore it. No response will be sent.
            if (!message.ToLower().Substring("say".Length).Trim().Contains(this.Name.ToLower()))
                return;

            //If the message was directed at this player, we will only respond
            //if this character is controlled by AI.
            if (!this.IsControlled)
            {
                //Instance a new Say command, and send the dialog response that we have saved.
                IGameCommand cmd = CommandEngine.GetCommand("Say");
                if (cmd != null)
                    cmd.Execute("say " + this.Dialog.Response, instigator);
            }
        }

        public void ExecuteCommand(String command)
        {
            CommandSystem.ExecuteCommand(command, this);

            Send(""); //Blank line to help readability.

            //Now that the command has been executed, restore the Command: message
            Send("Command: ", false);
        }

        public virtual void Create(string playerName, string password)
        {
            Name = playerName;
            Password = password;

            Save();
        }

        public bool VarifyPassword(string password)
        {
            if (Password == password)
                return true;
            else
                return false;
        }

        public virtual void Initialize()
        {
            if (ActiveGame.IsMultiplayer)
                client.Receive(new byte[255]);

            //Ensure custom commands are loaded until everything is fleshed out.
            if (Game.IsDebug)
            {
                foreach (String command in CommandEngine.GetCommands())
                {
                    Log.Write("Command loaded: " + command);
                }
            }

            //Set the players initial room
            if ((ActiveGame.InitialRealm == null) || (ActiveGame.InitialRealm.InitialZone == null) || (ActiveGame.InitialRealm.InitialZone.InitialRoom == null))
            {
                CurrentRoom = new Room(ActiveGame);
                CurrentRoom.Name = "Abyss";
                CurrentRoom.Description = "You are in the Abyss. It is dark and void of life.";
            }
            else
                CurrentRoom = ActiveGame.InitialRealm.InitialZone.InitialRoom;

            IGameCommand gc = CommandEngine.GetCommand("CommandLogin");
            gc.Execute("Login", this);
            Log.Write(Name + " has logged in.");
            gc = CommandEngine.GetCommand("CommandLook");
            gc.Execute("Look", this); //MUST happen after Room setup is completed, otherwise the player default Abyss Room is printed.
            this.Send("Command: ", false);
        }

        public virtual void OnCreate()
        {
        }

        public virtual void OnDestroy()
        {
        }

        public virtual void OnEquip()
        {
        }

        public virtual void OnUnequip()
        {
        }


        internal void Receive(String data)
        {
            //data = ExecuteCommand(data);
            ExecuteCommand(data);
            //Send(data); //Results no longer returned as Player.Send() is used by the commands now.
            if (!ActiveGame.IsRunning)
                Disconnect();
        }

        /// <summary>
        /// Sends data to the player.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="newLine"></param>
        public void Send(String data, Boolean newLine)
        {
            try
            {
                System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
                if (newLine)
                    data += "\n\r";

                if (ActiveGame.IsMultiplayer)
                    client.Send(encoding.GetBytes(data));
                else
                    Console.Write(data);
            }
            catch (Exception)
            {
                Disconnect();
            }
        }

        /// <summary>
        /// Sends data to the player.
        /// </summary>
        /// <param name="data"></param>
        public void Send(String data)
        {
            Send(data, true);
        }

        public void FlushConsole()
        {
            try
            {
                if (ActiveGame.IsMultiplayer)
                {
                    System.Text.ASCIIEncoding encoding = new ASCIIEncoding();

                    //\x1B is hex
                    //[2J is terminal sequences for clearing the screen.
                    client.Send(encoding.GetBytes("\x1B[2J"));
                    //[H is terminal sequence for sending the cursor back to its home position.
                    client.Send(encoding.GetBytes("\x1B[H"));
                }
                else
                    Console.Clear();
            }
            catch
            {
                Disconnect();
            }
        }

        public void Disconnect()
        {
            if (IsActive)
            {
                this.Save();

                Send("Goodbye!");
                IsActive = false;
                client.Close();

                Log.Write(Name + " disconnected.");
            }
        }

        public String ReadInput()
        {
            if (ActiveGame.IsMultiplayer)
            {
                List<byte> buffer = new List<byte>();
                while (true)
                {
                    try
                    {
                        byte[] buf = new byte[1];
                        Int32 recved = client.Receive(buf);

                        if (recved > 0)
                        {
                            if (buf[0] == '\n' && buffer.Count > 0)
                            {
                                if (buffer[buffer.Count - 1] == '\r')
                                    buffer.RemoveAt(buffer.Count - 1);

                                String str;
                                System.Text.UTF8Encoding enc = new System.Text.UTF8Encoding();
                                str = enc.GetString(buffer.ToArray());
                                return str;
                            }
                            else
                                buffer.Add(buf[0]);
                        }
                    }
                    catch (Exception e)
                    {
                        Disconnect();
                        return e.Message;
                    }
                }
            }
            else
            {
                return Console.ReadLine();
            }
        }

        internal Socket client;
    }
}
