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

namespace MudEngine.Scripting
{
    /// <summary>
    /// Standard C# source code compiler.
    /// </summary>
    public class MudScriptCompiler : rScripting.Compilers.ICompiler
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
            String[] baseScripts = Directory.GetFiles(scriptRepository, "*" + this.ScriptExtension, SearchOption.AllDirectories);
            String modifiedScriptsPath = "temp";
            
            //Setup the additional sourcecode that's needed in the script.
            String[] usingStatements = new String[] 
            { 
                "using System;", 
                "using System.Collections.Generic;", 
                "using System.Text;",
                "using System.Linq;",
                "using MudEngine.Commands;",
                "using MudEngine.GameObjects;", 
                "using MudEngine.GameObjects.Characters;", 
                "using MudEngine.GameObjects.Environment;", 
                "using MudEngine.GameObjects.Items;", 
                "using MudEngine.GameManagement;", 
                "using MudEngine.FileSystem;", 
                "using MudEngine.Scripting;" 
            };

            if (System.IO.Directory.Exists(modifiedScriptsPath))
                System.IO.Directory.Delete(modifiedScriptsPath, true);

            System.IO.Directory.CreateDirectory(modifiedScriptsPath);

            //Wrap the scripts around a namespace.
            foreach (String script in baseScripts)
            {
                String revisedScriptContent = "namespace MudScripts\n{\n\n\n}";
                FileStream input = new FileStream(script, FileMode.Open, FileAccess.Read, FileShare.None);
                FileStream output = new FileStream(Path.Combine(modifiedScriptsPath, Path.GetFileName(script)), FileMode.Create, FileAccess.Write);
                StreamReader reader = new StreamReader(input, System.Text.Encoding.Default);
                StreamWriter writer = new StreamWriter(output, System.Text.Encoding.Default);
                
                //Read the script into a private field for manipulation
                String scriptContent = reader.ReadToEnd();

                //No longer need the reader, as we now have the content that we need.
                reader.Close();
                
                //Insert using statements into the revised code section containing the scripts namespace
                foreach (String statement in usingStatements)
                    revisedScriptContent = revisedScriptContent.Insert(0, statement);

                //Insert the original script content into the revised content, now including the correct script header
                revisedScriptContent = revisedScriptContent.Insert(revisedScriptContent.Length - 1, scriptContent);
                writer.Write(revisedScriptContent);
                writer.Flush();
                writer.Close();
            }

            String[] ConvertedScripts = Directory.GetFiles("temp", "*" + this.ScriptExtension, SearchOption.AllDirectories);

            //Compile the scripts and provide the Results property with a reference to the compilation results.
            param.ReferencedAssemblies.Add("MudEngine.dll");
            Results = provider.CompileAssemblyFromFile(param, ConvertedScripts);
            System.IO.Directory.Delete(modifiedScriptsPath, true);

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
            //TODO: Add single-file compilation support
            return false; //Single file compiling not implemented. TODO!
            
            /**** Unreachable Code at the moment
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
            ***/
        }

        /// <summary>
        /// Compiles the source code found within the scriptSourceCode argument
        /// The Compiler defaults to the C# 4.0 compiler if none other is supplied via the ICompiler.CompilerOptions argument.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public Boolean Compile(CompilerParameters param, String[] scriptSourceCode)
        {
            //Source Code compiling not implemented.
            return false; //TODO: Add source code compiling support.

            /**** Unreachable code at the moment
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
            ****/
        }
    }
}