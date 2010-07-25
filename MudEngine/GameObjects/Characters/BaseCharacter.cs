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
        /// Gets or Sets if this user has Admin privileges or not.
        /// </summary>
        public Boolean IsAdmin { get; private set; }

        /// <summary>
        /// Gets a reference to the currently running game.
        /// </summary>
        public Game Game { get; private set; }

        public BaseCharacter(Game game)
        {
            Game = game;
            CurrentRoom = game.InitialRealm.InitialZone.InitialRoom;
        }

        public virtual void OnTravel(AvailableTravelDirections travelDirection)
        {
            if (CurrentRoom.DoorwayExist(travelDirection.ToString()))
            {
                string connectedRoom = CurrentRoom.GetDoor(travelDirection).ConnectedRoom;
                CurrentRoom = (Room)CurrentRoom.Load(connectedRoom);
            }
        }

        public CommandResults ExecuteCommand(string command)
        {
            //TODO: Character class can handle a lot of the command management here, checking various things prior to sending
            //the command off to the command engine for execution.
            return CommandEngine.ExecuteCommand(command, this);
        }

        public void Initialize(ref MudEngine.Networking.ClientSocket rcs)
        {
            CurrentRoom = Game.InitialRealm.InitialZone.InitialRoom;
            sock = rcs;
        }

        public MudEngine.Networking.ClientSocket sock;
    }
}
