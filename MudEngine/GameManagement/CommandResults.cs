//Microsoft .NET Framework
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MudEngine.GameManagement
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
