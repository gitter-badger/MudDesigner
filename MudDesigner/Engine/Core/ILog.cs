using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MudEngine.Engine.Core
{
    /// <summary>
    /// Provides a contract for objects wanting to implement logging features.
    /// </summary>
    public interface ILog
    {
        /// <summary>
        /// Writes the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        void Write(string message);
    }
}
