using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MudDesigner.Engine.Core;
using MudDesigner.Engine.Objects;

namespace MudDesigner.Engine.Environment
{
    public abstract class Door : IDoor
    {
        public bool Locked { get; protected set; }

        public IInventory Key { get; protected set; }

        public AvailableTravelDirections FacingDirection { get;  protected set; }

        public IRoom Arrival { get; protected set; }

        public IRoom Departure { get; protected  set; }

        public Door()
        {

        }

        public Door(AvailableTravelDirections direction, IRoom departingRoom, IRoom arrivingRoom)
        {
            FacingDirection = direction;
            Arrival = arrivingRoom;
            Departure = departingRoom;
        }

        public virtual void Lock(IInventory key)
        {
            Key = key;
            Locked = true;
        }

        public virtual bool Unlock(IInventory key)
        {
            if (key == Key)
                Locked = false;

            return Locked;
        }


        public void SetFacingDirection(AvailableTravelDirections directions)
        {
            FacingDirection = directions;
        }

        public void SetArrivalRoom(IRoom room)
        {
            Arrival = room;
        }

        public void SetDepartingRoom(IRoom room)
        {
            Departure = room;
        }
    }

}
