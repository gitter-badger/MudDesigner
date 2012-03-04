using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MudEngine.Core.Interfaces;
using MudEngine.Core;
using MudEngine.Game;
using MudEngine.Game.Characters;
using MudEngine.GameScripts;
namespace MudEngine.Game.Environment
{
    public class Room : BaseScript, IUpdatable
    {
        public Room(StandardGame game, String name, String description)
            : base(game, name, description)
        {
            this._Doors = new List<Doorway>();
        }

        public void Update()
        {
            throw new NotImplementedException();
        }

        public String[] GetDescription()
        {
            return new List<String>().ToArray();
        }

        private List<Doorway> _Doors;
    }
}
