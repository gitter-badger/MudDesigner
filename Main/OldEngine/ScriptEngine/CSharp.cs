//-----------------------------------------------------------------------
// <copyright file="CSharp.cs" company="AllocateThis!">
//     Copyright (c) AllocateThis! Studio's. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using Microsoft.CSharp;

namespace MudEngine.Engine.Scripting
{
    /// <summary>
    /// The C# Compiler used by the ScriptCompiler
    /// </summary>
    internal class CSharp : ICompiler
    {
        /// <summary>
        /// The file extension used for the script files.
        /// </summary>
        public string ScriptExtension { get; set; }

        /// <summary>
        /// Provides a collection of Assemblies that the compiler will add to its reference list.
        /// </summary>
        public List<string> AssemblyReferences { get; set; }

        /// <summary>
        /// The results of the compilation
        /// </summary>
        public CompilerResults Results { get; set; }

        /// <summary>
        /// Provides compiling options to various compilers, if they support this feature.
        /// </summary>
        public Dictionary<string, string> CompilerOptions { get; set; }

        /// <summary>
        /// Compiles the source files found within the scriptRepository directory matching the ICompiler.ScriptExtension
        /// The Compiler defaults to the C# 4.0 compiler if none other is supplied via the ICompiler.CompilerOptions argument.
        /// </summary>
        /// <param name="param">Compiler Parameters that can be supplied to customize the compilation of the source.</param>
        /// <returns>Returns true if the compilation was completed without error.</returns>
        public bool Compile(CompilerParameters param, string scriptRepository)
        {
            // Make sure we have a compiler version supplied.
            if (!this.CompilerOptions.ContainsKey("CompilerVersion"))
            { 
                this.CompilerOptions.Add("CompilerVersion", "v4.0");
            }

            // Instance a reference to the C# code provider, this is what will perform the compiling.
            CSharpCodeProvider provider = new CSharpCodeProvider(this.CompilerOptions);

            // Create an array of script files found within the ScriptRepository matching the ScriptExtension properties.
            if (!Directory.Exists(scriptRepository))
            { 
                Directory.CreateDirectory(scriptRepository);
            }

            string[] scripts = Directory.GetFiles(scriptRepository, "*" + this.ScriptExtension, SearchOption.AllDirectories);

            if (scripts.Length > 0)
            {
                // Compile the scripts and provide the Results property with a reference to the compilation results.
                this.Results = provider.CompileAssemblyFromFile(param, scripts);

                // if the compiler has errors, return false.
                if (this.Results.Errors.HasErrors)
                {
                    return false;
                }
                else
                { 
                    return true;
                }
            }
            else
            {
                this.Results = new CompilerResults(new TempFileCollection());
                return false;
            }
        }

        /// <summary>
        /// Compiles the source files found within the scriptFile argument.
        /// The Compiler defaults to the C# 4.0 compiler if none other is supplied via the ICompiler.CompilerOptions argument.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public bool Compile(CompilerParameters param, FileInfo scriptFile)
        {
            // Make sure we have a compiler version supplied.
            if (!this.CompilerOptions.ContainsKey("CompilerVersion"))
            { 
                this.CompilerOptions.Add("CompilerVersion", "v4.0");
            }

            CSharpCodeProvider provider = new CSharpCodeProvider(this.CompilerOptions);

            // Make sure the file exists prior to attempting to compile it.
            if (scriptFile.Exists)
            {
                // Compile the script and provide the Results property with a referece to the compilation results.
                this.Results = provider.CompileAssemblyFromFile(param, scriptFile.FullName);
            }
            else
            {
                this.Results.Errors.Add(new CompilerError(scriptFile.FullName, 0, 0, "rS01", "The supplied filename does not exist."));
                return false;
            }

            if (this.Results.Errors.HasErrors)
            {
                return false;
            }
            else
            { 
                return true;
            }
        }

        /// <summary>
        /// Compiles the source code found within the scriptSourceCode argument
        /// The Compiler defaults to the C# 4.0 compiler if none other is supplied via the ICompiler.CompilerOptions argument.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public bool Compile(CompilerParameters param, string[] scriptSourceCode)
        {
            if (!this.CompilerOptions.ContainsKey("CompilerVersion"))
            {
                this.CompilerOptions.Add("CompilerVersion", "v4.0");
            }

            CSharpCodeProvider provider = new CSharpCodeProvider(this.CompilerOptions);

            if (scriptSourceCode.Length == 0)
            {
                this.Results.Errors.Add(new CompilerError("None", 0, 0, "rS02", "No Source provided."));
                return false;
            }
            else
            {
                this.Results = provider.CompileAssemblyFromSource(param, scriptSourceCode);
            }

            if (this.Results.Errors.HasErrors)
            {
                return false;
            }
            else
            { 
                return true;
            }
        }
    }
}