using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MudEngine.Engine.Factories
{
    /// <summary>
    /// Defines a contract for creating a Factory object.
    /// </summary>
    /// <typeparam name="T">An interface that the factory is targeting.</typeparam>
    public interface IFactory<T>
    {
        /// <summary>
        /// Gets the objects.
        /// </summary>
        /// <param name="fromAssemblies">From assemblies.</param>
        /// <returns>Returns a collection of objects matching T</returns>
        List<T> GetObjects(Assembly[] fromAssemblies = null);

        /// <summary>
        /// Gets the world.
        /// </summary>
        /// <typeparam name="UTypeToFetch">The type of the type to fetch.</typeparam>
        /// <param name="fromAssemblies">From assemblies.</param>
        /// <returns>Returns an instance of UTypeToFetch casted as T.</returns>
        T GetObject<UTypeToFetch>(Assembly[] fromAssemblies = null, string compatibleType = null);
    }
}
