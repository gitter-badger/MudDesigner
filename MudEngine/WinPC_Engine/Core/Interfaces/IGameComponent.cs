using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MudEngine.Core.Interfaces
{
    /// <summary>
    /// Provides an API for scripts that need to be Initialized and Destroyed during gameplay.
    /// </summary>
    public interface IGameComponent
    {
        /// <summary>
        /// Method for initializing any code that must be executed prior to anything else.
        /// </summary>
        void Initialize();

        /// <summary>
        /// Method for destroying any resources that the class might be using.
        /// </summary>
        void Destroy();
    }
}
