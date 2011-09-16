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

        private Realm realm;
        private BaseCharacter player;
        private Boolean isEditing;

        public BaseCommand()
        {
            Help = new List<string>();
        }

        public abstract void Execute(String command, BaseCharacter player);
    }
}
