//Microsoft .NET Framework
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.ComponentModel;

//MUD Engine
using MudEngine.Core;
using MudEngine.GameManagement;

namespace MudEngine.GameObjects.Environment
{
    [XmlInclude(typeof(BaseItem))]
    [Serializable]
    public class Door
    {
        public enum RoomTravelType
        {
            Arrival,
            Departure
        }
    
        [Category("Door Settings")]
        [DefaultValue(false)]
        public Boolean IsLocked
        {
            get;
            set;
        }

        [Category("Door Settings")]
        [Browsable(false)]
        public BaseItem RequiredKey
        {
            get;
            set;
        }

        [Category("Door Settings")]
        [DefaultValue(0)]
        public Int32 LevelRequirement
        {
            get;
            set;
        }

        [Category("Door Settings")]
        public AvailableTravelDirections TravelDirection { get; set; }

        /// <summary>
        /// Gets or Sets the Room that the player will be arriving.
        /// </summary>
        public Room ArrivalRoom { get; set; }

        /// <summary>
        /// Gets or Sets the Room that the user is leaving.
        /// </summary>
        public Room DepartureRoom { get; set; }

        private Game _Game;

        public Door(GameManagement.Game game)
        {
            LevelRequirement = 0;
            IsLocked = false;
            RequiredKey = new BaseItem(game);

            _Game = game;
        }

        public Boolean SetRoom(RoomTravelType roomType, String roomPath)
        {
            String[] path = roomPath.Split('>');

            if (path.Length > 3)
            {
                Log.Write("Error in Door.SetRoom(" + roomType.ToString() + ", " + roomPath + ") does not contain a full Room Path.");
                return false;
            }

            //TODO: Load the Realm via Game.World if it isn't loaded yet. Ensures that the Realm is only ever loaded once.
            if (_Game.World.GetRealm(path[0]) != null)
            {
                Realm r = _Game.World.GetRealm(path[0]);

                //TODO: Load the Zone via Game.World.GetRealm(). Ensures that only 1 instance of the Realm is loaded.
                if (r.GetZone(path[1]) != null)
                {
                    List<Zone> zlist = r.GetZone(path[1]);

                    Zone z = zlist[0];

                    //TODO: Load the Room via Game.World.GetRealm().GetZone(). Ensures that the Room is only loaded once in memory.
                    if (z.GetRoom(path[2]) != null)
                    {
                        List<Room> rlist = z.GetRoom(path[2]);

                        if (roomType == RoomTravelType.Arrival)
                            ArrivalRoom = rlist[0];
                        else
                            DepartureRoom = rlist[0];

                        return true;
                    }
                    else
                    {
                        Log.Write("Error in Door.SetRoom(" + roomType.ToString() + ", " + roomPath + ") does not contain a valid Room");
                        return false;
                    }//GetRoom
                }//GetZone
                else
                {
                    Log.Write("Error in Door.SetRoom(" + roomType.ToString() + ", " + roomPath + ") does not contain a valid zone.");
                    return false;
                }
            }//GetRealm
            else
            {
                Log.Write("Error in Door.SetRoom(" + roomType.ToString() + ", " + roomPath + ") does not contain a valid Realm");
                return false;
            }
        }
    }
}
