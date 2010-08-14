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
        public Boolean IsInitialRealm { get; set; }

        /// <summary>
        /// The Initial Starting Zone for this Realm.
        /// </summary>
        public Zone InitialZone { get; private set; }

        public Realm(GameManagement.Game game) : base(game)
        {
            ZoneCollection = new List<Zone>();
        }

        public override void Save(String path)
        {
            path = Path.Combine(path, Path.GetFileNameWithoutExtension(Filename));
            base.Save(path);

            String filename = Path.Combine(path, Filename);

            FileManager.WriteLine(filename, this.IsInitialRealm.ToString(), "IsInitialRealm");
            FileManager.WriteLine(filename, this.InitialZone.Filename, "InitialZone");

            foreach (Zone z in ZoneCollection)
            {
                FileManager.WriteLine(filename, z.Filename, "ZoneCollection");
                z.Save(path);
            }
        }

        public List<Zone> GetZoneByFilename(String filename)
        {

            List<Zone> zones = new List<Zone>();

            foreach (Zone zone in ZoneCollection)
            {
                if (zone.Filename == filename)
                {
                    zones.Add(zone);
                }
            }

            return zones;
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
            zone.Realm = Filename;
        }
    }
}
