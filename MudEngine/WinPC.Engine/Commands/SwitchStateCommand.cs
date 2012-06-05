using WinPC.Engine.Abstract.Core;
using WinPC.Engine.Directors;

namespace WinPC.Engine.Commands
{
    public class SwitchStateCommand : ICommand
    {
        private ServerDirector Director { get; set; }
        private IState NewState { get; set; }
        private int Index { get; set; }
        public SwitchStateCommand(ServerDirector director, IState newState, int index)
        {
            Director = director;
            NewState = newState;
        }

        public void Execute()
        {
            Director.ConnectedPlayers[Index].SwitchState(NewState);
        }
    }
}