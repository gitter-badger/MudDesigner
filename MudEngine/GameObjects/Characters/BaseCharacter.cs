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
using MudEngine.GameObjects.Items;

using System.Net;
using System.Net.Sockets;

namespace MudEngine.GameObjects.Characters
{
    public class BaseCharacter : BaseObject
    {
        /// <summary>
        /// The current Room this Character is located at.
        /// </summary>
        public Room CurrentRoom { get; set; }

        /// <summary>
        /// Gets or Sets if this Character is controlled by the user. If not user controlled then it will be AI controlled.
        /// </summary>
        public Boolean IsControlled { get; set; }

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
        /// Gets a working copy of the CommandEngine used by the player.
        /// </summary>
        public CommandEngine CommandSystem { get; internal set; }

        public BaseCharacter(Game game) : base(game)
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

        public override void Load(string filename)
        {
            base.Load(filename);

            this.IsControlled = Convert.ToBoolean(FileManager.GetData(filename, "IsControlled"));

            //Need to re-assign the enumerator value that was previously assigned to the Role property
            Array values = Enum.GetValues(typeof(SecurityRoles));
            foreach (int value in values)
            {
                //Since enum values are not strings, we can't simply just assign the string to the enum
                string displayName = Enum.GetName(typeof(SecurityRoles), value);

                //If the value = the string saved, then perform the needed conversion to get our data back
                if (displayName.ToLower() == FileManager.GetData(filename, "Role").ToLower())
                {
                    Role = (SecurityRoles)Enum.Parse(typeof(SecurityRoles), displayName);
                    break;
                }
            }

            //Restore the users current Room.
            Realm realm= ActiveGame.GetRealm(FileManager.GetData(filename, "CurrentRealm"));
            if (realm == null)
            {
                realm = new Realm(ActiveGame);
                return;
            }

            Zone zone = realm.GetZone(FileManager.GetData(filename, "CurrentZone"));
            if (zone == null)
            {
                zone = new Zone(ActiveGame);
                return;
            }

            CurrentRoom = zone.GetRoom(FileManager.GetData(filename, "CurrentRoom"));
            if (CurrentRoom == null)
            {
                CurrentRoom = new Room(ActiveGame);
                CurrentRoom.Name = "Abyss";
                CurrentRoom.Description = "You are in the Aybss. It is void of all life.";
                return;
            }
        }

        public override void Save(string filename)
        {
            //Don't attempt to save a blank filename.
            if (String.IsNullOrEmpty(filename))
                return;

            base.Save(filename);

            FileManager.WriteLine(filename, this.IsControlled.ToString(), "IsControlled");
            FileManager.WriteLine(filename, this.Role.ToString(), "Role");
            FileManager.WriteLine(filename, this.CurrentRoom.Name, "CurrentRoom");
            FileManager.WriteLine(filename, this.CurrentRoom.Zone, "CurrentZone");
            FileManager.WriteLine(filename, this.CurrentRoom.Realm, "CurrentRealm");
        }

        /// <summary>
        /// Moves the player from one Room to another if the supplied direction contains a doorway.
        /// Returns false if no doorway is available.
        /// </summary>
        /// <param name="travelDirection"></param>
        /// <returns></returns>
        public bool Move(AvailableTravelDirections travelDirection)
        {
            //Check if the current room has a doorway in the supplied direction of travel.
            if (!CurrentRoom.DoorwayExist(travelDirection))
            {
                return false;
            }

            //Let other players know that the user walked out.
            for (int i = 0; i != ActiveGame.PlayerCollection.Length; i++)
            {
                if (ActiveGame.PlayerCollection[i].Name == Name)
                    continue;

                string room = ActiveGame.PlayerCollection[i].CurrentRoom.Name;
                string realm = ActiveGame.PlayerCollection[i].CurrentRoom.Realm;
                string zone = ActiveGame.PlayerCollection[i].CurrentRoom.Zone;

                if ((room == CurrentRoom.Name) && (realm == CurrentRoom.Realm) && (zone == CurrentRoom.Zone))
                {
                    ActiveGame.PlayerCollection[i].Send(Name + " walked out towards the " + travelDirection.ToString());
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
            for (int i = 0; i != ActiveGame.PlayerCollection.Length; i++)
            {
                if (ActiveGame.PlayerCollection[i].Name == Name)
                    continue;

                string room = ActiveGame.PlayerCollection[i].CurrentRoom.Name;
                string realm = ActiveGame.PlayerCollection[i].CurrentRoom.Realm;
                string zone = ActiveGame.PlayerCollection[i].CurrentRoom.Zone;

                if ((room == CurrentRoom.Name) && (realm == CurrentRoom.Realm) && (zone == CurrentRoom.Zone))
                {
                    ActiveGame.PlayerCollection[i].Send(Name + " walked in from the " + TravelDirections.GetReverseDirection(travelDirection));
                }
            }
        }

        public void ExecuteCommand(string command)
        {
            //TODO: Character class can handle a lot of the command management here, checking various things prior to sending
            //the command off to the command engine for execution.
            CommandSystem.ExecuteCommand(command, this);
            
            Send(""); //Blank line to help readability.

            //Now that the command has been executed, restore the Command: message
            Send("Command: ", false);

            /* No longer needed due to player.send() sending content to the player.
            if (result.Result != null)
            {
                StringBuilder sb = new StringBuilder();
                foreach (object item in result.Result)
                {
                    if (item is string)
                        sb.AppendLine(item.ToString());
                }
                return sb.ToString();
            }
             */
        }

        internal void Initialize()
        {
            if (ActiveGame.IsMultiplayer)
                client.Receive(new byte[255]);

            if (Game.IsDebug)
                Log.Write("New Player Connected.");
            
            Log.Write("Loading internal game commands...");
            //Loads the MudEngine Game Commands
            //CommandSystem.LoadBaseCommands();

            //Ensure custom commands are loaded until everything is fleshed out.
            if (Game.IsDebug)
            {
                foreach (string command in CommandEngine.GetCommands())
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

            ExecuteCommand("Login");
            ExecuteCommand("Look"); //MUST happen after Room setup is completed, otherwise the player default Abyss Room is printed.
        }
        internal void Receive(string data)
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
        internal void Send(string data, bool newLine)
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
        internal void Send(string data)
        {
            Send(data, true);
        }

        internal void Disconnect()
        {
            if (IsActive)
            {
                string filePath =  Path.Combine(ActiveGame.DataPaths.Players, Filename);
                this.Save(filePath);

                Send("Goodbye!");
                IsActive = false;
                client.Close();

                Log.Write("Player " + this.Name + " disconnected.");
            }
        }
        internal string ReadInput()
        {
            if (ActiveGame.IsMultiplayer)
            {
                List<byte> buffer = new List<byte>();
                while (true)
                {
                    try
                    {
                        byte[] buf = new byte[1];
                        int recved = client.Receive(buf);

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
