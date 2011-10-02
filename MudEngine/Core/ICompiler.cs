using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace MudEngine.Core
{
    /// <summary>
    /// Used to implement a wrapper for an existing compiler.
    /// </summary>
    public interface ICompiler
    {
        CompilerResults Results { get; set; }
        /// <summary>
        /// The file extension used for the script files.
        /// </summary>
        String ScriptExtension { get; set; }

        /// <summary>
        /// Provides a collection of Assemblies that the compiler will add to its reference list.
        /// </summary>
        List<String> AssemblyReferences { get; set; }

        /// <summary>
        /// Provides compiling options to various compilers, if they support this feature.
        /// </summary>
        Dictionary<String, String> CompilerOptions { get; set; }

        /// <summary>
        /// Compiles the source files found within the scriptRepository directory matching the ICompiler.ScriptExtension
        /// The Compiler defaults to the C# 4.0 compiler if none other is supplied via the ICompiler.CompilerOptions argument.
        /// </summary>
        /// <param name="param">Compiler Parameters that can be supplied to customize the compilation of the source.</param>
        /// <returns>Returns true if the compilation was completed without error.</returns>
        Boolean Compile(CompilerParameters param, String scriptRepository);

        Boolean Compile(CompilerParameters param, System.IO.FileInfo scriptFile);

        Boolean Compile(CompilerParameters param, String[] scriptSource);
    }
}