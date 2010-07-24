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
using MudEngine.GameObjects.Characters.Controlled;
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

        public CommandResults Execute(string command, BaseCharacter player, Game project, Room room)
        {
            if (player is PlayerAdmin)
            {
                for (int i = 0; i < project.PlayerCollection.Count; i++)
                    project.PlayerCollection[i].Save(project.PlayerCollection[i].Name + ".dat");
                project.server.EndServer();
                if (project.ServerType == ProtocolType.Tcp)
                    project.server.InitializeTCP(555, ref project.PlayerCollection);
                else
                    project.server.InitializeUDP(555, ref project.PlayerCollection);
                return new CommandResults("Server Restarted.");
            }
            return new CommandResults("Access Denied.");
        }
    }
}