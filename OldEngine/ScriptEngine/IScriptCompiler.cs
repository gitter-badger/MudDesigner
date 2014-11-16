//-----------------------------------------------------------------------
// <copyright file="IScriptCompiler.cs" company="AllocateThis!">
//     Copyright (c) AllocateThis! Studio's. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace MudEngine.Engine.Scripting
{
    /// <summary>
    /// The compiler interface that can be used to implement additional compiler to the engine
    /// </summary>
    public interface ICompiler
    {
        /// <summary>
        /// Gets or sets the results.
        /// </summary>
        /// <value>
        /// The results.
        /// </value>
        CompilerResults Results { get; set; }

        /// <summary>
        /// The file extension used for the script files.
        /// </summary>
        string ScriptExtension { get; set; }

        /// <summary>
        /// Provides a collection of Assemblies that the compiler will add to its reference list.
        /// </summary>
        List<string> AssemblyReferences { get; set; }

        /// <summary>
        /// Provides compiling options to various compilers, if they support this feature.
        /// </summary>
        Dictionary<string, string> CompilerOptions { get; set; }

        /// <summary>
        /// Compiles the source files found within the scriptRepository directory matching the ICompiler.ScriptExtension
        /// The Compiler defaults to the C# 4.0 compiler if none other is supplied via the ICompiler.CompilerOptions argument.
        /// </summary>
        /// <param name="param">Compiler Parameters that can be supplied to customize the compilation of the source.</param>
        /// <returns>Returns true if the compilation was completed without error.</returns>
        bool Compile(CompilerParameters param, string scriptRepository);

        /// <summary>
        /// Compiles the specified FileInfo object.
        /// </summary>
        /// <param name="param">The parameter.</param>
        /// <param name="scriptFile">The script file.</param>
        /// <returns></returns>
        bool Compile(CompilerParameters param, System.IO.FileInfo scriptFile);

        /// <summary>
        /// Compiles the raw source code.
        /// </summary>
        /// <param name="param">The parameter.</param>
        /// <param name="scriptSource">The script source.</param>
        /// <returns></returns>
        bool Compile(CompilerParameters param, string[] scriptSource);
    }
}