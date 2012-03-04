using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MudEngine.Game.Environment;

namespace MudEngine.Game
{
    public class World
    {
        public StandardGame Game { get; private set; }

        public World(StandardGame game)
        {
            this.Game = game;
            this._RealmCollection = new List<Realm>();
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
