using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;

namespace MudEngine.Engine.Factories
{
    public class EngineFactory
    {
        public static T FindFactory<T>(string type, params object[] values) where T : class
        {
            var types = new List<Type>();

            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                types.AddRange(assembly.GetTypes().Where(
                    t => t.GetInterface(typeof(T).Name) != null &&
                    !t.IsAbstract && // Do not add abstract classes
                    !t.IsInterface)); // Do not add interfaces. Concrete Types only.
            }

            return types.FirstOrDefault() as T;
        }
    }
}
