using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MudDesigner.Engine.Core;

namespace MudDesigner.Engine.Environment
{
    public abstract class Door : IDoor
    {
        public bool Locked { get; protected set; }

        public BaseGameObject Key { get; protected set; }

        public AvailableTravelDirections FacingDirection { get; protected set; }

        public IRoom Arrival { get; protected set; }

        public IRoom Departure { get; protected set; }

        public Door(AvailableTravelDirections direction, IRoom departingRoom, IRoom arrivingRoom)
        {
            FacingDirection = direction;
            Arrival = arrivingRoom;
            Departure = departingRoom;
        }

        public virtual void Lock(BaseGameObject key)
        {
            Key = key;
            Locked = true;
        }

        public virtual bool Unlock(BaseGameObject key)
        {
            if (key == Key)
                Locked = false;

            return Locked;
        }
    }
}
