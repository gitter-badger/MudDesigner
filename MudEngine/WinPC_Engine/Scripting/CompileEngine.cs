using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
#if WINDOWS_PC
using Microsoft.CSharp;
#endif

using MudEngine.Core;

namespace MudEngine.Scripting
{
    /// <summary>
    /// Provides Properties & Methods needed to compile script source code into .NET assemblies.
    /// </summary>
    public class CompileEngine
    {
        /// <summary>
        /// The file extension used for the script files.
        /// </summary>
        public String ScriptExtension
        {
            get
            {
                return _ScriptExtension;
            }
            set
            {
                if (value.StartsWith("."))
                    _ScriptExtension = value;
                else
                    _ScriptExtension = "." + value;
            }
        }
        private String _ScriptExtension;

        /// <summary>
        /// Provides a collection of Assemblies that the compiler will add to its reference list.
        /// </summary>
        public List<String> AssemblyReferences { get; private set; }

        /// <summary>
        /// Provides a reference to the assembly generated during script compilation.
        /// </summary>
        public Assembly CompiledAssembly { get; set; }

        /// <summary>
        /// The compiler that will be used when the contents of ScriptRepository are compiled.
        /// </summary>
        public String Compiler { get; set; }

        /// <summary>
        /// Used to supply compiling options to various compilers if they support this feature.
        /// </summary>
        public Dictionary<String, String> CompilerOptions { get; set; }

        /// <summary>
        /// Used to check if the compilation contained any errors.
        /// </summary>
        public Boolean HasErrors { get; internal set; }

        /// <summary>
        /// String of errors that occurred during compilation, if any.
        /// </summary>
        public String Errors
        {
            get
            {
                if (_CompileMessages.Length == 0)
                    return "No Errors.";
                else
                {
                    StringBuilder builder = new StringBuilder();
                    foreach (String error in _CompileMessages)
                    {
                        builder.AppendLine(error);
                    }

                    return builder.ToString();
                }
            }
        }

        //Messages stored from the compilers CompilerResults property.
        private String[] _CompileMessages;

        //Returns all of the assemblies currently loaded in the current domain.
        private Assembly[] _Assemblies
        {
            get
            {
                return AppDomain.CurrentDomain.GetAssemblies();
            }
        }

        public CompileEngine() : this(".cs")
        {
            //Passes defaults off to the parameterized constructor.
        }

        public CompileEngine(String scriptExtensions)
        {
            _CompileMessages = new[] { "No compiler messages available." };

            CompilerOptions = new Dictionary<string, string>();
            Compiler = "C#";
            
            AssemblyReferences = new List<string>();
            AssemblyReferences.Add("mscorlib.dll");
            AssemblyReferences.Add("System.dll");
            AssemblyReferences.Add("System.Core.dll");

            ScriptExtension = scriptExtensions;
        }

        /// <summary>
        /// Adds a reference to the supplied Assembly name to the compilers reference collection.
        /// </summary>
        /// <param name="assembly"></param>
        public void AddAssemblyReference(String assembly)
        {
            if (!AssemblyReferences.Contains(assembly))
                AssemblyReferences.Add(assembly);
        }

        /// <summary>
        /// Adds a reference to the supplied Assembly to the compilers reference collection.
        /// </summary>
        /// <param name="assembly"></param>
        public void AddAssemblyReference(Assembly assembly)
        {
            if (!AssemblyReferences.Contains(assembly.GetName().Name))
                AssemblyReferences.Add(assembly.GetName().Name);
        }

        /// <summary>
        /// Removes the supplied assembly from the compilers reference collection.
        /// </summary>
        /// <param name="assembly"></param>
        public void RemoveAssemblyReference(String assembly)
        {
            if (AssemblyReferences.Contains(assembly))
                AssemblyReferences.Remove(assembly);
        }

        /// <summary>
        /// Clears the compilers reference collection, leaving it empty.
        /// </summary>
        public void ClearAssemblyReference()
        {
            AssemblyReferences.Clear();
        }

