//-----------------------------------------------------------------------
// <copyright file="HelpCommand.cs" company="AllocateThis!">
//     Copyright (c) AllocateThis! Studio's. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

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
    /// <summary>
    /// Provides helpful information to the user. The help information is displayed for any command that has a HelpAttribute attached to it.
    /// </summary>
    public class HelpCommand : ICommand
    {
        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="player">The player sending the command.</param>
        public void Execute(IPlayer player)
        {
            player.SendMessage("This command is not yet implemented.");
        }
    }
}
