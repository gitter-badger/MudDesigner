using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace MudDesigner.MudEngine.GameCommands
{
    public class CommandResults
    {
        /// <summary>
        /// Result of the command.
        /// </summary>
        public object[] Result { get; set; }

        public CommandResults()
        {
        }

        public CommandResults(object[] Result)
        {
            this.Result = Result;
        }

        public CommandResults(string message)
        {
            this.Result = new object[] { message };
        }
    }
}
