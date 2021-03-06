﻿//-----------------------------------------------------------------------
// <copyright file="ScriptFactory.cs" company="AllocateThis!">
//     Copyright (c) AllocateThis! Studio's. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using MudDesigner.Engine.Core;
using MudDesigner.Engine.Networking;
using MudDesigner.Engine.Objects;
using log4net;

namespace MudDesigner.Engine.Scripting
{
    /// <summary>
    /// The ScriptFactory provides helper methods for instancing scripted game objects
    /// </summary>
    public static class ScriptFactory
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(ScriptFactory));

        //The assembly loaded that will be used.
        private static List<Assembly> assemblyCollection = new List<Assembly>();

        /// <summary>
        /// Adds another assembly to the factories assembly collection.
        /// </summary>
        /// <param name="assembly">provides the name of the assembly, or file name that needs to be loaded.</param>
        public static void AddAssembly(String assembly)
        {
            Assembly a = null;

            // See if a file exists first with this assembly name.
            // TODO - why does the following line cause an exception when a file doesn't exist?
            try
            {
                bool f = File.Exists(assembly);
                a = !File.Exists(assembly) ? Assembly.Load(new AssemblyName(assembly)) : Assembly.Load(assembly);
            }
            catch (Exception ex)
            {
                Log.Fatal(string.Format("{0}", ex.Message));
            }
            if (a == null)
                return;

            // Add the assembly to our assembly collection.
            assemblyCollection.Add(a);
        }

        /// <summary>
        /// Adds another assembly to the factories assembly collection.
        /// </summary>
        /// <param name="assembly">Provides a reference to the assembly that will be added to the collection.</param>
        public static void AddAssembly(Assembly assembly)
        {
            // Add the supplied assembly to our AssemblyCollection
            if (assembly != null)
                assemblyCollection.Add(assembly);
        }

        /// <summary>
        /// Gets an instance of the script specified.
        /// </summary>
        /// <param name="className">The script name you want to have an instance returned of.</param>
        /// <param name="args">Constructor arguments that the script requires.</param>
        /// <returns></returns>
        public static object GetScript(string className, params object[] args)
        {
            Type type = null;
            bool foundScript = false;

            foreach (Assembly assembly in assemblyCollection)
            {
                Type[] types = assembly.GetTypes();

                foreach (Type t in types)
                {
                    if (t.FullName == className)
                    {
                        type = t;
                        foundScript = true;
                        break;
                    }
                }

                if (foundScript)
                    break;
            }

            if (type == null)
                return null;

            object script = null;
            if (args == null || args.Length == 0)
            {
                // Only call Activator if a parameterless constructor exists.
                if (type.GetType().GetConstructors().All(c => c.GetParameters().Length == 0))
                    script = Activator.CreateInstance(type);
                else
                    Log.Error(string.Format("Tried to instance {0} with a parameterless constructor while one does not exist!", type.Name)); 
            }
            else
            {
                if (type.GetType().GetConstructors().All(c => c.GetParameters().Length > 0)) //This ensures we have a constructor that accepts a parameter
                    script = Activator.CreateInstance(type, args);
            }
            return script;
        }

        /// <summary>
        /// Finds a script that inherits from a base class. This does not search if the class implements any interfaces.
        /// </summary>
        /// <param name="baseScript">The base script that you want classes returned as a child of</param>
        /// <param name="arguments">Arguments that your script might require for the constructors</param>
        /// <returns></returns>
        public static Object FindInheritedScript(string baseScript, params Object[] arguments)
        {
            Type script = null;
            Boolean foundScript = false;

            if (assemblyCollection.Count == 0)
                return null;

            try
            {
                foreach (var a in assemblyCollection.Where(a => a != null))
                {
                    Type[] types = a.GetTypes();
                    foreach (Type t in types)
                    {
                        Type type = GetParentType(baseScript, t);
                        if (type != null) // If the returned object is not null, then 't' inherits.
                        {
                            foundScript = true;
                            script = t;
                            break;
                        }
                    }

                    if (foundScript)
                        break;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            try
            {
                if (script == null)
                    return null;

                Object obj = Activator.CreateInstance(script, arguments);
                return obj;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Searches and returns an array of scripts that inherit from a specified base class. This does not include interfaces.
        /// </summary>
        /// <param name="baseScript">The base script that you want to have an array of classes returned from.</param>
        /// <returns></returns>
        public static Type[] FindInheritedTypes(string baseScript)
        {
            List<Type> collection = new List<Type>();

            if (assemblyCollection.Count == 0)
                return null;

            try
            {
                foreach (var a in assemblyCollection.Where(a => a != null))
                {
                    Type[] types = a.GetTypes();
                    foreach (Type t in types)
                    {
                        Type type = GetParentType(baseScript, t);
                        if (type != null) // If the returned object is not null, then 't' inherits.
                            collection.Add(t); // This type inherits from baseScript, so add the object
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return collection.ToArray();
        }

        /// <summary>
        /// Searches and returns a Type for the child class specified
        /// </summary>
        /// <param name="baseScript"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        private static Type GetParentType(string baseScript, Type t)
        {
            if (t.BaseType != null)
            {
                if (t.BaseType.FullName == baseScript)
                    return t.BaseType;
                else
                {
                    if (t.BaseType.BaseType != null)
                        return GetParentType(baseScript, t.BaseType);
                }
            }

            return null;
        }

        /// <summary>
        /// Searches and returns an array of Types that implement a specified interface.
        /// </summary>
        /// <param name="interfaceName">The name of the interface</param>
        /// <returns></returns>
        public static Type[] GetTypesWithInterface(string interfaceName)
        {
            List<Type> collection = new List<Type>();

            if (assemblyCollection.Count == 0)
                return null;

            try
            {
                foreach (var a in assemblyCollection.Where(a => a != null))
                {
                    Type[] types = a.GetTypes();
                    foreach (Type t in types)
                    {
                        foreach (Type inter in t.GetInterfaces())
                        {
                            if (inter.Name == interfaceName && !t.IsAbstract)
                                collection.Add(t);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return collection.ToArray();
        }
    }
}
