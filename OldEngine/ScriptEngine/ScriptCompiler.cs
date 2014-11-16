//-----------------------------------------------------------------------
// <copyright file="ScriptCompiler.cs" company="AllocateThis!">
//     Copyright (c) AllocateThis! Studio's. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MudEngine.Engine.Scripting
{
    /// <summary>
    /// The compiler engine is used to compile user scripts so that they may be used by the game. The compiler currently only supports C# syntax.
    /// </summary>
    public static class CompileEngine
    {
        /// <summary>
        /// The script extension backing field.
        /// </summary>
        private static string scriptExtension;

        /// <summary>
        /// The assembly references backing field.
        /// </summary>
        private static List<string> assemblyReferences;

        /// <summary>
        /// The compiler backing field.
        /// </summary>
        private static string compiler;

        // Messages stored from the compilers CompilerResults property.
        private static string[] _CompileMessages = new string[] { "No compiler messages available." };

        /// <summary>
        /// Used to supply compiling options to various compilers if they support this feature.
        /// </summary>
        private static Dictionary<string, string> CompilerOptions = new Dictionary<string, string>();

        /// <summary>
        /// The file extension used for the script files.
        /// </summary>
        public static string ScriptExtension
        {
            get
            {
                if (string.IsNullOrEmpty(scriptExtension))
                { 
                    scriptExtension = ".cs";
                }

                return scriptExtension;
            }
            set
            {
                if (value.StartsWith("."))
                {
                    scriptExtension = value;
                }
                else
                { 
                    scriptExtension = "." + value;
                }
            }
        }

        /// <summary>
        /// Provides a collection of Assemblies that the compiler will add to its reference list.
        /// </summary>
        public static List<string> AssemblyReferences
        {
            get
            {
                if (assemblyReferences == null)
                {
                    assemblyReferences = new List<string>();
                    assemblyReferences.Add("mscorlib.dll");
                    assemblyReferences.Add("System.dll");
                    assemblyReferences.Add("System.Core.dll");
                    assemblyReferences.Add("MudDesigner.Engine.dll");
                    assemblyReferences.Add("log4net.dll");
                }

                return assemblyReferences;
            }
        }

        /// <summary>
        /// Provides a reference to the assembly generated during script compilation.
        /// </summary>
        public static Assembly CompiledAssembly { get; internal set; }

        /// <summary>
        /// The compiler that will be used when the contents of ScriptRepository are compiled.
        /// </summary>
        public static string Compiler
        {
            get
            {
                if (string.IsNullOrEmpty(compiler))
                { 
                    compiler = "C#";
                }

                return compiler;
            }
            set
            {
                compiler = value;
            }
        }


        /// <summary>
        /// Used to check if the compilation contained any errors.
        /// </summary>
        public static bool HasErrors { get; internal set; }

        /// <summary>
        /// string of errors that occurred during compilation, if any.
        /// </summary>
        public static string Errors
        {
            get
            {
                if (_CompileMessages.Length == 0)
                {
                    return "No compiler Errors found.";
                }
                else
                {
                    StringBuilder builder = new StringBuilder();
                    for (int x = 2; x != _CompileMessages.Length; x++)
                    {
                        builder.Append(_CompileMessages[x]);
                    }

                    return builder.ToString();
                }
            }
        }

        // Returns all of the assemblies currently loaded in the current domain.
        private static Assembly[] _Assemblies
        {
            get
            {
                return AppDomain.CurrentDomain.GetAssemblies();
            }
        }

        /// <summary>
        /// Adds a reference to the supplied Assembly name to the compilers reference collection.
        /// </summary>
        /// <param name="assembly"></param>
        public static void AddAssemblyReference(string assembly)
        {
            if (!AssemblyReferences.Contains(assembly))
            { 
                AssemblyReferences.Add(assembly);
            }
        }

        /// <summary>
        /// Adds a reference to the supplied Assembly to the compilers reference collection.
        /// </summary>
        /// <param name="assembly"></param>
        public static void AddAssemblyReference(Assembly assembly)
        {
            if (!AssemblyReferences.Contains(assembly.GetName().Name))
            { 
                AssemblyReferences.Add(assembly.GetName().Name);
            }
        }

        /// <summary>
        /// Removes the supplied assembly from the compilers reference collection.
        /// </summary>
        /// <param name="assembly"></param>
        public static void RemoveAssemblyReference(string assembly)
        {
            if (AssemblyReferences.Contains(assembly))
            { 
                AssemblyReferences.Remove(assembly);
            }
        }

        /// <summary>
        /// Clears the compilers reference collection, leaving it empty.
        /// </summary>
        public static void ClearAssemblyReference()
        {
            AssemblyReferences.Clear();
        }

        /// <summary>
        /// Compiles the scripts found within the CompileEngine.ScriptRepository directory that match the CompileEngine.ScriptExtension file extension.
        /// The compiler will compile the scripts using the compiler specified with the CompileEngine.Compiler Property.
        /// </summary>
        /// <returns>Returns true if compilation was completed without any errors.</returns>
        public static bool Compile(string scriptRepository)
        {
            // Get the compiler that the developer has selected.
            // If the developer chooses a compiler that is not part of the engine, the GetCompiler() method
            // will check all the currently loaded assemblies in memory for a custom compiler implementing
            // the ICompiler interface.
            Type compiler = GetCompiler();

            // Incase a non-default compiler was specified and we could not find it in memory, fail.
            if (compiler.Name == "ICompiler")
            {
                _CompileMessages = new[] { "Compilation Failed.", "Unable to locate the specified compiler of Type '" + Compiler + "'." };
                return false;
            }

            // Get the compiler parameters.
            CompilerParameters param = GetParameters();

            // Create a Instance of the compiler, either custom or internal.
            ICompiler com = (ICompiler)Activator.CreateInstance(compiler);

            // Setup it's properties to match that of our CompileEngine.
            com.AssemblyReferences = AssemblyReferences;
            com.ScriptExtension = ScriptExtension;
            com.CompilerOptions = CompilerOptions;

            // Compile the scripts.
            bool isSuccess = com.Compile(param, scriptRepository);
            HasErrors = !isSuccess;

            // If the compilation failed, store all of the compiler errors
            // into our _CompileMessages string.
            if (!isSuccess)
            {
                _CompileMessages = com.Results.Output.Cast<string>().ToArray();
                return false;
            }
            else
            {
                // Compiling completed without error, so we need to save 
                // a reference to the compiled assembly.
                CompiledAssembly = com.Results.CompiledAssembly;
                return true;
            }
        }

        /// <summary>
        /// Compiles the script supplied.
        /// The compiler will compile the script using the compiler specified with the CompileEngine.Compiler Property.
        /// </summary>
        /// <returns>Returns true if compilation was completed without any errors.</returns>
        public static bool Compile(FileInfo sourceFile)
        {
            if (!sourceFile.Exists)
            {
                _CompileMessages = new[] { "Error: File " + sourceFile.FullName + " does not exists." };
                return false;
            }

            // Get the compiler that the developer has selected.
            // If the developer chooses a compiler that is not part of the engine, the GetCompiler() method
            // will check all the currently loaded assemblies in memory for a custom compiler implementing
            // the ICompiler interface.
            Type compiler = GetCompiler();

            // Incase a non-default compiler was specified and we could not find it in memory, fail.
            if (compiler.Name == "ICompiler")
            {
                _CompileMessages = new[] { "Compilation Failed.", "Unable to locate the specified compiler of Type '" + Compiler + "'." };
                return false;
            }

            // Get the compiler parameters.
            CompilerParameters param = GetParameters();

            // Create a Instance of the compiler, either custom or internal.
            ICompiler com = (ICompiler)Activator.CreateInstance(compiler);

            // Setup it's properties to match that of our CompileEngine.
            com.AssemblyReferences = AssemblyReferences;
            com.ScriptExtension = ScriptExtension;
            com.CompilerOptions = CompilerOptions;

            // Compile the script.
            bool isSuccess = com.Compile(param, sourceFile);
            HasErrors = !isSuccess;

            // If the compilation failed, store all of the compiler errors
            // into our _CompileMessages string.
            if (!isSuccess)
            {
                _CompileMessages = com.Results.Output.Cast<string>().ToArray();
                return false;
            }
            else
            {
                // Compiling completed without error, so we need to save 
                // a reference to the compiled assembly.
                CompiledAssembly = com.Results.CompiledAssembly;
                return true;
            }

        }

        /// <summary>
        /// Compiles the source code provided.
        /// The compiler will compile the scripts using the compiler specified with the CompileEngine.Compiler Property.
        /// </summary>
        /// <returns>Returns true if compilation was completed without any errors.</returns>
        public static bool Compile(string[] sourceCode)
        {
            // Get the compiler that the developer has selected.
            // If the developer chooses a compiler that is not part of the engine, the GetCompiler() method
            // will check all the currently loaded assemblies in memory for a custom compiler implementing
            // the ICompiler interface.
            Type compiler = GetCompiler();

            // Incase a non-default compiler was specified and we could not find it in memory, fail.
            if (compiler.Name == "ICompiler")
            {
                _CompileMessages = new[] { "Compilation Failed.", "Unable to locate the specified compiler of Type '" + Compiler + "'." };
                return false;
            }

            // Get the compiler parameters.
            var param = GetParameters();

            // Create a Instance of the compiler, either custom or internal.
            var com = (ICompiler)Activator.CreateInstance(compiler);

            // Setup it's properties to match that of our CompileEngine.
            com.AssemblyReferences = AssemblyReferences;
            com.ScriptExtension = ScriptExtension;
            com.CompilerOptions = CompilerOptions;

            // Compile the scripts.
            var isSuccess = com.Compile(param, sourceCode);
            HasErrors = !isSuccess;

            // If the compilation failed, store all of the compiler errors
            // into our _CompileMessages string.
            if (!isSuccess)
            {
                _CompileMessages = com.Results.Output.Cast<string>().ToArray();
                return false;
            }
            else
            {
                // Compiling completed without error, so we need to save 
                // a reference to the compiled assembly.
                CompiledAssembly = com.Results.CompiledAssembly;
                return true;
            }
        }

        /// <summary>
        /// Gets compiler parameters that the compiler will be supplied with.
        /// </summary>
        /// <returns></returns>
        private static CompilerParameters GetParameters()
        {
            // Setup some default parameters that will be used by the compilers.
            CompilerParameters param = new CompilerParameters(AssemblyReferences.ToArray());
            param.GenerateExecutable = false;
            param.GenerateInMemory = true;
            param.OutputAssembly = "MudDesigner.Compiled.dll";

            // Left out, Add as CompileEngine properties in the future.
            // param.TreatWarningsAsErrors = true;
            // param.WarningLevel = 0;
            // param.IncludeDebugInformation = true;
            return param;
        }

        /// <summary>
        /// Gets the compiler that will be used during the compilation of the scripts.
        /// If a custom compiler is used, then the method will check every assembly in memory
        /// and find the custom one requested. If none are found, then it will return a new
        /// object of type ICompiler.
        /// </summary>
        /// <returns></returns>
        private static Type GetCompiler()
        {
            Type compiler = typeof(ICompiler);

            // Internal CSharpRaw compiler Type specified, so we'll use that.
            if ((Compiler.ToLower() == "c#") || (Compiler.ToLower() == "csharp"))
            {
                compiler = typeof(CSharp);
                return compiler;
            }
            else
            {

                // Build a collection of available compilers by scanning all the assemblies loaded in memory.
                // If any of the assemblies contain a Type that uses the ICompiler interface, we will assume that the
                // assembly is a add-on assembly for rScript, adding a new compiler to the CompileEngine.
                // Only used if a non-internal compiler is specified

                // Non-internal compiler supplied, so loop through every assembly loaded in memory
                foreach (var a in _Assemblies)
                {
                    var isCompiler = false;

                    // Create an array of all Types within this assembly
                    var types = a.GetTypes();

                    // Itterate through each Type; See if any implement the ICompiler interface.
                    foreach (var t in a.GetTypes().Where(t => (t.GetInterface("ICompiler") != null) && (t.Name.ToLower() == Compiler.ToLower())))
                    {
                        // compiler needs to reference this custom compiler Type.
                        compiler = t;
                        isCompiler = true;
                        break;
                    }

                    // If we found a matching compiler, then exit this loop.
                    if (isCompiler)
                    { 
                        break;
                    }
                }
            }

            return compiler;
        }
    }
}