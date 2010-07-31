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
        public string Name { get; set; }
        public bool Override { get; set; }

        public CommandResults Execute(string command, BaseCharacter player)
        {
            if (player.Role == SecurityRoles.Admin)
            {
                string path = player.ActiveGame.DataPaths.Players;
                
                for (int i = 0; i < player.ActiveGame.PlayerCollection.Length; i++)
                {
                    string filename = Path.Combine(path, player.ActiveGame.PlayerCollection[i].Filename);
                    player.ActiveGame.PlayerCollection[i].Save(filename);
                }
                player.ActiveGame.Server.EndServer();
                player.ActiveGame.Server.Initialize(555, ref player.ActiveGame.PlayerCollection);
                return new CommandResults("Server Restarted.");
            }
            return new CommandResults("Access Denied.");
        }
    }
}