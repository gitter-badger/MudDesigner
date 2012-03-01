using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

using MudEngine.Core;
using MudEngine.Game;
using MudEngine.GameScripts;

namespace MudEngine.Scripting
{
    public class ScriptFactory
    {
        //The assembly loaded that will be used.
        private List<Assembly> _AssemblyCollection;

#if WINDOWS_PC
        /// <summary>
        /// Constructor for a Windows PC Script Factory
        /// </summary>
        /// <param name="assembly"></param>
        public ScriptFactory(String assembly)
        {
            Assembly a;
            _AssemblyCollection = new List<Assembly>();
            
            //See if a file exists first with this assembly name.
            if (File.Exists(assembly))
            {
                a = Assembly.Load(new AssemblyName(assembly));
            }
            //If not, then try and load it differently
            else
            {
                a = Assembly.Load(assembly);
            }

            if (a == null)
                return;

            //Add the assembly to our assembly collection.
            _AssemblyCollection.Add(a);
        }

        /// <summary>
        /// Alternate Constructor for a Windows PC ScriptFactory
        /// </summary>
        /// <param name="assembly"></param>
        public ScriptFactory(Assembly assembly)
        {
            _AssemblyCollection = new List<Assembly>();
            //Add the supplied assembly to our AssemblyCollection
            _AssemblyCollection.Add(assembly);
        }
#endif
        /// <summary>
        /// Adds another assembly to the factories assembly collection.
        /// </summary>
        /// <param name="assembly">provides the name of the assembly, or file name that needs to be loaded.</param>
        public void AddAssembly(String assembly)
        {
            Assembly a;

            //See if a file exists first with this assembly name.
            if (File.Exists(assembly))
            {
                a = Assembly.Load(new AssemblyName(assembly));
            }
            //If not, then try and load it differently
            else
            {
                a = Assembly.Load(assembly);
            }

            //Add the assembly to our assembly collection.
            _AssemblyCollection.Add(a);
        }

        /// <summary>
        /// Adds another assembly to the factories assembly collection.
        /// </summary>
        /// <param name="assembly">Provides a reference to the assembly that will be added to the collection.</param>
        public void AddAssembly (Assembly assembly)
        {
            //Add the supplied assembly to our AssemblyCollection
            _AssemblyCollection.Add(assembly);
        }

        public BaseScript GetScript(String scriptName, StandardGame game)
        {
            Type script = typeof(Object);
            Boolean foundScript = false;

            if (_AssemblyCollection.Count == 0)
                return new BaseScript(game, "New Object", String.Empty);

            try
            {  
#if WINDOWS_PC
                foreach (Assembly a in _AssemblyCollection)
                {
                    //The assembly can be null if accessing after a failed compilation.
                    if (a == null)
                        continue;

                    foreach (Type t in a.GetTypes())
                    {
                        if (t.Name == scriptName)
                        {
                            script = t;
                            foundScript = true;
                            break;
                        }
                    }

                    if (foundScript)
                        break;
                }
#elif WINDOWS_PHONE
                foreach (Type t in Assembly.GetExecutingAssembly().GetTypes())
                {
                    if (t.Name == scriptName)
                    {
                        script = t;
                        foundScript = true;
                        break;
                    }
                }
#endif
            }
            catch
            {
                throw new Exception("Error encounted during factory instancing of script " + scriptName + ".");
            }

            try
            {
                BaseScript obj = (BaseScript)Activator.CreateInstance(script, game, "New Object", String.Empty);
                return obj;
            }
            catch
            {
                Logger.WriteLine("ERROR: Failed to locate and instance script (" + scriptName + ")");
                return new BaseScript(game, "New Object", String.Empty);
            }
        }
    }
}
