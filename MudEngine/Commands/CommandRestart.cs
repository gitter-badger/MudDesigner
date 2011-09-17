//Microsoft.NET Framework
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;

//MUD Engine
using MudEngine.GameObjects.Characters;
using MudEngine.GameManagement;
using MudEngine.GameObjects.Environment;
using MudEngine.GameObjects;
using MudEngine.FileSystem;

namespace MudEngine.Commands
{
    class CommandRestart : BaseCommand
    {
        public CommandRestart()
        {
            Help.Add("Restarts the game server.");
        }

        public override void Execute(String command, BaseCharacter player)
        {
            if (player.Role == SecurityRoles.Admin)
            {
                String path = player.ActiveGame.DataPaths.Players;
                
                for (Int32 i = 0; i < player.ActiveGame.GetPlayerCollection().Length; i++)
                {
                    String filename = Path.Combine(path, player.ActiveGame.GetPlayerCollection()[i].Filename);
                    player.ActiveGame.GetPlayerCollection()[i].Save();
                }

                //player.ActiveGame.Server.EndServer(); //-Handled in Game.Shutdown() below.
                player.ActiveGame.Shutdown();
                player.ActiveGame.Start();
                /* Game.Start() calls this, do we need a reference to the GetPlayerCollection()?
                 * They should be unloaded anyway and re-loaded during game.start to force a clean restart of all objects.
                player.ActiveGame.Server.Initialize(555, ref player.ActiveGame.GetPlayerCollection());
                 */

                Log.Write("Server Restart Completed.");
                //This is never printed as CommandResults is no longer outputted to the player console, player.Send is used
                player.Send("Server Restarted.");
                return;
            }

            player.Send("Access Denied.");
        }
    }
}