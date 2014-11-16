using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mud.Engine.Runtime.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class ExceptionFactoryResult
    {
        /// <summary>
        /// Callback on the results of an ExceptionFactory invocation
        /// </summary>
        /// <param name="callback">The callback.</param>
        public void ElseDo(Action callback)
        {
            callback();
        }
    }
}
