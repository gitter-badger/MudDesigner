//Microsoft .NET Framework
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            CurrentRoom = ActiveGame.InitialRealm.InitialZone.InitialRoom;

            IsActive = true;
        }
        internal void Receive(byte[] data)
        {
            // convert that data to string
            String str;
            System.Text.UTF8Encoding enc = new System.Text.UTF8Encoding();
            str = enc.GetString(data);

            // execute, and get result
            str = ExecuteCommand(str);
            
            // convert the result back to bytes and send it back
            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
            Send(encoding.GetBytes(str));
            if (!ActiveGame.IsRunning)
                Disconnect();
        }

        internal void Send(byte[] data)
        {
            try
            {
                client.Send(data);
            }
            catch (Exception) // error, connection failed: close client
            {
                Disconnect();
            }
        }
        internal void Disconnect()
        {
            // TODO: Save();
            Save();

            IsActive = false;
            client.Close();
            // TODO: Reset game so it can be used again
        }

        internal Socket client;
    }
}
