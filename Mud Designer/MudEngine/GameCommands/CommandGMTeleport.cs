using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MudDesigner.MudEngine.Interfaces;
using MudDesigner.MudEngine.Characters.Controlled;

namespace MudDesigner.MudEngine.GameCommands
{
    public class CommandGMTeleport : IGameCommand
    {
        public string Name { get; set; }
        public bool Override { get; set; }

        public CommandGMTeleport()
        {
            Override = true;
        }

        public object Execute(params object[] parameters)
        {
            PlayerGM playerGM = new PlayerGM();
            bool foundGM = false;

            foreach (object obj in parameters)
            {
                if (obj is PlayerGM)
                {
                    foundGM = true;
                    break;
                }
            }

            if (!foundGM)
                return null;

            //TODO: Find the Realm/Zone/Room that the GM specified to teleport to.
            return null;
        }
    }
}
