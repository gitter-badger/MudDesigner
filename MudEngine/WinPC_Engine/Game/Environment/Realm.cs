using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;

using MudEngine.Core.Interfaces;
using MudEngine.Game.Characters;
using MudEngine.Game;
using MudEngine.GameScripts;

namespace MudEngine.Game.Environment
{
    public class Realm : Environment
    {
        public Realm(StandardGame game, String name, String description) : base(game, name, description)
        {
            this._ZoneCollection = new List<Zone>();
        }

        public Zone CreateZone(String name, String description)
        {
            Zone zone = new Zone(this.Game, name, description);
            this._ZoneCollection.Add(zone);
            zone.Realm = this;
            return zone;
        }

        public Zone GetZone(String name)
        {
            var v = from zone in this._ZoneCollection
                    where zone.Name == name
                    select zone;

            return v.First();
        }

        private List<Zone> _ZoneCollection;
    }
}
