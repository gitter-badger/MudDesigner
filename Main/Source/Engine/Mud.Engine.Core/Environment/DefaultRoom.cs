using Mud.Engine.Core.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mud.Engine.Core.Environment
{
    public class DefaultRoom : IRoom
    {
        public event EventHandler<OccupancyChangedEventArgs> EnteredRoom;

        public event EventHandler<OccupancyChangedEventArgs> LeftRoom;

        public Guid Id { get; set; }

        public string Name { get; set; }

        public double TimeFromCreation { get; protected set; }

        public DateTime CreationDate { get; set; }

        public bool IsEnabled { get; set; }

        public IZone Zone { get; protected set; }

        public ICollection<IDoorway> Doorways { get; protected set; }

        public ICollection<ICharacter> Occupants { get; protected set; }

        public virtual void Initialize(IZone zone)
        {
            this.Zone = zone;
        }

        public void AddOccupantToRoom(ICharacter character)
        {
            // We don't allow the user to enter a disabled room.
            if (this.IsEnabled)
            {
                // TODO: Need to do some kind of communication back to the caller that this can't be traveled to.
                return;
            }

            if (character == null)
            {
                throw new NullReferenceException("Attempted to add a null character to the Room.");
            }

            // Remove the character from their previous room.
            character.CurrentRoom.RemoveOccupantFromRoom(character, this);

            this.Occupants.Add(character);
            this.OnEnteringRoom(character, character.CurrentRoom);

            character.CurrentRoom = this;
        }

        public void RemoveOccupantFromRoom(ICharacter character, IRoom arrivalRoom)
        {
            if (character == null)
            {
                return;
            }

            this.Occupants.Remove(character);
            this.OnLeavingRoom(character, arrivalRoom);
        }

        protected virtual void OnEnteringRoom(ICharacter character, IRoom departingRoom)
        {
            EventHandler<OccupancyChangedEventArgs> handler = this.EnteredRoom;
            if (handler == null)
            {
                return;
            }

            handler(this, new OccupancyChangedEventArgs(character, departingRoom, this));
        }

        protected virtual void OnLeavingRoom(ICharacter character, IRoom arrivalRoom)
        {
            EventHandler<OccupancyChangedEventArgs> handler = this.LeftRoom;
            if (handler == null)
            {
                return;
            }

            handler(this, new OccupancyChangedEventArgs(character, this, arrivalRoom));
        }
    }
}
