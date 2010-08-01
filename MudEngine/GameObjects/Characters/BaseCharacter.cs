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
        public Boolean IsActive { get; private set; }

        /// <summary>
        /// Gets the current inventory of the character
        /// </summary>
        public Bag Inventory { get; private set; }

        public BaseCharacter(Game game) : base(game)
        {
            ActiveGame = game;
            IsActive = false;
            CurrentRoom = game.InitialRealm.InitialZone.InitialRoom;
            Inventory = new Bag(game);

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
            Realm realm = ActiveGame.GetRealm(FileManager.GetData(filename, "CurrentRealm"));
            Zone zone = realm.GetZone(FileManager.GetData(filename, "CurrentZone"));
            CurrentRoom = zone.GetRoom(FileManager.GetData(filename, "CurrentRoom"));
        }

        public override void Save(string filename)
        {
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

            //We have a doorway, lets move to the next room.
            CurrentRoom = CurrentRoom.GetDoor(travelDirection).ArrivalRoom;

            OnTravel(travelDirection);

            return true;
        }

        public virtual void OnTravel(AvailableTravelDirections travelDirection)
        {
            //TODO: Check the Room/Zone/Realm to see if anything needs to occure during travel.
        }

        public String ExecuteCommand(string command)
        {
            //TODO: Character class can handle a lot of the command management here, checking various things prior to sending
            //the command off to the command engine for execution.
            CommandResults result = CommandEngine.ExecuteCommand(command, this);

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
            return "";
        }

        internal void Initialize()
        {
            string result = ExecuteCommand("Login");


            CurrentRoom = ActiveGame.InitialRealm.InitialZone.InitialRoom;
            IsActive = true;
        }
        internal void Receive(string data)
        {
            ExecuteCommand(data);
            Send(data);
            if (!ActiveGame.IsRunning)
                Disconnect();
        }

        internal void Send(string data)
        {
            try
            {
                System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
                client.Send(encoding.GetBytes(data));
            }
            catch (Exception) // error, connection failed: close client
            {
                Disconnect();
            }
        }
        internal void Disconnect()
        {
            string filePath = Path.Combine(ActiveGame.DataPaths.Players, Filename);
            this.Save(filePath);

            IsActive = false;
            client.Close();
            // TODO: Reset game so it can be used again
        }
        internal string ReadInput()
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
                            if (buffer[buffer.Count-1] == '\r')
                                buffer.RemoveAt(buffer.Count-1);

                            String str;
                            System.Text.UTF8Encoding enc = new System.Text.UTF8Encoding();
                            str = enc.GetString(buffer.ToArray());
                            return str;
                        }
                        else
                            buffer.Add(buf[0]);
                    }
                }
                catch (Exception) // error receiving, close player
                {
                    Disconnect();
                }
            }
        }

        internal Socket client;
    }
}
