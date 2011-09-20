/*
 * Microsoft Public License (Ms-PL)
 * This license governs use of the accompanying software. If you use the software, you accept this license. If you do not accept the license, do not use the software.
 * 1. Definitions
 *    The terms "reproduce," "reproduction," "derivative works," and "distribution" have the same meaning here as under U.S. copyright law.
 *    A "contribution" is the original software, or any additions or changes to the software.
 *    A "contributor" is any person that distributes its contribution under this license.
 *    "Licensed patents" are a contributor's patent claims that read directly on its contribution.
 * 2. Grant of Rights
 *   (A) Copyright Grant- Subject to the terms of this license, including the license conditions and limitations in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free copyright license to reproduce its contribution, prepare derivative works of its contribution, and distribute its contribution or any derivative works that you create.
 *   (B) Patent Grant- Subject to the terms of this license, including the license conditions and limitations in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free license under its licensed patents to make, have made, use, sell, offer for sale, import, and/or otherwise dispose of its contribution in the software or derivative works of the contribution in the software.
 * 3. Conditions and Limitations
 *   (A) No Trademark License- This license does not grant you rights to use any contributors' name, logo, or trademarks.
 *   (B) If you bring a patent claim against any contributor over patents that you claim are infringed by the software, your patent license from such contributor to the software ends automatically.
 *   (C) If you distribute any portion of the software, you must retain all copyright, patent, trademark, and attribution notices that are present in the software.
 *   (D) If you distribute any portion of the software in source code form, you may do so only under this license by including a complete copy of this license with your distribution. If you distribute any portion of the software in compiled or object code form, you may only do so under a license that complies with this license.
 *   (E) The software is licensed "as-is." You bear the risk of using it. The contributors give no express warranties, guarantees or conditions. You may have additional consumer rights under your local laws which this license cannot change. To the extent permitted under your local laws, the contributors exclude the implied warranties of merchantability, fitness for a particular purpose and non-infringement.
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace rScripting.LateBinding
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
                a = Assembly.Load(assembly);
            }
            //If not, then try and load it differently
            else
            {
                a = Assembly.Load(new AssemblyName(assembly));
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
#elif WINDOWS_PHONE
        public ScriptFactory()
        {
            _AssemblyCollection = new List<Assembly>();
            foreach(System.Windows.AssemblyPart part in System.Windows.Deployment.Current.Parts)
            {
                String assemName = part.Source.Replace(".dll", String.Empty);
                Assembly a = Assembly.Load(assemName);
                _AssemblyCollection.Add(a);
            }
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

        public ScriptObject GetScript(String scriptName)
        {
            Type script = typeof(Object);
            Boolean foundScript = false;

            if (_AssemblyCollection.Count == 0)
                return new ScriptObject(null);

            try
            {
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
            }
            catch
            {
                throw new Exception("Error encounted during factory instancing of script " + scriptName + ".");
            }

            ScriptObject obj = new ScriptObject(Activator.CreateInstance(script));
            return obj;
        }
    }
}
