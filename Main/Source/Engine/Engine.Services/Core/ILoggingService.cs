using Mud.Engine.Shared.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mud.Engine.Services.Core
{
    public interface ILoggingService
    {
        void Log(IMessage message);
    }
}
