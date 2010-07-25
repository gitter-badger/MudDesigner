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
            if (player.IsAdmin)
            {
                for (int i = 0; i < player.Game.PlayerCollection.Count; i++)
                    player.Game.PlayerCollection[i].Save(player.Game.PlayerCollection[i].Name + ".dat");
                player.Game.Server.EndServer();
                if (player.Game.ServerType == ProtocolType.Tcp)
                    player.Game.Server.InitializeTCP(555, ref player.Game.PlayerCollection);
                else
                    player.Game.Server.InitializeUDP(555, ref player.Game.PlayerCollection);
                return new CommandResults("Server Restarted.");
            }
            return new CommandResults("Access Denied.");
        }
    }
}