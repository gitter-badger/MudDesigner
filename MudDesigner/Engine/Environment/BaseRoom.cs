using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

using MudDesigner.Engine.Core;
using MudDesigner.Engine.Objects;
using MudDesigner.Engine.Scripting;

using MudDesigner.Engine.Mobs;
namespace MudDesigner.Engine.Environment
{
    public abstract class BaseRoom : GameObject, IRoom
    {
        /// <summary>
        /// Zone that this Room resides within
        /// </summary>
        [Browsable(false)]
        public IZone Zone { get; set; }

        /// <summary>
        /// Determins if this Room is a safe room where no attacks can be made.
        /// </summary>
        public bool Safe { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Browsable(false)]
        public Senses Sense { get; set; }

        //Room Collections
        [Browsable(false)]
        public Dictionary<string, IPlayer> Occupants { get; set; }

        [Browsable(false)]
        public Dictionary<AvailableTravelDirections, IDoor> Doorways { get; protected set; }

        public BaseRoom(string name, IZone zone) : base()
        {
            Zone = zone;
            Doorways = new Dictionary<AvailableTravelDirections, IDoor>();
            Occupants = new Dictionary<string, IPlayer>();
            Name = name;
        }
        public BaseRoom(string name, IZone zone, Guid id, bool safe = false) : base(id)
        {
            Safe = safe;
            Zone = zone;
            Doorways = new Dictionary<AvailableTravelDirections, IDoor>();
            Occupants = new Dictionary<string, IPlayer>();
            Name = name;
        }

        public override void Destroy()
        {
            Doorways.Clear();

            this.BroadcastMessage("Room is being destroyed!  If you become trapped, please inform an admin.",
                Occupants.Values.ToList<IPlayer>());

            foreach (IPlayer player in Occupants.Values)
            {
                //TODO: Move players into a default room when this is destroyed.
                //player.Move(MudDesigner.Engine.Properties.EngineSettings.Default.LoginRoom);
            }
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
                Door door = (Door)ScriptFactory.GetScript(MudDesigner.Engine.Properties.EngineSettings.Default.DoorType, direction, this, arrivalRoom);
                Doorways.Add(direction, door);
            }
                //Direction does not exist, so lets add a new doorway
            else
            {
                //Get a scripted instance of a Door.
                IDoor door = (Door)ScriptFactory.GetScript(MudDesigner.Engine.Properties.EngineSettings.Default.DoorType, direction, this, arrivalRoom);
                //Add the new doorway to this rooms collection.
                Doorways.Add(direction, door);

                //If autoreverse is enabled, add the doorway to the arrival room too.
                if (autoAddReverseDirection)
                {
                    arrivalRoom.AddDoorway(TravelDirections.GetReverseDirection(direction), this, false, forceOverwrite);
                }
            }
        }

        public IDoor GetDoorway(AvailableTravelDirections direction)
        {
            if (Doorways.ContainsKey(direction))
                return Doorways[direction];
            else
                return null;
        }

        public IDoor[] GetDoorways()
        {
            List<IDoor> doorways = new List<IDoor>();
            foreach (IDoor door in Doorways.Values)
            {
                doorways.Add(door);
            }

            return doorways.ToArray();
        }

        public virtual void RemoveDoorway(AvailableTravelDirections direction, bool autoRemoveReverseDirection = true)
        {
            if (Doorways.ContainsKey(direction))
            {
                if (autoRemoveReverseDirection)
                {
                    //When removig the reverse direction, always set "autoRemoveReverseDirection" within the Arrival room
                    //to false, otherwise a circular loop will start.
                    Doorways[direction].Arrival.RemoveDoorway(TravelDirections.GetReverseDirection(direction), false);
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

        public override string ToString()
        {
            return Zone.Realm.Name + "->" + Zone.Name + "->" + Name;
        }

        #region == Events ==
        public delegate void OnEnterHandler(IPlayer player, AvailableTravelDirections enteredDirection);
        public event OnEnterHandler OnEnterEvent;
        public virtual void OnEnter(IPlayer player, AvailableTravelDirections enteredDirection)
        {
            BroadcastMessage(player.Name + " has entered from the " + enteredDirection.ToString());
        }
        #endregion

        public void BroadcastMessage(string message)
        {
            throw new NotImplementedException();
        }


        public void OnEnter(IMob occupant, IEnvironment departureEnvironment, AvailableTravelDirections direction)
        {
            throw new NotImplementedException();
        }

        public void OnLeave(IMob occupant, IEnvironment arrivalEnvironment, AvailableTravelDirections direction)
        {
            throw new NotImplementedException();
        }
    }
}
