using System;
using System.Net.Sockets;
using System.Text;
using WinPC.Engine.Abstract.Core;
using WinPC.Engine.Commands;
using WinPC.Engine.Directors;
using WinPC.Engine.Core;

namespace WinPC.Engine.States
{
    public class LoginState : IState
    {
        public ServerDirector Director { get; private set; }

        private Socket connection;
        private ASCIIEncoding encoding;
        private IPlayer player;

        public LoginState(ServerDirector director)
        {
            Director = director;
            encoding = new ASCIIEncoding();

        }
        public void Render(IPlayer connectedPlayer)
        {
            connection = connectedPlayer.Connection;
            player = connectedPlayer;

            player.SendMessage("Enter your name: ");
        }

        public ICommand GetCommand()
        {
            var input = Director.RecieveInput(player);

            //Since player login state is the first location were we receive input from the player, we need to strip
            //out the telnet clients header information.  Unfortunately this method scans and pulls the first full word
            //that it finds.  Until another method can be found to trim the junk out of the stream, only single word names
            //can be supported for characters.
            System.Text.RegularExpressions.Match m = System.Text.RegularExpressions.Regex.Match(input, @"\w+");
            input = m.Value;

            //If no user name is supplied, re-try.
            if (String.IsNullOrEmpty(input))
            {
                player.SendMessage("Invalid username.");
                player.SendMessage("Enter your name: ");
            }
            else
            {
                //Check if player exists.  Should check a database of some kind...
                string filename = System.IO.Path.Combine(input, ".player");
                if (System.IO.File.Exists(System.IO.Path.Combine("Players\\", filename)))
                {
                    return new SwitchStateCommand(Director, new LoginExistingUserState(Director), player);
                }
                else //If no user exists, then we create a new player.
                {
                    return new SwitchStateCommand(Director, new CreatePlayerState(Director), player);
                }
            }

            return new InvalidCommand(connection);
        }
    }
}