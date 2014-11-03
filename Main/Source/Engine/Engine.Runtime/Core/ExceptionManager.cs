using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mud.Engine.Runtime.Core
{
    /// <summary>
    /// Provides helper methods for quickly throwing exceptions based on conditions.
    /// </summary>
    public static class ExceptionManager
    {
        /// <summary>
        /// Throws the exception if the given condition is true.
        /// </summary>
        /// <typeparam name="TException">The type of the exception.</typeparam>
        /// <param name="condition">if set to <c>true</c> [condition].</param>
        /// <param name="data">The data assigned to the Exception.Data property.</param>
        public static void ThrowExceptionIf<TException>(bool condition, params KeyValuePair<string, string>[] data) where TException : Exception, new()
        {
            if (condition)
            {
                var exception = new TException();
                data.AsParallel().ForAll(d => exception.Data.Add(d.Key, d.Value));

                throw exception;
            }
        }

        /// <summary>
        /// Throws the exception if the predicate given evaluates to true.
        /// </summary>
        /// <typeparam name="TException">The type of the exception.</typeparam>
        /// <param name="predicate">The predicate.</param>
        /// <param name="data">The data assigned to the Exception.Data property.</param>
        public static void ThrowExceptionIf<TException>(Func<bool> predicate, params KeyValuePair<string, string>[] data) where TException : Exception, new()
        {
            if (predicate())
            {
                var exception = new TException();
                data.AsParallel().ForAll(d => exception.Data.Add(d.Key, d.Value));

                throw exception;
            }
        }
    }
}
