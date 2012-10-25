using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Reflection;
using System.Xml.Serialization;

using MudDesigner.Engine.Mobs;

namespace MudDesigner.Engine.Commands
{
    public class HelpCommand : ICommand
    {
        public string Command { get; private set; }
        public IPlayer Player { get; private set; }

        public HelpCommand(string command, IPlayer player)
        {
            Command = command;
            Player = player;
        }

        public void Execute()
        {
            Type[] com = null;

            foreach (Type t in com)
            {
                if (t.Name == Command + "Command")
                {
                    object[] a = t.GetCustomAttributes(typeof(HelpAttribute), true);

                    if (a.Length == 0)
                        break;

                    HelpAttribute help = (HelpAttribute)a[0];
                    Player.SendMessage(help.Description);
                }
            }
        }
    }
}
