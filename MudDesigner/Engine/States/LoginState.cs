using System;
using System.Net.Sockets;
using System.Text;
using MudDesigner.Engine.Core;
using MudDesigner.Engine.Commands;
using MudDesigner.Engine.Directors;

using MudDesigner.Engine.Mobs;
namespace MudDesigner.Engine.States
{
    public class LoginState : IState
    {
        public ServerDirector Director { get; private set; }
        private int IsLoggingIn { get; set; }
        private Socket connection;
        private ASCIIEncoding encoding;
        private IPlayer _player; 

        public LoginState(ServerDirector director)
        {
            Director = director;
            encoding = new ASCIIEncoding();
            IsLoggingIn = 0; // 0 = just landed on the page, 1 = entered username & password, 2 = wants to register a new username & password, 3 = Currently Registering.
        }

        public void Render(IPlayer connectedPlayer)
        {
            connection = connectedPlayer.Connection;
            _player = connectedPlayer;

            switch (IsLoggingIn)
            {
                case -1: // We just logged in Let's see if we can fix the junk issue.
                    {
                        _player.SendMessage("Let's Do Some Debuggign Yo! . "+"\n\r");
                        break;
                    }
                case 0:
                    {
                        _player.SendMessage("Please enter your username and password (username:password) to login, or type /register to register a new account. "+ "\n\r");
                        break;
                    }
                case 1:
                    {
                        _player.SendMessage("Authenticating..." + "\n\r");
                        break;
                    }
                case 2:
                    {
                        _player.SendMessage("Welcome to the Server!, please enter a username and password that you would like. ex: (username:password) " + "\n\r");
                        break;
                        
                    }
            }
            
            
        }

        public ICommand GetCommand()
        {
            var input = Director.RecieveInput(_player);

            //Since player login state is the first location were we receive input from the player, we need to strip
            //out the telnet clients header information.  Unfortunately this method scans and pulls the first full word
            //that it finds.  Until another method can be found to trim the junk out of the stream, only single word names
            //can be supported for characters.
            
            //System.Text.RegularExpressions.Match m = System.Text.RegularExpressions.Regex.Match(input, @"\w+");
            //TODO Scion can you give me how you are reproducing the garbage string on input, I wasn't able to send garblygook as input.
            //TODO - I use PuttyTel and it sends the junk.  The header junk is client specific - JS.

            if (string.IsNullOrWhiteSpace(input))
                return new InvalidCommand(_player.Connection);  // FYI we can turn this into InvalidLogin or something.


            switch (IsLoggingIn)
            {
                case 0:
                    {
                        if(input[0] == '/')
                        {
                            var parts = input.Substring(1).Split(' ');
                            if (parts.Length == 1)
                            {
                                if (parts[0] == "register") 
                                {
                                    IsLoggingIn = 2; // Set that we want to register.    
                                    return  new NoOpCommand(_player.Connection);
                                }
                                else
                                {
                                    //  We don't allow any other commands currently, we can introduce admin bypass if we want but i don't recommend it.
                                    return new InvalidCommand(_player.Connection);
                                }

                            }
                            else
                            {
                                //  We don't allow any other commands currently
                                return new InvalidCommand(_player.Connection);
                            }
                        }
                        else
                        {
                            var  parts = input.Substring(0).Split(':');
                            return parts.Length == 2 ? new LoginFailureCommand(_player.Connection) : new LoginFailureCommand(_player.Connection);
                        }
                        }
                case 1:
                    {
                        break;
                    }
                case 2:
                    {
                        var parts = input.Substring(0).Split(':');
                        if(parts.Length == 2)
                        {
                            var player = _player as IPlayer;
                            if (player != null)
                            {
                                player.Username = parts[0];
                                player.Password = parts[1];

                                
                                return new SwitchStateCommand(Director, new CreatePlayerState(Director, _player), _player); // @ToDO  we are passing Director & _player twice on SwitchState we need to see about making it more efficient.
                            }
                        }
                        else
                        {
                            return new InvalidCommand(_player.Connection); // they either did usernamepassword or username:password: or something like that...
                        }
                        
                        break;
                    }
          
                default:
                    {
                        return new LoginFailureCommand(_player.Connection);  // by default we error out 
                    }

                        
                    }
            
            
            // just re tell the user what to do.. and tell them what they entered was invalid
      
            /*
            //If no user name is supplied, re-try.
            if (String.IsNullOrEmpty(input))
            {
                _player.SendMessage("Invalid username.");
                _player.SendMessage("Enter your name: ");
            }
            else
            {
                //Check if player exists.  Should check a database of some kind...
                string filename = System.IO.Path.Combine(input, ".player");
                if (System.IO.File.Exists(System.IO.Path.Combine("Players\\", filename)))
                {
                    return new SwitchStateCommand(Director, new LoginExistingUserState(Director), _player);
                }
                else //If no user exists, then we create a new player.
                {
                    return new SwitchStateCommand(Director, new CreatePlayerState(Director,_player), _player);
                }
            } */

            return new InvalidCommand(connection);
        }
    }
}