using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;

namespace MudEngine.Engine.Factories
{
    /// <summary>
    /// A simple factory that is used to fetch other factories
    /// </summary>
    public class EngineFactory
    {
        /// <summary>
        /// Finds a Factory that can be used with T
        /// </summary>
        /// <typeparam name="T">The interface that the Factory found must support.</typeparam>
        /// <returns>Returns a Factory that can fetch objects matching T</returns>
        public static Type FindFactory<T>() where T : class
        {
            var supportedTypes = new List<Type>();

            // Loop through each assembly in memory, fetching the Types they contain.
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                // We only want Types that satisfy the following criteria:
                // 1: Must be Generic (as it must support being given <T> specified
                // 2: Must implelemt IFactory, so we have guaranteed method signatures
                Type[] types = assembly.GetTypes()
                    .Where(t => t.IsGenericType && t.GetInterfaces()
                        .FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() ==  typeof(IFactory<>)) != null)
                        .ToArray();

                // Now that we have the Types meeting our criteria, we loop
                // through them to determine which have Constraints matching
                // the <T> supplied to us.
                foreach (Type type in types)
                {
                    foreach (var arg in type.GetGenericArguments())
                    {
                        Type constraint = arg.GetGenericParameterConstraints().FirstOrDefault();

                        // Does this constraint match T?
                        if (constraint == typeof(T))
                        {
                            // If so, then we have found a Factory object that 
                            // can take <T> and return a concrete Type matching it.
                            return type;
                        }
                    }

                    continue;
                }
            }

            return null;
        }
    }
}
