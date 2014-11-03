using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mud.Engine.Runtime.Core
{
    public static class ExceptionManager
    {
        public static void ThrowExceptionIf<T>(bool condition, params KeyValuePair<string, string>[] data) where T : Exception, new()
        {
            if (condition)
            {
                var exception = new T();
                data.AsParallel().ForAll(d => exception.Data.Add(d.Key, d.Value));

                throw exception;
            }
        }
    }
}
