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
            private String Password { get; set; }
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
                Realm realm = (Realm)ActiveGame.World.GetObject(ObjectTypes.Realm, FileManager.GetData(filename, "CurrentRealm"));

                if (realm == null)
                {
                    realm = new Realm(ActiveGame);
                    return;
                }

                List<Zone> zones = realm.GetZoneByFilename(FileManager.GetData(filename, "CurrentZone"));
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
                }

                List<Room> rooms = zone.GetRoomByFilename(FileManager.GetData(filename, "CurrentRoom"));

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

            public override void Save(String path)
            {
                base.Save(path);

                path = Path.Combine(path, Filename);

                FileManager.WriteLine(path, this.Password, "Password");
                FileManager.WriteLine(path, this.IsControlled.ToString(), "IsControlled");
                FileManager.WriteLine(path, this.Role.ToString(), "Role");
                FileManager.WriteLine(path, this.CurrentRoom.Filename, "CurrentRoom");
                FileManager.WriteLine(path, this.CurrentRoom.Zone, "CurrentZone");
                FileManager.WriteLine(path, this.CurrentRoom.Realm, "CurrentRealm");
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
                for (Int32 i = 0; i != ActiveGame.PlayerCollection.Length; i++)
                {
                    if (ActiveGame.PlayerCollection[i].Name == Name)
                        continue;

                    String room = ActiveGame.PlayerCollection[i].CurrentRoom.Filename;
                    String realm = ActiveGame.PlayerCollection[i].CurrentRoom.Realm;
                    String zone = ActiveGame.PlayerCollection[i].CurrentRoom.Zone;

                    if ((room == CurrentRoom.Filename) && (realm == CurrentRoom.Realm) && (zone == CurrentRoom.Zone))
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
                for (Int32 i = 0; i != ActiveGame.PlayerCollection.Length; i++)
                {
                    if (ActiveGame.PlayerCollection[i].Name == Name)
                        continue;

                    String room = ActiveGame.PlayerCollection[i].CurrentRoom.Name;
                    String realm = ActiveGame.PlayerCollection[i].CurrentRoom.Realm;
                    String zone = ActiveGame.PlayerCollection[i].CurrentRoom.Zone;

                    if ((room == CurrentRoom.Name) && (realm == CurrentRoom.Realm) && (zone == CurrentRoom.Zone))
                    {
                        ActiveGame.PlayerCollection[i].Send(Name + " walked in from the " + TravelDirections.GetReverseDirection(travelDirection));
                    }
                }
            }

            public void ExecuteCommand(String command)
            {
                CommandSystem.ExecuteCommand(command, this);
            
                Send(""); //Blank line to help readability.

                //Now that the command has been executed, restore the Command: message
                Send("Command: ", false);
            }

            public void Create(string playerName, string password)
            {
                Name = playerName;
                Password = password;

                Save(ActiveGame.DataPaths.Players);
            }

            public bool VarifyPassword(string password)
            {
                if (Password == password)
                    return true;
                else
                    return false;
            }

            internal void Initialize()
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

                ExecuteCommand("Login");
                Log.Write(Name + " has logged in.");
                ExecuteCommand("Look"); //MUST happen after Room setup is completed, otherwise the player default Abyss Room is printed.
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
            internal void Send(String data, Boolean newLine)
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
            internal void Send(String data)
            {
                Send(data, true);
            }

            internal void FlushConsole()
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

            internal void Disconnect()
            {
                if (IsActive)
                {
                    this.Save(ActiveGame.DataPaths.Players);

                    Send("Goodbye!");
                    IsActive = false;
                    client.Close();

                    Log.Write(Name + " disconnected.");
                }
            }
            internal String ReadInput()
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
