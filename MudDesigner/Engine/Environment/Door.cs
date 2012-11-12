using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MudDesigner.Engine.Core;
using MudDesigner.Engine.Objects;

namespace MudDesigner.Engine.Environment
{
    public abstract class Door : GameObject,  IDoor
    {
        public bool Locked { get; protected set; }

        public IItem Key { get; protected set; }

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

        public override void CopyState(ref dynamic copyTo)
        {
            if (copyTo is Door)
            {
                Scripting.ScriptObject newObject = new Scripting.ScriptObject(copyTo);

                newObject.SetProperty("Locked", Locked, null);
                newObject.SetProperty("Key", Key, null);
                newObject.SetProperty("FacingDirection", FacingDirection, null);
                newObject.SetProperty("Arrival", Arrival, null);
                newObject.SetProperty("Departure", Departure, null);
            }

            base.CopyState(ref copyTo);
        }

        public virtual void Lock(IItem key)
        {
            Key = key;
            Locked = true;
        }

        public virtual bool Unlock(IItem key)
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
