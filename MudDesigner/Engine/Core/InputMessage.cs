using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MudEngine.Engine.Core
{
    public class InputMessage : IMessage
    {
        public InputMessage(string message)
        {
            this.Message = message;
        }

        public string Message { get; set; }
    }
}
