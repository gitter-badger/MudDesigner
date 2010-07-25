//Microsoft .NET Framework
using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Reflection;

using MudEngine.GameObjects;

namespace MudEngine.Scripting
{
    public class ScriptEngine
    {
        public enum ScriptTypes
        {
            Assembly,
            SourceFiles
        }

        /// <summary>
        /// Path to the the script files directory
        /// </summary>
        public string ScriptPath { get; set; }
        public string InstallPath { get; private set; }
        public GameObjectCollection ObjectCollection { get; private set; }

        /// <summary>
        /// File Extension for the scripts
        /// </summary>
        public string ScriptExtension { get; set; }

        /// <summary>
        /// Error Messages logged during script compilation
        /// </summary>
        public string ErrorMessage 
        { 
            get
            {
                string errorMessages = "Script Compilation Failed!\n";
                //Construct our error message.
                foreach (string error in _ErrorMessages)
                    errorMessages += error + "\n";

                return errorMessages;
            }
            private set
            {
                _ErrorMessages = new string[] { value };
            }
        }

        private ScriptTypes _ScriptTypes;
        private Assembly _ScriptAssembly;
        private string[] _ErrorMessages;

        public ScriptEngine() : this(ScriptTypes.Assembly)
        {
            //Empty constructor. Only here for end-user ease of use. ScriptEngine(Game, ScriptTypes) is called from here.
        }
        

        /// <summary>
        /// Instances a new copy of the script engine
        /// </summary>
        /// <param name="scriptTypes">Tells the engine what kind of scripts will be loaded. Source File or assembly based.</param>
        public ScriptEngine(ScriptTypes scriptTypes)
        {
            //Initialize our engine fields
            _ScriptTypes = scriptTypes;
            ScriptExtension = ".cs";

            //Get our current install path
            ScriptPath = Environment.CurrentDirectory;
            InstallPath = Environment.CurrentDirectory;
        }

        /// <summary>
        /// Compiles a collection of scripts stored in ScriptEngine.ScriptPath. Not supported on XBox360.
        /// </summary>
        /// <returns></returns>
        public bool CompileScripts()
        {
            //Ensure the script path exists.
            if (!System.IO.Directory.Exists(ScriptPath))
            {
                ErrorMessage = "Invalid Script path supplied.";
                return false;
            }
            //Build an array of scripts
            string[] scripts = System.IO.Directory.GetFiles(ScriptPath, "*" + ScriptExtension, System.IO.SearchOption.AllDirectories);

            //Prepare the scripts. MUD Scripts are wrote without defining a namespace
            if (Directory.Exists("temp"))
                Directory.Delete("temp", true);

            Directory.CreateDirectory("temp");

            //Setup the additional sourcecode that's needed in the script.
            string[] usingStatements = new string[] { "using System;", "using MudEngine.GameObjects;", "using MudEngine.FileSystem;" };
            string source = "\nnamespace MudScripts{\n}";

            foreach (string script in scripts)
            {
                string tempPath = "temp";

                FileStream fr = new FileStream(script, FileMode.Open, FileAccess.Read, FileShare.None);
                FileStream fw = new FileStream(Path.Combine(tempPath, Path.GetFileName(script)), FileMode.Create, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fw, System.Text.Encoding.Default);
                StreamReader sr = new StreamReader(fr, System.Text.Encoding.Default);
                
                string content = sr.ReadToEnd();
                foreach (string statement in usingStatements)
                    source = source.Insert(0, statement);
                
                source = source.Insert(source.Length - 1, content);
                sw.Write(source);
                sr.Close();
                sw.Flush();
                sw.Close();
            }
            string oldPath = ScriptPath;
            ScriptPath = "temp";

            //Prepare the compiler.
            Dictionary<string, string> providerOptions = new Dictionary<string,string>();
            providerOptions.Add("CompilerVersion", "v3.5");

            CompilerParameters param = new CompilerParameters(new string[] {"mscorlib.dll", "System.dll", "MudEngine.dll"});
            param.GenerateExecutable = false;
            param.GenerateInMemory = true;
            param.OutputAssembly = "Scripts.dll";
            param.IncludeDebugInformation = false;
            param.TreatWarningsAsErrors = true;

            //Compile the scripts with the C# CodeProvider
            CSharpCodeProvider codeProvider = new CSharpCodeProvider(providerOptions);
            CompilerResults results = new CompilerResults(new TempFileCollection());
            scripts = Directory.GetFiles(ScriptPath, "*.Mud", SearchOption.AllDirectories);
            results = codeProvider.CompileAssemblyFromFile(param, scripts);

            //Delete the temp folder
            //Directory.Delete("temp", true);
            ScriptPath = oldPath;

            //if we encountered errors we need to log them to our ErrorMessages property
            if (results.Errors.Count >= 1)
            {
                List<string> errorCollection = new List<string>();
                foreach (CompilerError error in results.Errors)
                {
                    string prefix = "Error: ";
                    if (error.IsWarning)
                        prefix = "Warning: ";

                    errorCollection.Add(prefix + error.FileName + "(" + error.Line + ") - " + error.ErrorText);
                    _ErrorMessages = errorCollection.ToArray();
                }
                return false;
            }
            else
                return true;
        }

        /// <summary>
        /// Initializes the script engine, loading the compiled scripts into memory
        /// </summary>
        /// <param name="scriptAssembly"></param>
        public void Initialize()
        {
            if (_ScriptTypes == ScriptTypes.Assembly)
            {
                InitializeAssembly();
            }
            else
            {
                InitializeSourceFiles();
            }
        }

        private void InitializeAssembly()
        {
            if (!System.IO.File.Exists("Scripts.dll"))
            {
                ErrorMessage = "Failed to load Script Assembly!";
                return;
            }

            _ScriptAssembly = Assembly.LoadFile(Path.Combine(InstallPath, "Scripts.dll"));

            foreach (Type type in _ScriptAssembly.GetTypes())
            {
                //TODO: Re-implement StartupObject instancing only during Initialize calls.
                //Remaining scripts should be instanced via a ScriptEngine.LoadObjectList() method.
                //if (type.BaseType == typeof(StartupObject))
                //{
                    GameObject gameObject = new GameObject();
                    gameObject.Instance = Activator.CreateInstance(type);
                    gameObject.Name = type.Name;

                    ObjectCollection._GameObjects.Add(gameObject);
                //}
            }
        }

        private void InitializeSourceFiles()
        {
        }

        public GameObject GetObject(string objectName)
        {
            IEnumerable<GameObject> objectQuery =
                from gameObject in ObjectCollection._GameObjects
                where gameObject.Name == objectName
                select gameObject;

            foreach (GameObject gameObject in objectQuery)
            {
                if (gameObject.Name == objectName)
                    return gameObject;
            }

            return null;
        }
    }
}
