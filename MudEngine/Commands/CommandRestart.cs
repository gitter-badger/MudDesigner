using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using MudEngine.GameObjects.Characters;
using MudEngine.GameManagement;
using MudEngine.GameObjects.Environment;
using MudEngine.GameObjects;
using MudEngine.FileSystem;
using System.Net;
using System.Net.Sockets;

namespace MudEngine.Commands
{
    class CommandRestart : IGameCommand
    {
        public string Name { get; set; }
        public bool Override { get; set; }

        public CommandResults Execute(string command, BaseCharacter player, Game project, Room room)
        {
            /*if (player.admin)
            {
                for (int i = 0; i < project.player.Length; i++)
                    project.player[i].Save(project.player[i].Name + ".dat");
                project.server.EndServer();
                if (project.ServerType == ProtocolType.Tcp)
                    project.server.InitializeTCP(555, ref project.player);
                else
                    project.server.InitializeUDP(555, ref project.player);
                return new CommandResults("Server Restarted.");
            }*/
            return new CommandResults("Access Denied.");
        }
    }
}