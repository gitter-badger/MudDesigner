using System;
using System.Net.Sockets;
using System.Text;
using WinPC.Engine.Abstract.Core;
using WinPC.Engine.Commands;
using WinPC.Engine.Directors;

namespace WinPC.Engine.States
{
    public class ConnectState : IState
    {
        public ServerDirector Director { get; private set; }
        private Socket Connection { get; set; }
        private ASCIIEncoding Encoding { get; set; }
        private int Index;
        
        public ConnectState(ServerDirector director)
        {
            Director = director;
            Encoding = new ASCIIEncoding();

        }
        public void Render(int index)
        {
            Index = index;
            
            Connection = Director.ConnectedPlayers[index].Connection;

            Director.ConnectedPlayers[index].Connection.Send(Encoding.GetBytes("Welcome to Scionwest's Mud Engine!"+"\n\r"));
            Director.ConnectedPlayers[index].Connection.Send(Encoding.GetBytes("Please enter your name" + "\n\r"));

           
            
        }

        public ICommand GetCommand()
        {

            var input = Director.RecieveInput(Index);

            if (input == "menu")
            {
                return new SwitchStateCommand(Director, new MainMenuState(Director), Index);
            }

            return new InvalidCommand(Connection);
        }
    }
}