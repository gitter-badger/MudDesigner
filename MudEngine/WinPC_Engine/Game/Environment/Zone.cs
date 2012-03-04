using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MudEngine.Core.Interfaces;
using MudEngine.GameScripts;
using MudEngine.Game.Characters;

namespace MudEngine.Game.Environment
{
    public class Zone : BaseScript, IGameComponent, ISavable, IUpdatable
    {
        /// <summary>
        /// Gets or Sets the what stats 
        /// </summary>
        public CharacterStats StatDrain { get; set; }

        public Boolean Safe { get; set; }

        public String Realm { get; private set; }

        public Zone(StandardGame game, String name, String description) : base(game, name, description)
        {
            this._RoomCollection = new List<Room>();
        }

        public void Initialize()
        {
            throw new NotImplementedException();
        }

        public void Destroy()
        {
            throw new NotImplementedException();
        }

        public string Filename
        {
            get { throw new NotImplementedException(); }
        }

        public void Update()
        {
            throw new NotImplementedException();
        }

        public void CreateRoom(String name, String description)
        {

        }

        private List<Room> _RoomCollection;
    }
}
