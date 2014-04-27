using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Mud.Core;

namespace Mud.Models.Mobs
{
    public interface IPlayer : IMob
    {
        IGame Game { get; set; }

        void SendMessage(string message, bool newLine = true);
    }
}
