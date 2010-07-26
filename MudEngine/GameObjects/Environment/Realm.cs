//Microsoft .NET Framework
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.ComponentModel;

//MUD Engine
using MudEngine.FileSystem;
using MudEngine.GameObjects;

namespace MudEngine.GameObjects.Environment
{
    public class Realm : BaseObject
    {

        [Category("Environment Information")]
        [Description("A collection of Zones that are contained within this Realm. Players can traverse the world be traveling through Rooms that are contained within Zones. Note that it is not required to place a Zone into a Realm.")]
        //[EditorAttribute(typeof(UIRealmEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public List<Zone> ZoneCollection { get; private set; }

        /// <summary>
        /// Gets or Sets if this Realm is the starting realm for the game.
        /// </summary>
        public bool IsInitialRealm { get; set; }

        /// <summary>
        /// The Initial Starting Zone for this Realm.
        /// </summary>
        public Zone InitialZone { get; private set; }

        public Realm()
        {
            ZoneCollection = new List<Zone>();
        }

        /// <summary>
        /// Returns the requested Zone if the Zone exists within this Realm.
        /// </summary>
        /// <param name="zoneName"></param>
        /// <returns></returns>
        public Zone GetZone(string name)
        {
            foreach (Zone zone in ZoneCollection)
            {
                if (zone.Name == name)
                    return zone;
            }

            return null;
        }

        public void AddZone(Zone zone)
        {
            if (zone.IsInitialZone)
            {
                foreach (Zone z in ZoneCollection)
                {
                    if (z.IsInitialZone)
                    {
                        z.IsInitialZone = false;
                        break;
                    }
                }
            }


            if (zone.IsInitialZone)
                InitialZone = zone;

            //TODO: Check fo duplicates
            ZoneCollection.Add(zone);
        }
    }
}