        /// <summary>
        /// Compiles the scripts found within the CompileEngine.ScriptRepository directory that match the CompileEngine.ScriptExtension file extension.
        /// The compiler will compile the scripts using the compiler specified with the CompileEngine.Compiler Property.
        /// </summary>
        /// <returns>Returns true if compilation was completed without any errors.</returns>
        public Boolean Compile(String scriptRepository)
        {
#if WINDOWS_PC
            //Get the compiler that the developer has selected.
            //If the developer chooses a compiler that is not part of the engine, the GetCompiler() method
            //will check all the currently loaded assemblies in memory for a custom compiler implementing
            //the ICompiler interface.
            Type compiler = GetCompiler();

            //Incase a non-default compiler was specified and we could not find it in memory, fail.
            if (compiler.Name == "ICompiler")
            {
                _CompileMessages = new[] { "Compilation Failed.", "Unable to locate the specified compiler of Type '" + Compiler + "'." };
                return false;
            }

            //Get the compiler parameters.
            CompilerParameters param = GetParameters();

            //Create a Instance of the compiler, either custom or internal.
            ICompiler com = (ICompiler)Activator.CreateInstance(compiler);

            //Setup it's properties to match that of our CompileEngine.
            com.AssemblyReferences = AssemblyReferences;
            com.ScriptExtension = ScriptExtension;
            com.CompilerOptions = CompilerOptions;

            //Compile the scripts.
            Boolean isSuccess = com.Compile(param, scriptRepository);
            HasErrors = !isSuccess;

            //If the compilation failed, store all of the compiler errors
            //into our _CompileMessages string.
            if (!isSuccess)
            {
                _CompileMessages = com.Results.Output.Cast<string>().ToArray();
                return false;
            }
            else
            {
                //Compiling completed without error, so we need to save 
                //a reference to the compiled assembly.
                CompiledAssembly = com.Results.CompiledAssembly;
                return true;
            }
#else
            return false;
#endif
        }

        /// <summary>
        /// Compiles the script supplied.
        /// The compiler will compile the script using the compiler specified with the CompileEngine.Compiler Property.
        /// </summary>
        /// <returns>Returns true if compilation was completed without any errors.</returns>
        public Boolean Compile(FileInfo sourceFile)
        {
#if WINDOWS_PC
            if (!sourceFile.Exists)
            {
                _CompileMessages = new[] { "Error: File " + sourceFile.FullName + " does not exists." };
                return false;
            }

            //Get the compiler that the developer has selected.
            //If the developer chooses a compiler that is not part of the engine, the GetCompiler() method
            //will check all the currently loaded assemblies in memory for a custom compiler implementing
            //the ICompiler interface.
            Type compiler = GetCompiler();

            //Incase a non-default compiler was specified and we could not find it in memory, fail.
            if (compiler.Name == "ICompiler")
            {
                _CompileMessages = new[] { "Compilation Failed.", "Unable to locate the specified compiler of Type '" + Compiler + "'." };
                return false;
            }

            //Get the compiler parameters.
            CompilerParameters param = GetParameters();

            //Create a Instance of the compiler, either custom or internal.
            ICompiler com = (ICompiler)Activator.CreateInstance(compiler);

            //Setup it's properties to match that of our CompileEngine.
            com.AssemblyReferences = AssemblyReferences;
            com.ScriptExtension = ScriptExtension;
            com.CompilerOptions = CompilerOptions;

            //Compile the script.
            Boolean isSuccess = com.Compile(param, sourceFile);
            HasErrors = !isSuccess;

            //If the compilation failed, store all of the compiler errors
            //into our _CompileMessages string.
            if (!isSuccess)
            {
                _CompileMessages = com.Results.Output.Cast<string>().ToArray();
                return false;
            }
            else
            {
                //Compiling completed without error, so we need to save 
                //a reference to the compiled assembly.
                CompiledAssembly = com.Results.CompiledAssembly;
                return true;
            }
#else
            return false;
#endif
        }

