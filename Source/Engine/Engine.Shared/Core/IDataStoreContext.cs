using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mud.Engine.Shared.Core
{
    public interface IDataStoreContext
    {
        bool IsValid { get; set; }
    }
}
