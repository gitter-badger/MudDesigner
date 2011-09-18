/*
 * rScripting Engine for .NET Framework
 * Website: http://rScript.Codeplex.com
 * 
 * Current Working Version: 1.3 
 *      - Change Log:
 *          * Compilers and Compile Engine only compile under the Windows Platform.
 *          * Late Binding Classes compile for both Windows & Windows Phone.
 *              Note: Windows Phone compiles the Late Binding classes, however use of the classes not tested.
 *                    Apps using the Late Binding classes might not pass certification at this time. (Un-tested)  
 *          * Restructed the engine class namespace paths.
 *
 * Future Versions:
 *      - TODO (Ordered by Priority)
 *          * Provide separate example projects for each supported language.
 *          * Add IronPython Compiler Wrapper
 *          * Validate the use of PC compiled script libraries running on Windows Phone 7.
 *          * Support the XNA Framework
 *              - Windows PC
 *              - Xbox 360
 *              - GameComponent Types
 *              - DrawableGameComponent Types
 *          * Add support for Script Styles.
 *              - Classless Scripts.
 *              - Namespace free Scripts.
 *              - Script Styles Interface for 3rd party extensions.
 *          * Add IronRuby Compiler Wrapper
 *          * Add F# Compiler Wrapper
 *          * Add client/server compiling (HTML Content sent to Server for compiling)
 *          
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
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Microsoft.CSharp;
using Microsoft.VisualBasic;

using rScripting.Compilers;
using rScripting.LateBinding;

namespace rScripting
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
            _CompileMessages = new String[] { "No compiler messages available." };

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
            //Get the compiler that the developer has selected.
            //If the developer chooses a compiler that is not part of the engine, the GetCompiler() method
            //will check all the currently loaded assemblies in memory for a custom compiler implementing
            //the ICompiler interface.
            Type compiler = GetCompiler();

            //Incase a non-default compiler was specified and we could not find it in memory, fail.
            if (compiler.Name == "ICompiler")
            {
                this._CompileMessages = new string[] { "Compilation Failed.", "Unable to locate the specified compiler of Type '" + Compiler + "'." };
                return false;
            }
            
            //Get the compiler parameters.
            CompilerParameters param = GetParameters();

            //Create a Instance of the compiler, either custom or internal.
            ICompiler com = (ICompiler)Activator.CreateInstance(compiler);

            //Setup it's properties to match that of our CompileEngine.
            com.AssemblyReferences = AssemblyReferences;
            com.ScriptExtension = ScriptExtension;
            com.CompilerOptions = this.CompilerOptions;

            //Compile the scripts.
            Boolean isSuccess = com.Compile(param, scriptRepository);
            HasErrors = !isSuccess;

            //If the compilation failed, store all of the compiler errors
            //into our _CompileMessages string.
            if (!isSuccess)
            {
                List<String> compilerMessages = new List<string>();
                foreach (String message in com.Results.Output)
                {
                    compilerMessages.Add(message);
                }

                _CompileMessages = compilerMessages.ToArray();
                return false;
            }
            else
            {
                //Compiling completed without error, so we need to save 
                //a reference to the compiled assembly.
                CompiledAssembly = com.Results.CompiledAssembly;
                return true;
            }
        }

        /// <summary>
        /// Compiles the script supplied.
        /// The compiler will compile the script using the compiler specified with the CompileEngine.Compiler Property.
        /// </summary>
        /// <returns>Returns true if compilation was completed without any errors.</returns>
        public Boolean Compile(FileInfo sourceFile)
        {
            if (!sourceFile.Exists)
            {
                this._CompileMessages = new String[] { "Error: File " + sourceFile.FullName + " does not exists." };
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
                this._CompileMessages = new string[] { "Compilation Failed.", "Unable to locate the specified compiler of Type '" + Compiler + "'." };
                return false;
            }

            //Get the compiler parameters.
            CompilerParameters param = GetParameters();

            //Create a Instance of the compiler, either custom or internal.
            ICompiler com = (ICompiler)Activator.CreateInstance(compiler);

            //Setup it's properties to match that of our CompileEngine.
            com.AssemblyReferences = AssemblyReferences;
            com.ScriptExtension = ScriptExtension;
            com.CompilerOptions = this.CompilerOptions;

            //Compile the script.
            Boolean isSuccess = com.Compile(param, sourceFile);
            HasErrors = !isSuccess;

            //If the compilation failed, store all of the compiler errors
            //into our _CompileMessages string.
            if (!isSuccess)
            {
                List<String> compilerMessages = new List<string>();
                foreach (String message in com.Results.Output)
                {
                    compilerMessages.Add(message);
                }

                _CompileMessages = compilerMessages.ToArray();
                return false;
            }
            else
            {
                //Compiling completed without error, so we need to save 
                //a reference to the compiled assembly.
                CompiledAssembly = com.Results.CompiledAssembly;
                return true;
            }
        }

        /// <summary>
        /// Compiles the source code provided.
        /// The compiler will compile the scripts using the compiler specified with the CompileEngine.Compiler Property.
        /// </summary>
        /// <returns>Returns true if compilation was completed without any errors.</returns>
        public Boolean Compile(String[] sourceCode)
        {
            //Get the compiler that the developer has selected.
            //If the developer chooses a compiler that is not part of the engine, the GetCompiler() method
            //will check all the currently loaded assemblies in memory for a custom compiler implementing
            //the ICompiler interface.
            Type compiler = GetCompiler();

            //Incase a non-default compiler was specified and we could not find it in memory, fail.
            if (compiler.Name == "ICompiler")
            {
                this._CompileMessages = new string[] { "Compilation Failed.", "Unable to locate the specified compiler of Type '" + Compiler + "'." };
                return false;
            }

            //Get the compiler parameters.
            CompilerParameters param = GetParameters();

            //Create a Instance of the compiler, either custom or internal.
            ICompiler com = (ICompiler)Activator.CreateInstance(compiler);

            //Setup it's properties to match that of our CompileEngine.
            com.AssemblyReferences = AssemblyReferences;
            com.ScriptExtension = ScriptExtension;
            com.CompilerOptions = this.CompilerOptions;

            //Compile the scripts.
            Boolean isSuccess = com.Compile(param, sourceCode);
            HasErrors = !isSuccess;

            //If the compilation failed, store all of the compiler errors
            //into our _CompileMessages string.
            if (!isSuccess)
            {
                List<String> compilerMessages = new List<string>();
                foreach (String message in com.Results.Output)
                {
                    compilerMessages.Add(message);
                }

                _CompileMessages = compilerMessages.ToArray();
                return false;
            }
            else
            {
                //Compiling completed without error, so we need to save 
                //a reference to the compiled assembly.
                CompiledAssembly = com.Results.CompiledAssembly;
                return true;
            }
        }

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
            if ((this.Compiler.ToLower() == "c#") || (this.Compiler.ToLower() == "csharp"))
            {
                compiler = typeof(Compilers.CSharp);
                return compiler;
            }
            else if ((this.Compiler.ToLower() == "vb") || (this.Compiler.Replace(" ", "").ToLower() == "visualbasic"))
            {
                compiler = typeof(Compilers.VisualBasic);
            }
            //Build a collection of available compilers by scanning all the assemblies loaded in memory.
            //If any of the assemblies contain a Type that uses the ICompiler interface, we will assume that the
            //assembly is a add-on assembly for rScript, adding a new compiler to the CompileEngine.
            //Only used if a non-internal compiler is specified
            else
            {   //Non-internal compiler supplied, so loop through every assembly loaded in memory
                foreach (Assembly a in _Assemblies)
                {
                    Boolean isCompiler = false;

                    //Create an array of all Types within this assembly
                    Type[] types = a.GetTypes();

                    //Itterate through each Type; See if any implement the ICompiler interface.
                    foreach (Type t in a.GetTypes())
                    {
                        //If this Type implements ICompiler, then our compiler field needs to reference the Type.
                        if ((t.GetInterface("ICompiler") != null) && (t.Name.ToLower() == Compiler.ToLower()))
                        {
                            //compiler needs to reference this custom compiler Type.
                            compiler = t;
                            isCompiler = true;
                            break;
                        }
                    }

                    //If we found a matching compiler, then exit this loop.
                    if (isCompiler)
                        break;
                }
            }


            return compiler;
        }
    }
}