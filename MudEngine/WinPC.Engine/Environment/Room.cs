using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MudDesigner.Engine.Core;
using MudDesigner.Engine.Objects;
using MudDesigner.Engine.Scripting;

namespace MudDesigner.Engine.Environment
{
    public abstract class Room : BaseGameObject, IGameObject, IRoom
    {
        /// <summary>
        /// Zone that this Room resides within
        /// </summary>
        public IZone Zone { get; set; }

        /// <summary>
        /// Determins if this Room is a safe room where no attacks can be made.
        /// </summary>
        public bool Safe { get; set; }

        //Room Collections
        public Dictionary<string, IPlayer> Occupants { get; set; }
        public Dictionary<AvailableTravelDirections, IDoor> Doorways { get; protected set; }

        //public GameObjectType Type { get; private set; } //Inherited from baseGameObject

        public Room(string name, IZone zone, bool safe = true) : base(name)
        {
            Safe = safe;
            Zone = zone;
            Doorways = new Dictionary<AvailableTravelDirections, IDoor>();
            Occupants = new Dictionary<string, IPlayer>();
        }

        public virtual void AddDoorway(AvailableTravelDirections direction, IRoom arrivalRoom, bool autoAddReverseDirection = true, bool forceOverwrite = true)
        {
            //Check if room is null.
            if (arrivalRoom == null)
                return; //No null references within our collections!

            //If this direction already exists, overwrite it
            //but only if 'forceOverwrite' is true
            if (Doorways.ContainsKey(direction))
            {
                //Remove the old door
                RemoveDoorway(direction);
                //Get a scripted Door instance to add back to the collection
                Door door = (Door)ScriptFactory.GetScript(MudDesigner.Engine.Properties.Engine.Default.DoorType, direction, this, arrivalRoom);
                Doorways.Add(direction, door);
            }
                //Direction does not exist, so lets add a new doorway
            else
            {
                //Get a scripted instance of a Door.
                IDoor door = (Door)ScriptFactory.GetScript(MudDesigner.Engine.Properties.Engine.Default.DoorType, direction, this, arrivalRoom);
                //Add the new doorway to this rooms collection.
                Doorways.Add(direction, door);

                //If autoreverse is enabled, add the doorway to the arrival room too.
                if (autoAddReverseDirection)
                {
                    arrivalRoom.AddDoorway(TravelDirections.GetReverseDirection(direction), this, false, forceOverwrite);
                }
            }
        }

        public virtual void RemoveDoorway(AvailableTravelDirections direction, bool autoRemoveReverseDirection = true)
        {
            if (Doorways.ContainsKey(direction))
            {
                if (autoRemoveReverseDirection)
                {
                    //When removig the reverse direction, always set "autoRemoveReverseDirection" within the Arrival room
                    //to false, otherwise a circular loop will start.
                    Doorways[direction].Arrival.RemoveDoorway(direction, false);
                }
                Doorways.Remove(direction);
            }
        }

        public virtual void BroadcastMessage(string message, List<IPlayer> playersToOmmit = null)
        {
                foreach (IPlayer player in Occupants.Values)
                {
                    if (playersToOmmit != null)
                    {
                        if (playersToOmmit.Contains(player))
                            continue; //Skip this player if it's in the list.
                    }
                    //Send the message
                    player.SendMessage(message);
                }
        }

        public void Save(System.IO.BinaryWriter writer)
        {
            throw new NotImplementedException();
        }

        public void Load(IGame game, System.IO.BinaryReader reader)
        {
            throw new NotImplementedException();
        }

        #region == Events ==
        public delegate void OnEnterHandler(IPlayer player, AvailableTravelDirections enteredDirection);
        public event OnEnterHandler OnEnterEvent;
        public virtual void OnEnter(IPlayer player, AvailableTravelDirections enteredDirection)
        {
            BroadcastMessage(player.Name + " has entered from the " + enteredDirection.ToString());
        }
        #endregion
    }
}
