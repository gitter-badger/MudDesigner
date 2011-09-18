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
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using Microsoft.CSharp;

namespace rScripting.Compilers
{
    /// <summary>
    /// Standard C# source code compiler.
    /// </summary>
    internal class CSharp : ICompiler
    {
        /// <summary>
        /// The file extension used for the script files.
        /// </summary>
        public String ScriptExtension { get; set; }

        /// <summary>
        /// Provides a collection of Assemblies that the compiler will add to its reference list.
        /// </summary>
        public List<String> AssemblyReferences { get; set; }

        /// <summary>
        /// The results of the compilation
        /// </summary>
        public CompilerResults Results { get; set; }

        /// <summary>
        /// Provides compiling options to various compilers, if they support this feature.
        /// </summary>
        public Dictionary<String, String> CompilerOptions { get; set; }

        /// <summary>
        /// Compiles the source files found within the scriptRepository directory matching the ICompiler.ScriptExtension
        /// The Compiler defaults to the C# 4.0 compiler if none other is supplied via the ICompiler.CompilerOptions argument.
        /// </summary>
        /// <param name="param">Compiler Parameters that can be supplied to customize the compilation of the source.</param>
        /// <returns>Returns true if the compilation was completed without error.</returns>
        public Boolean Compile(CompilerParameters param, String scriptRepository)
        {
            //Make sure we have a compiler version supplied.
            if (!CompilerOptions.ContainsKey("CompilerVersion"))
                CompilerOptions.Add("CompilerVersion", "v4.0");

            //Instance a reference to the C# code provider, this is what will perform the compiling.
            CSharpCodeProvider provider = new CSharpCodeProvider(CompilerOptions);
            //Create an array of script files found within the ScriptRepository matching the ScriptExtension properties.
            String[] scripts = Directory.GetFiles(scriptRepository, "*" + this.ScriptExtension, SearchOption.AllDirectories);

            //Compile the scripts and provide the Results property with a reference to the compilation results.
            Results = provider.CompileAssemblyFromFile(param, scripts);

            //if the compiler has errors, return false.
            if (Results.Errors.HasErrors)
                return false;
            else
                return true;
        }

        /// <summary>
        /// Compiles the source files found within the scriptFile argument.
        /// The Compiler defaults to the C# 4.0 compiler if none other is supplied via the ICompiler.CompilerOptions argument.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public Boolean Compile(CompilerParameters param, FileInfo scriptFile)
        {
            //Make sure we have a compiler version supplied.
            if (!CompilerOptions.ContainsKey("CompilerVersion"))
                CompilerOptions.Add("CompilerVersion", "v4.0");

            CSharpCodeProvider provider = new CSharpCodeProvider(CompilerOptions);

            //Make sure the file exists prior to attempting to compile it.
            if (scriptFile.Exists)
            {
                //Compile the script and provide the Results property with a referece to the compilation results.
                Results = provider.CompileAssemblyFromFile(param, scriptFile.FullName);
            }
            else
            {
                Results.Errors.Add(new CompilerError(scriptFile.FullName, 0, 0, "rS01", "The supplied filename does not exist."));
                return false;
            }

            if (Results.Errors.HasErrors)
                return false;
            else
                return true;
        }

        /// <summary>
        /// Compiles the source code found within the scriptSourceCode argument
        /// The Compiler defaults to the C# 4.0 compiler if none other is supplied via the ICompiler.CompilerOptions argument.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public Boolean Compile(CompilerParameters param, String[] scriptSourceCode)
        {
            if (!CompilerOptions.ContainsKey("CompilerVersion"))
                CompilerOptions.Add("CompilerVersion", "v4.0");

            CSharpCodeProvider provider = new CSharpCodeProvider(CompilerOptions);

            if (scriptSourceCode.Length == 0)
            {
                Results.Errors.Add(new CompilerError("None", 0, 0, "rS02", "No Source provided."));
                return false;
            }
            else
            {
                Results = provider.CompileAssemblyFromSource(param, scriptSourceCode);
            }

            if (Results.Errors.HasErrors)
                return false;
            else
                return true;
        }
    }
}