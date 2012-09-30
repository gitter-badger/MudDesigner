using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MudDesigner.Engine.Core;
using MudDesigner.Engine.Objects;

namespace MudDesigner.Engine.Environment
{
    public enum TravelDirections
    {
        North,
        South,
        East,
        West,
        Up,
        Down,
    }

    public abstract class EngineRoom : BaseGameObject, IGameObject, IRoom
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
        public Dictionary<TravelDirections, IDoor> Doorways { get; protected set; }

        public EngineRoom(string name, Guid id, bool safe = true) : base(name, id)
        {
            Safe = safe;
        }

        public void AddDoorway(TravelDirections direction, IRoom arrivalRoom, bool forceOverwrite = true)
        {
            //Check if room is null.
            if (arrivalRoom == null)
                return; //No null references within our collections!

            //If this direction already exists, overwrite it
            //but only if 'forceOverwrite' is true
            if (Doorways.ContainsKey(direction))
            {
                Doorways[direction].Arrival = arrivalRoom;
            }
            else
            {
                //Doorways.Add(direction, new Door);
            }
        }

        public void BroadcastMessage(string message, List<IPlayer> playersToOmmit = null)
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
        public delegate void OnEnterHandler(IPlayer player, TravelDirections enteredDirection);
        public event OnEnterHandler OnEnterEvent;
        public virtual void OnEnter(IPlayer player, TravelDirections enteredDirection)
        {
            BroadcastMessage(player.Name + " has entered from the " + enteredDirection.ToString());
        }
        #endregion


        public new GameObjectType Type
        {
            get { throw new NotImplementedException(); }
        }
    }
}
