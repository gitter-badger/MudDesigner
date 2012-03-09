using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MudEngine.Game.Environment;
using MudEngine.Core.Interfaces;
using MudEngine.Core;

namespace MudEngine.Game
{
    public class World : IGameComponent
    {
        /// <summary>
        /// Gets a reference to the currently running game.
        /// </summary>
        public StandardGame Game { get; private set; }

        /// <summary>
        /// Gets or Sets the starting location for new characters.
        /// </summary>
        public Room StartLocation { get; set; }

        public World(StandardGame game)
        {
            this.Game = game;
            this._RealmCollection = new List<Realm>();
        }

        public void Initialize()
        {
            Logger.WriteLine("Initializing game world...");
            Realm realm = new Realm(this.Game, "Azeroth", "");
            realm.Initialize();

            //Zone initialize method is called by Realm.
            Zone zone = realm.CreateZone("Bablo", "");

            //Room initialize method is called by Zone
            zone.CreateRoom("Bedroom", "");
            zone.CreateRoom("Hallway", "");

            zone.LinkRooms("Bedroom", "Hallway", AvailableTravelDirections.East);

            this.StartLocation = zone.GetRoom("Bedroom");

            Logger.WriteLine("Initialization completed.");
        }

        public void Save()
        {
        }

        public void Load()
        {
            Logger.WriteLine("World Loading has not been implemented as of yet!");
        }

        public void Destroy()
        {
            throw new NotImplementedException();
        }

        public void CreateRealm(String name, String description)
        {
            Realm r = new Realm(this.Game, name, description);

            this._RealmCollection.Add(r);
        }

        public Realm GetRealm(String name)
        {
            var v = from realm in this._RealmCollection
                    where realm.Name == name
                    select realm;

            Realm r = v.First();
            return r;
        }

        private List<Realm> _RealmCollection;
    }
}
