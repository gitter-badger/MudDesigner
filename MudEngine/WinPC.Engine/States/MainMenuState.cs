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
        private Socket Connection { get; set; }
        private ASCIIEncoding Encoding { get; set; }
        private int Index;

        public MainMenuState(ServerDirector director)
        {
            Director = director;
            Encoding = new ASCIIEncoding();

        }
        public void Render(int index)
        {
            Index = index;
            
            Connection = Director.ConnectedPlayers[index].Connection;

            Director.ConnectedPlayers[index].Connection.Send(Encoding.GetBytes("Your now in the Main Menu State Welcome!! !"+"\n\r"));

           
            
        }

        public ICommand GetCommand()
        {

            var input = Director.RecieveInput(Index);

            if(input == "connect")
            {
                return new SwitchStateCommand(Director, new ConnectState(Director),Index);
            }
            return new InvalidCommand(Connection);
        }
    }
}