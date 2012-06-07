using System.Net.Sockets;
using System.Text;
using WinPC.Engine.Abstract.Core;
using WinPC.Engine.Commands;
using WinPC.Engine.Directors;

namespace WinPC.Engine.States
{
    public class MainMenuState : IState
    {

        public ServerDirector Director { get; private set; }
        
        private Socket connection { get; set; }
        private ASCIIEncoding encoding { get; set; }
        private IPlayer player { get; set; }

        public MainMenuState(ServerDirector director)
        {
            Director = director;
            encoding = new ASCIIEncoding();

        }
        public void Render(IPlayer connectedPlayer)
        {
            connection = connectedPlayer.Connection;
            player = connectedPlayer;

            connection.Send(encoding.GetBytes("Your now in the Main Menu State Welcome!! !" + "\n\r"));
        }

        public ICommand GetCommand()
        {
            var input = Director.RecieveInput(player);

            if(input == "connect")
            {
                return new SwitchStateCommand(Director, new ConnectState(Director),player);
            }
            return new InvalidCommand(connection);
        }
    }
}