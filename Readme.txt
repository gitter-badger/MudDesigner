* Introduction *
=======================================================================
Mud Designer Game Engine
Version: Alpha 1.3
Website: http://MudDesigner.Codeplex.com

Team:
Lead Developer: Scionwest
Networking: u8sand
Concept Ideas: genericperson


* About *
=======================================================================
The Mud Designer Game Engine is a complete MUD engine developed from
scratch using Microsofts C# and .NET Framework 4.0.
The project includes several components, including the game engine
library itself, a script compiler and a server/client executable.

The Mud Designer Game Engine comes with a complete script engine,
allowing developers to extend off the engine and build their own MUD's
without being constrained to our code only. The script engine allows
developers to build their game without ever needing to open the engine
source code itself.

The Engine supports custom game commands via scripts, allowing 
developers to write custom in-game commands that users can use, and
extend on the already existing commands if they want to.

The Engine supports dynamic object creation, allowing developers to
create their game world on the fly, while players are connected
and playing. Any object created in the game world becomes immediately
available for the connected players to use/traverse.
Note that this is still a work in progress feature.

The MudGame.exe is a custom Multiplayer Server/Singleplayer Client
built for the Mud Designer Engine. In either Multiplayer or Singleplayer
mode, the engine will load all of the Scripts found within the Scripts
folder, compile them and use them once the game is fully loaded.
Players can use the Settings.ini file to enable or disable the Server
mode. When server mode is disabled the game becomes a offline singleplayer
game.
Adding ServerEnabled=false to the Settings.ini file will force the game
to run in offline mode. Removing the property from the file, or setting
to true will enable the multiplayer server.

* Using The Game *
=======================================================================
Running the MudGame.exe in Singleplayer or Multiplayer will perform the
same. All of the same commands will be available for use, and the player
will interact with objects in the same manor.
When the Server mode is enabled, the console becomes read-only, printing
information related to the game to the server window. However, you can
not enter any commands to the server. You will need to connect to the
server via a telnet client (I recommend putty telnet) in order to execute
any server commands.
You may type 'Help' to recieve a list of available commands. Typing
Help CommandNameHere will print help for the specified command.

Running the MudCompiler.exe will allow you to compile your scripts without
having to start/stop the game/server all the time.

* Using the Scripts *
=======================================================================
You may add onto the game via the game scripts. Please review the 
included sample scripts to see how scripts are wrote.

If you wish to construct custom game commands, you MUST give your script
a name that starts with 'Command'. Example would be adding a attack command.
In game the user might enter 'Attack' but your script name (public class MyCommand)
MUST be called CommandAttack. The internal command engine performs corrections
to commands that assume their name begins with 'Command'.

* Using the Source Code *
=======================================================================
You will need to have Visual Studio 2010 or Visual C# Express 2010
installed along with .NET Framework 4.0 in order to compile the source
code.