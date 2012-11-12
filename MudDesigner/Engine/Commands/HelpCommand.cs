/* HelpCommand
 * Product: Mud Designer Engine
 * Copyright (c) 2012 AllocateThis! Studios. All rights reserved.
 * http://MudDesigner.Codeplex.com
 *  
 * File Description: Provides helpful information to the user. The help information is displayed for any command that has a HelpAttribute attached to it.
 */

//Microsoft .NET using statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Reflection;
using System.Xml.Serialization;

//AllocateThis! Mud Designer using statements
using MudDesigner.Engine.Mobs;

namespace MudDesigner.Engine.Commands
{
    /// <summary>
    /// Provides helpful information to the user. The help information is displayed for any command that has a HelpAttribute attached to it.
    /// </summary>
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
