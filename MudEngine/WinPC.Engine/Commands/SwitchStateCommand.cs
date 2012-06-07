using WinPC.Engine.Abstract.Core;
using WinPC.Engine.Directors;

namespace WinPC.Engine.Commands
{
    public class SwitchStateCommand : ICommand
    {
        private ServerDirector Director { get; set; }
        private IState NewState { get; set; }
        private IPlayer player { get; set; }

        public SwitchStateCommand(ServerDirector director, IState newState, IPlayer connectedPlayer)
        {
            Director = director;
            NewState = newState;
            player = connectedPlayer;
        }

        public void Execute()
        {
            player.SwitchState(NewState);
        }
    }
}