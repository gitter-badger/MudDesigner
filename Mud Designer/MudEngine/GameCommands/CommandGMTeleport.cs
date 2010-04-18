using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MudDesigner.MudEngine.Interfaces;
using MudDesigner.MudEngine.Characters;
using MudDesigner.MudEngine.Characters.Controlled;
using MudDesigner.MudEngine.GameManagement;
using MudDesigner.MudEngine.GameObjects.Environment;

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

        public CommandResults Execute(BaseCharacter player, ProjectInformation project, Room room, string command)
        {
            PlayerGM playerGM = new PlayerGM();
            
            if (player is PlayerGM)
            {
                playerGM = (PlayerGM)player;
            }
            else
                return null;

            //TODO: Find the Realm/Zone/Room that the GM specified to teleport to.
            return null;
        }
    }
}
