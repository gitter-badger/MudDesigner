using System;
using System.Collections.Generic;

using MudEngine.GameManagement;
using MudEngine.GameObjects.Characters;
using MudEngine.GameObjects.Environment;

namespace MudEngine.Commands
{
    public abstract class BaseCommand : IGameCommand
    {
        public Boolean Override { get; set; }

        public String Name { get; set; }

        //public String Name { get; set; }
        public List<String> Help { get; set; }

        protected Realm realm;
        protected Zone zone;
        protected Room room;
        protected BaseCharacter player;
        protected Boolean isEditing;

        public BaseCommand()
        {
            Help = new List<string>();
        }

        public abstract void Execute(String command, BaseCharacter player);
    }
}
