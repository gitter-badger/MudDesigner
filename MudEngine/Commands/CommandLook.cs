﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MudEngine.GameObjects.Characters;
using MudEngine.FileSystem;
using MudEngine.Commands;
using MudEngine.GameManagement;
using MudEngine.GameObjects.Environment;
using MudEngine.GameObjects.Items;

namespace MudEngine.Commands
{
    public class CommandLook : IGameCommand
    {
        public String Name { get; set; }
        public Boolean Override { get; set; }
        public List<String> Help { get; set; }
        public void Execute(String command, BaseCharacter player)
        {
            if (player.CurrentRoom == null)
            {
                player.Send("You are not within any Room.");
                return;
            }

            if (player.CurrentRoom.DetailedDescription.Count == 0)
                player.Send(player.CurrentRoom.Description);
            else
            {
                foreach(String entry in player.CurrentRoom.DetailedDescription)
                {
                    player.Send(entry);
                }
            }
        }
    }
}
