using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MudEngine.DAL
{
    /// <summary>
    /// Contains the paths for the engines file storage.
    /// </summary>
    public struct DataPaths
    {
        /// <summary>
        /// Path to the engines Script directory
        /// </summary>
        public String Scripts { get; set; }

        /// <summary>
        /// Path to the engines Environment files.
        /// </summary>
        public String Environments { get; set; }

        /// <summary>
        /// Gets the Path to the Characters save directory
        /// </summary>
        public String Characters { get; set; }

        /// <summary>
        /// Gets the path to the saved players directory.
        /// </summary>
        public String Players { get; set; }
    }
}
