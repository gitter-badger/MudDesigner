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
        public Zone InitialZone { get; set; }

        protected override string SavePath
        {
            get
            {
                return Path.Combine(ActiveGame.DataPaths.Environment, Path.GetFileNameWithoutExtension(this.Filename));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="game"></param>
        public Realm(GameManagement.Game game) : base(game)
        {
            ZoneCollection = new List<Zone>();
            InitialZone = new Zone(game);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        public override void Load(string filename)
        {
            base.Load(filename);

            IsInitialRealm = Convert.ToBoolean(FileManager.GetData(filename, "IsInitialRealm"));

            //Load all zones
            foreach (String zone in FileManager.GetCollectionData(filename, "ZoneCollection"))
            {
                Zone z = new Zone(ActiveGame);
                String path = Path.Combine(ActiveGame.DataPaths.Environment, Path.GetFileNameWithoutExtension(this.Filename), "Zones", Path.GetFileNameWithoutExtension(zone));
                z.Load(Path.Combine(path, zone));

                //Check if this is the initial Zone.
                if (z.IsInitialZone)
                    InitialZone = z;

                ZoneCollection.Add(z);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Save()
        {
            String path = Path.Combine(ActiveGame.DataPaths.Environment, Path.GetFileNameWithoutExtension(Filename));
            base.Save();

            String filename = Path.Combine(path, Filename);

            FileManager.WriteLine(filename, this.IsInitialRealm.ToString(), "IsInitialRealm");
            if (this.InitialZone.Name != "New Zone")
                FileManager.WriteLine(filename, this.InitialZone.Filename, "InitialZone");

            String zonePath = Path.Combine(path, "Zones");
            foreach (Zone z in ZoneCollection)
            {
                FileManager.WriteLine(filename, z.Filename, "ZoneCollection");
                
                z.Save();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public List<Zone> GetZone(String filename)
        {
            List<Zone> zones = new List<Zone>();

            if (!filename.ToLower().EndsWith(".zone"))
                filename += ".zone";

            foreach (Zone zone in ZoneCollection)
            {
                if (zone.Filename.ToLower() == filename.ToLower())
                {
                    zones.Add(zone);
                }
            }

            return zones;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="zone"></param>
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

            //Set the Zones default senses to that of the Realm provided the Zone does 
            //not already have a sense description assigned to it.
            if ((!String.IsNullOrEmpty(this.Feel)) && (String.IsNullOrEmpty(zone.Feel)))
                zone.Feel = this.Feel;
            if ((!String.IsNullOrEmpty(this.Listen)) && (String.IsNullOrEmpty(zone.Listen)))
                zone.Listen = this.Listen;
            if ((!String.IsNullOrEmpty(this.Smell)) && (String.IsNullOrEmpty(zone.Smell)))
                zone.Smell = this.Smell;
        }
    }
}