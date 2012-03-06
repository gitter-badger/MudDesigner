using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MudEngine.Game.Environment;
using MudEngine.Core.Interfaces;

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
            Realm realm = new Realm(this.Game, "Azeroth", "");
            Zone zone = realm.CreateZone("Bablo", "");

            zone.CreateRoom("Bedroom", "");
            zone.CreateRoom("Hallway", "");

            zone.LinkRooms("Bedroom", "Hallway", AvailableTravelDirections.East);

            this.StartLocation = zone.GetRoom("Bedroom");
        }

        public void Save()
        {
        }

        public void Load()
        {
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
