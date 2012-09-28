using System.Net.Sockets;
using System.Text;
using WinPC.Engine.Abstract.Core;
using WinPC.Engine.Commands;
using WinPC.Engine.Core;
using WinPC.Engine.Directors;

namespace WinPC.Engine.States
{
    public class MainMenuState : IState
    {

        public ServerDirector Director { get; private set; }
        
        private Socket connection { get; set; }
        private ASCIIEncoding encoding { get; set; }
        private IPlayer Player { get; set; }

        public MainMenuState(ServerDirector director)
        {
            Director = director;
            encoding = new ASCIIEncoding();

        }
        public void Render(IPlayer connectedPlayer)
        {
            connection = connectedPlayer.Connection;
            Player = connectedPlayer;

            var player = Player as Player;
            if(player != null)
            {
                connection.Send(encoding.GetBytes(string.Format("Welcome {0}, What do you want to do? {1}",player.CharacterName,"\n\r")));    
            }
            else
            {
                connection.Send(encoding.GetBytes(string.Format("Something seriously wrong happened... What did you do!!!")));    
            }


            // Some Fancy Menu
            connection.Send(encoding.GetBytes("-----------------------------------------\n\r"));
            //connection.Send(encoding.GetBytes(string.Format("|{0}|\n\r", Director.Server.Game.Name))); // @ToDo: I'll look into Text Centering stuff.
            connection.Send(encoding.GetBytes("-----------------------------------------\n\r"));
            connection.Send(encoding.GetBytes("| [Enter] a town                        |\n\r"));
            connection.Send(encoding.GetBytes("| [Join] a chat channel                 |\n\r"));
            connection.Send(encoding.GetBytes("| [Save] my current player              |\n\r"));
            connection.Send(encoding.GetBytes("| Change some game [Options]            |\n\r"));
            connection.Send(encoding.GetBytes("| [Quit] the game                       |\n\r"));
            connection.Send(encoding.GetBytes("-----------------------------------------\n\r")); 



        }

        public ICommand GetCommand()
        {
            var input = Director.RecieveInput(Player);

            // We Don't have any commands here yet... but we will! (EnterCommand, JoinCommand, SaveCommand, OptionsCommand, QuitCommand etc)
            return new InvalidCommand(connection);
        }
    }
}