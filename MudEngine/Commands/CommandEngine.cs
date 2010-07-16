using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MudEngine.Commands
{
    class CommandEngine
    {
        /// <summary>
        /// Gets or Sets a Dictionary list of available commands to use.
        /// </summary>
        static internal Dictionary<string, IGameCommand> Commands { get; set; }

        public List<string> GetCommands
        {
            get
            {
                List<string> temp = new List<string>();
                foreach (string name in Commands.Keys)
                {
                    temp.Add(name);
                }

                return temp;
            }
        }
    }
}
