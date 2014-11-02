//-----------------------------------------------------------------------
// <copyright file="DefaultDoorway.cs" company="Sully">
//     Copyright (c) Johnathon Sullinger. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Mud.Engine.Core.Environment
{
    using System;
    using System.Linq;
    using Mud.Engine.Core.Character;
    using Mud.Engine.Core.Environment.Travel;
    using Mud.Engine.Core.Engine;

    /// <summary>
    /// The default doorway class for the engine. Provides methods for connecting rooms, disconnecting rooms and traveling between rooms.
    /// </summary>
    public class DefaultDoorway : IDoorway, IPersistedObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultDoorway"/> class.
        /// </summary>
        public DefaultDoorway()
        {
        }

        /// <summary>
        /// Gets or sets the unique identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultDoorway"/> class.
        /// </summary>
        /// <param name="departureDirection">The departure direction.</param>
        public DefaultDoorway(ITravelDirection departureDirection)
        {
            this.DepartureDirection = departureDirection;
        }

        /// <summary>
        /// Gets or sets the direction that must be traveled in order to move from the departure room.
        /// </summary>
        [PersistValue(PersistValueAttribute.PersistStyle.StringRepresentation)]
        public ITravelDirection DepartureDirection { get; set; }

        /// <summary>
        /// Gets or sets the arrival room.
        /// </summary>
        [PersistValue(PersistValueAttribute.PersistStyle.RelatedPersistedObject)]
        public IRoom ArrivalRoom { get; protected set; }

        /// <summary>
        /// Connects two rooms together.
        /// </summary>
        /// <param name="departureRoom">The departure room.</param>
        /// <param name="arrivalRoom">The arrival room.</param>
        /// <param name="createDoorwayForArrival">If set to <c>true</c>, a new doorway will be created for the arrival room that is linked back to the originating departure room.
        /// Otherwise, a one-way door will be created.</param>
        /// <exception cref="System.NullReferenceException">
        /// Neither the departure room or arrival room can be null
        /// or
        /// Can not connect rooms when the DepartureDirection is null.
        /// or
        /// Can not connect rooms when the doorways collection is null.
        /// </exception>
        public void ConnectRooms(IRoom departureRoom, IRoom arrivalRoom, bool createDoorwayForArrival = true)
        {
            if (departureRoom == null || arrivalRoom == null)
            {
                throw new NullReferenceException("Neither the departure room or arrival room can be null");
            }
            else if (this.DepartureDirection == null)
            {
                throw new NullReferenceException("Can not connect rooms when the DepartureDirection is null.");
            }
            else if (departureRoom.Doorways == null)
            {
                throw new NullReferenceException("Can not connect rooms when the doorways collection is null.");
            }

            // Set up the departure room first.
            this.ArrivalRoom = arrivalRoom;

            departureRoom.Doorways.Add(this);

            // The opposite rooms arrival needs to be the departing room that this doorway is connected to.
            if (createDoorwayForArrival)
            {
                // Create a new doorway for the arrival room, so that you can leave the room once you are in it.
                ITravelDirection oppositeDirection = this.DepartureDirection.GetOppositeDirection();
                IDoorway arrivalDoorway = new DefaultDoorway(oppositeDirection);
                arrivalDoorway.ConnectRooms(arrivalRoom, departureRoom, false);
            }
        }

        /// <summary>
        /// Disconnects the two connected rooms.
        /// </summary>
        public void DisconnectRoom()
        {
            // If this doorway isn't set up, then there is nothing to disconnect.
            if (this.DepartureDirection == null || this.ArrivalRoom == null)
            {
                return;
            }

            // This doorway always belongs to the departing room. We can get the departing room
            // by walking through the arrival rooms doorways and finding the opposite doorway.
            string oppositeDirection = this.DepartureDirection.GetOppositeDirection().Direction;
            IDoorway oppositeDoorway = this.ArrivalRoom.Doorways
                .FirstOrDefault(d => d.DepartureDirection.Direction == oppositeDirection);
            IRoom departureRoom = oppositeDoorway.ArrivalRoom;

            // Remove the doorway from the opposite room.
            oppositeDoorway.ArrivalRoom.Doorways.Remove(oppositeDoorway);

            // Remove this door from the arrival room
            this.ArrivalRoom.Doorways.Remove(oppositeDoorway);
            departureRoom.Doorways.Remove(this);
        }

        /// <summary>
        /// Moves the given occupant from it's current room to the arrival room associated with this doorway.
        /// </summary>
        /// <param name="occupant">The occupant.</param>
        /// <exception cref="System.NullReferenceException">Occupant can not be null when traversing doorways.</exception>
        public void TraverseDoorway(ICharacter occupant)
        {
            if (occupant == null)
            {
                throw new NullReferenceException("Occupant can not be null when traversing doorways.");
            }

            this.ArrivalRoom.AddOccupantToRoom(occupant);
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format("{0} doorway for {1} room.", this.DepartureDirection.Direction, this.ArrivalRoom.Name);
        }
    }
}
