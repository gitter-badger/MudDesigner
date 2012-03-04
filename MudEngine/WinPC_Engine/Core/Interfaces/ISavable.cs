using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MudEngine.Core.Interfaces
{
    /// <summary>
    /// Public API for classes that need to be saved during runtime.
    /// </summary>
    public interface ISavable
    {
        /// <summary>
        /// Objects filename.
        /// </summary>
        String Filename { get; }

        /// <summary>
        /// Save method for dumping the object to physical file.
        /// </summary>
        /// <param name="path"></param>
        Boolean Save(String filename);

        Boolean Save(String filename, Boolean ignoreFileWrite);

        /// <summary>
        /// Load method for retrieving saved data from file.
        /// </summary>
        /// <param name="filename">Filename is required complete with Path since this object does not exist yet (can not get filename from non-existing object)</param>
        void Load(String filename);
    }
}
