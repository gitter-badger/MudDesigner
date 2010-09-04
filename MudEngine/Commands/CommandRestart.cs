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
    class CommandRestart : IGameCommand
    {
        public String Name { get; set; }
        public Boolean Override { get; set; }

        public List<String> Help { get; set; }

        public CommandRestart()
        {
            Help = new List<string>();
            Help.Add("Restarts the game server.");
        }

        public void Execute(String command, BaseCharacter player)
        {
            if (player.Role == SecurityRoles.Admin)
            {
                String path = player.ActiveGame.DataPaths.Players;
                
                for (Int32 i = 0; i < player.ActiveGame.GetPlayerCollection().Length; i++)
                {
                    String filename = Path.Combine(path, player.ActiveGame.GetPlayerCollection()[i].Filename);
                    player.ActiveGame.GetPlayerCollection()[i].Save(filename);
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