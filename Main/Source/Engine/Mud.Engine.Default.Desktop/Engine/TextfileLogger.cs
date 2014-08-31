using Mud.Engine.Core.Engine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mud.Engine.DefaultDesktop.Engine
{
    public class TextfileLogger: ILogger
    {
        public void Log<TMessage>(TMessage message) where TMessage : IMessage, new()
        {
            using (var outfile = new StreamWriter(@"\Log.txt"))
            {
                outfile.Write(string.Format("{0} - {1}", typeof(TMessage).Name, message.Message));
            }
        }
    }
}
