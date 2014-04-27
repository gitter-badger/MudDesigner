// Microsoft .NET Framework
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

// Mud Designer Framework
using Mud.Core;
using Mud.DataAccess;
using Mud.DataAccess.FileSystem;
using Mud.Models;
using Mud.Models.Environment;
using Mud.Networking;
using Mud.Scripting;

namespace Mud.Win32.Server
{
    class Program
    {
        static int Main(string[] args)
        {
            // Load all of the assemblies specified in the engine core settings.
            Console.WriteLine("Loading assemblies...");
            foreach (string assemblyToLoad in Mud.Engine.Default.ExternalLibraries)
            {
                string filePath = Path.Combine(Environment.CurrentDirectory, assemblyToLoad);

                // Make sure that the assembly specified in the settings exists before we try to load it.
                if (File.Exists(filePath))
                {
                    Assembly assembly = Assembly.LoadFile(filePath);
                    CompileEngine.AddAssemblyReference(filePath);
                    Console.WriteLine(assemblyToLoad + " loaded.");
                }
            }

            // Now that the pre-compiled assemblies are loaded, we can compile the scripts.
            // Always compile scripts after loading the pre-compiled assemblies. This allows the scripts to reference them.
            Console.WriteLine("Compiling scripts...");
            CompileEngine.ScriptExtension = Engine.Default.ScriptExtension;
            bool result = CompileEngine.Compile(Engine.Default.Scripts);

            if (!result)
            {
                Console.WriteLine(CompileEngine.Errors);
            }

            // Fetch all of the data storage context's that are available.
            var contexts = DataContextFactory.GetAvailableContext(false);

            // If zero contexts were found, we consider the start-up as a failure.
            // Data context's are required in order for the game to run.
            if (contexts.Length == 0)
            {
                Console.WriteLine("Error: Failed to find a valid DataContext device for data persistance.");
                Environment.Exit(-1);
            }

            // Instance a new default game and server.
            IGame game = new Game();
            IServer server = new Mud.Networking.Server(null);
            
            // Instance the first data context found in the array and fetch it's user friendly display name.
            IDataContext dataContext = Activator.CreateInstance(contexts.First()) as IDataContext;
            var names = dataContext.GetType().GetCustomAttributes(typeof(DisplayNameAttribute), true);

            // If no display names are found then we throw a warning message.
            // All objects that implement IDataContext should have a class-level DisplayNameAttribute assigned.
            if (names.Length == 0)
                Console.WriteLine("Warning: Could not determine the name of the DataContext '" + dataContext.GetType().Name + "' being used! A IDataContext object is being used without being fully set up.");
            else
            {
                // If we found a name, then we get it and tell the user what context is being used.
                DisplayNameAttribute name = names.First() as DisplayNameAttribute;
                Console.WriteLine("Set primary data context to {0} .", name.DisplayName);
            }

            // Initialize the game with our data context and server.
            // If no server is provided, then the game defaults to single player mode.
            if (game.Initialize(dataContext, server))
            {
                Console.WriteLine("Game initialization completed successfully.");
            }
            // Successfull initialization is required for the game to continue. If it failed, we exit.
            else
            {
                Console.WriteLine("Error: Failed to initialize the game. Start-up failed.");
                Environment.Exit(-2);
            }

            Realm realm = new Realm();
            realm.Name = "Test Realm";
            realm.Description = "This is just a test realm used to test data persistance.";
            realm.ID = 599;
            realm.IsEditable = true;
            realm.IsPermanent = true;
            realm.LastSaved = DateTime.Now;
            realm.World = game.World;

            game.DataContext.Save(realm);

            Console.WriteLine("Game now running.");
            while (game.IsRunning)
            {
                // TODO: Need to invoke a update method of some kind.
            }

            Console.ReadKey();

            return 0;
        }

        // private void ServerUpdates(
    }
}