        /// <summary>
        /// Compiles the source code provided.
        /// The compiler will compile the scripts using the compiler specified with the CompileEngine.Compiler Property.
        /// </summary>
        /// <returns>Returns true if compilation was completed without any errors.</returns>
        public Boolean Compile(String[] sourceCode)
        {
#if WINDOWS_PC
            //Get the compiler that the developer has selected.
            //If the developer chooses a compiler that is not part of the engine, the GetCompiler() method
            //will check all the currently loaded assemblies in memory for a custom compiler implementing
            //the ICompiler interface.
            Type compiler = GetCompiler();

            //Incase a non-default compiler was specified and we could not find it in memory, fail.
            if (compiler.Name == "ICompiler")
            {
                _CompileMessages = new[] { "Compilation Failed.", "Unable to locate the specified compiler of Type '" + Compiler + "'." };
                return false;
            }

            //Get the compiler parameters.
            var param = GetParameters();

            //Create a Instance of the compiler, either custom or internal.
            var com = (ICompiler)Activator.CreateInstance(compiler);

            //Setup it's properties to match that of our CompileEngine.
            com.AssemblyReferences = AssemblyReferences;
            com.ScriptExtension = ScriptExtension;
            com.CompilerOptions = CompilerOptions;

            //Compile the scripts.
            var isSuccess = com.Compile(param, sourceCode);
            HasErrors = !isSuccess;

            //If the compilation failed, store all of the compiler errors
            //into our _CompileMessages string.
            if (!isSuccess)
            {
                _CompileMessages = com.Results.Output.Cast<string>().ToArray();
                return false;
            }
            else
            {
                //Compiling completed without error, so we need to save 
                //a reference to the compiled assembly.
                CompiledAssembly = com.Results.CompiledAssembly;
                return true;
            }
#else
            return false;
#endif
        }
#if WINDOWS_PC
        /// <summary>
        /// Gets compiler parameters that the compiler will be supplied with.
        /// </summary>
        /// <returns></returns>
        private CompilerParameters GetParameters()
        {
            //Setup some default parameters that will be used by the compilers.
            CompilerParameters param = new CompilerParameters(this.AssemblyReferences.ToArray());
            param.GenerateExecutable = false;
            param.GenerateInMemory = true;

            //Left out, Add as CompileEngine properties in the future.
            //param.TreatWarningsAsErrors = true;
            //param.WarningLevel = 0;
            //param.IncludeDebugInformation = true;
            return param;
        }

        /// <summary>
        /// Gets the compiler that will be used during the compilation of the scripts.
        /// If a custom compiler is used, then the method will check every assembly in memory
        /// and find the custom one requested. If none are found, then it will return a new
        /// Object of type ICompiler.
        /// </summary>
        /// <returns></returns>
        private Type GetCompiler()
        {
            Type compiler = typeof(ICompiler);

            //Internal CSharpRaw compiler Type specified, so we'll use that.
            if ((Compiler.ToLower() == "c#") || (Compiler.ToLower() == "csharp"))
            {
                compiler = typeof(CSharp);
                return compiler;
            }

            //Build a collection of available compilers by scanning all the assemblies loaded in memory.
            //If any of the assemblies contain a Type that uses the ICompiler interface, we will assume that the
            //assembly is a add-on assembly for rScript, adding a new compiler to the CompileEngine.
            //Only used if a non-internal compiler is specified
            else
            {   //Non-internal compiler supplied, so loop through every assembly loaded in memory
                foreach (var a in _Assemblies)
                {
                    var isCompiler = false;

                    //Create an array of all Types within this assembly
                    var types = a.GetTypes();

                    //Itterate through each Type; See if any implement the ICompiler interface.
                    foreach (var t in a.GetTypes().Where(t => (t.GetInterface("ICompiler") != null) && (t.Name.ToLower() == Compiler.ToLower())))
                    {
                        //compiler needs to reference this custom compiler Type.
                        compiler = t;
                        isCompiler = true;
                        break;
                    }

                    //If we found a matching compiler, then exit this loop.
                    if (isCompiler)
                        break;
                }
            }

            return compiler;
        }
#endif
    }
}