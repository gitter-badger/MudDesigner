using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Reflection;
using System.Text;

using MudEngine.Core;
using MudEngine.Runtime;
using MudEngine.Communication;
using MudGame.Characters;

namespace MudGame
{
    public class MudGame : BaseGame
    {
        /// <summary>
        /// Gets a reference to the game scripting system
        /// </summary>
        protected ScriptSystem ScriptSystem { get; private set; }

        /// <summary>
        /// Gets a reference to the games command execution system
        /// </summary>
        protected CommandSystem CommandSystem { get; private set; }

        public MudGame()
            : base()
        {
            ScriptSystem = new ScriptSystem(".mud");
            CommandSystem = new CommandSystem();
            Server = new Server(this);
        }

        public override void Initialize()
        {
            //Setup the example game.
            this.Name = "Mud Example Game";
            this.Description = "An simple Mud Game class that manages the basics";
            this.AutoSaveInterval = 5;
            this.EnableAutoSave = true;
            this.PasswordMinimumSize = 8;
            this.Version = "Alpha 2.0";

            //Add the engine.dll.
            string assembly = Assembly.GetExecutingAssembly().GetName().Name + ".dll";
            this.ScriptSystem.AddAssemblyReference(assembly);

            //If a scripts directory exists, compile them.
            if (Directory.Exists("Scripts"))
            {
                this.ScriptSystem.Compile("Scripts");
               // if (this.ScriptSystem.HasErrors)
                    //TODO: Output script system compile errors
                    //return; //temp.  Shouldn't return.
            }

            //Scripts are compiled, now load all of the commands, if any script commands exist.
            CommandSystem.LoadCommandLibrary(Assembly.GetExecutingAssembly());
            CommandSystem.LoadCommandLibrary(ScriptSystem.CompiledAssembly);

            //TODO: Initialize the game world.

            //TODO: Load previously saved state.

            //TODO: Enable server.
            if (this.EnableServer)
                this.Server.Initialize();

            this.IsRunning = true;
        }

        public override void Shutdown()
        {
            if (Server.IsRunning)
                this.EnableServer = false;

            this.Save();
        }

        public override void Update()
        {
            if (this.Server.IsRunning)
                this.Server.Update();
        }

        public override void OnConnect(System.Net.Sockets.TcpClient client)
        {
            MudCharacter character = new MudCharacter(this);
            character.Role = CharacterRoles.Player;
            character.OnConnect(client);
        }

        public override void OnDisconnect(System.Net.Sockets.TcpClient client)
        {
            throw new NotImplementedException();
        }

        public override int GetAvailableID()
        {
            return 1;
        }
    }
}
