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

        public virtual void OnTravel(AvailableTravelDirections travelDirection)
        {
            if (CurrentRoom.DoorwayExist(travelDirection.ToString()))
            {
                string connectedRoom = CurrentRoom.GetDoor(travelDirection).ConnectedRoom;
                CurrentRoom = (Room)CurrentRoom.Load(connectedRoom);
            }
        }

        public CommandResults ExecuteCommand(string command, BaseCharacter character, Game game, Room room)
        {
            //TODO: Character class can handle a lot of the command management here, checking various things prior to sending
            //the command off to the command engine for execution.
            return CommandEngine.ExecuteCommand(command, character, game, room);
        }

        public void Initialize(ref MudEngine.Networking.ClientSocket rcs)
        {
            sock = rcs;
        }

        public MudEngine.Networking.ClientSocket sock;
    }
}
