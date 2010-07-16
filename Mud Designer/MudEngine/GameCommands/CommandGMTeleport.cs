using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MudDesigner.Engine.Interfaces;
using MudEngine.GameObjects.Characters;
using MudEngine.GameObjects.Characters.Controlled;
using MudEngine.GameManagement;
using MudEngine.GameObjects.Environment;

namespace MudEngine.Commands
{
    public class CommandGMTeleport : IGameCommand
    {
        public string Name { get; set; }
        public bool Override { get; set; }

        public CommandGMTeleport()
        {
            Override = true;
        }

        public CommandResults Execute(BaseCharacter player, GameSetup project, Room room, string command)
        {
            /* TODO: Re-implement this.
            PlayerGM playerGM = new PlayerGM();
            
            if (player is PlayerGM)
            {
                playerGM = (PlayerGM)player;
            }
            else
                return null;
            */
            //TODO: Find the Realm/Zone/Room that the GM specified to teleport to.
            return null;
        }
    }
}
