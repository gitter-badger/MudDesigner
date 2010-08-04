﻿//Microsoft .NET Framework
using System;
using System.CodeDom;
using System.CodeDom.Compiler;
#if !MOBILE
using Microsoft.CSharp;
#endif
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Reflection;

using MudEngine.GameObjects;
using MudEngine.GameObjects.Characters;
using MudEngine.GameManagement;

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
        /// Collection of currently loaded objects created from compiled scripts
        /// </summary>
        public List<GameObject> GameObjects { get; private set; }

        /// <summary>
        /// Collection of currently loaded game commecnts that can be used. These must be compiled scripts inheriting from IGameCommand
        /// </summary>
        public List<IGameCommand> GameCommands { get; private set; }
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
        private List<Assembly> _AssemblyCollection;
        private string[] _ErrorMessages;
        Game _Game;

        public ScriptEngine(Game game) : this(game, ScriptTypes.Assembly)
        {
            _Game = game;
        }
        

        /// <summary>
        /// Instances a new copy of the script engine
        /// </summary>
        /// <param name="scriptTypes">Tells the engine what kind of scripts will be loaded. Source File or assembly based.</param>
        public ScriptEngine(Game game, ScriptTypes scriptTypes)
        {
            //Initialize our engine fields
            _ScriptTypes = scriptTypes;
            ScriptExtension = ".cs";

            //Get our current install path
            ScriptPath = Path.Combine(Environment.CurrentDirectory, "Scripts");
            InstallPath = Environment.CurrentDirectory;
            GameObjects = new List<GameObject>();
            _AssemblyCollection = new List<Assembly>();

            _Game = game;
        }

        /// <summary>
        /// Compiles a collection of scripts stored in ScriptEngine.ScriptPath. Not supported on XBox360.
        /// </summary>
        /// <returns></returns>
        public bool CompileScripts()
        {
#if !MOBILE
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
            string[] usingStatements = new string[] { "using System;", "using MudEngine.GameObjects;", "using MudEngine.GameObjects.Characters;", "using MudEngine.GameManagement;", "using MudEngine.FileSystem;" };

            foreach (string script in scripts)
            {
                string tempPath = "temp";
                string source = "\nnamespace MudScripts{\n}";

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
#endif
        }

        /// <summary>
        /// Initializes the script engine, loading the compiled scripts into memory
        /// </summary>
        /// <param name="scriptAssembly"></param>
        public void Initialize()
        {
            if (_ScriptTypes == ScriptTypes.Assembly)
            {
                Log.Write("Loading Assembly based scripts...");
                InitializeAssembly();
            }
            else
            {
                InitializeSourceFiles();
            }

            foreach (Assembly assembly in _AssemblyCollection)
            {
                Log.Write("Checking " + Path.GetFileName(assembly.Location) + " for scripts...");

                foreach (Type t in assembly.GetTypes())
                {
                    if (t.BaseType == null)
                        continue;
                    if (t.BaseType.Name == "BaseObject")
                    {
                        GameObjects.Add(new GameObject(Activator.CreateInstance(t, new object[] {_Game}), t.Name));
                        Log.Write(t.Name + " script loaded.");
                        continue;
                    }
                    else if (t.BaseType.Name == "BaseCharacter")
                    {
                        GameObject obj = new GameObject(Activator.CreateInstance(t, new object[] {_Game}), t.Name); 
                        GameObjects.Add(obj);
                        obj.GetProperty().CurrentRoom = _Game.InitialRealm.InitialZone.InitialRoom;
                        Log.Write(t.Name + " script loaded.");
                        continue;
                    }
                }
            }
        }

        private void InitializeAssembly()
        {
            if (!Directory.Exists(ScriptPath))
            {
                Log.Write("Supplied script path does not exist! No scripts loaded.");
                return;
            }
            string[] libraries = Directory.GetFiles(ScriptPath, "*.dll", SearchOption.AllDirectories);

            if (libraries.Length == 0)
            {
                Log.Write("No possible script libraries found.");
                return;
            }

            foreach (string library in libraries)
            {
                Log.Write("Found possible script libary: " + Path.GetFileName(library));
                _AssemblyCollection.Add(Assembly.LoadFile(library));
            }
            _AssemblyCollection.Add(Assembly.GetExecutingAssembly());
        }

        private void InitializeSourceFiles()
        {
            Log.Write("Source file scripts is not supported! Please change the script engine settings to load pre-compiled Assemblies!");
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
