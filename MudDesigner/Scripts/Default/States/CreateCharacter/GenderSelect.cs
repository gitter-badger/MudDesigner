using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MudDesigner.Engine.Core;
using MudDesigner.Engine.Commands;
using MudDesigner.Engine.Directors;
using MudDesigner.Engine.Mobs;
using MudDesigner.Engine.Properties;
using MudDesigner.Engine.Scripting;
using MudDesigner.Engine.States;

namespace MudDesigner.Scripts.Default.States.CreateCharacter
{
    public class GenderSelect : IState
    {
        private ServerDirector director;
        private IPlayer connectedPlayer;

        public GenderSelect(ServerDirector serverDirector)
        {
            director = serverDirector;
        }

        public void Render(IPlayer player)
        {
            connectedPlayer = player;

            player.SendMessage("Are you a Male or a Female? ", false);
        }

        public ICommand GetCommand()
        {
            var input = connectedPlayer.RecieveInput().ToLower().Trim();

            if (string.IsNullOrEmpty(input))
            {
                connectedPlayer.SendMessage("Invalid selection, you must choose either a Male or a Female");
                return new NoOpCommand();
            }

            Type[] genders = ScriptFactory.GetTypesWithInterface("IGender");

            //Fail safe in the event gender scripts are deleted.
            if (genders.Length == 0)
            {
                connectedPlayer.SendMessage("No genders are available for you to select from. Please contact the server admin!");
                Logger.WriteLine("No genders are available for users to select from! You should not create the GenderSelect state until Gender scripts are created", Logger.Importance.Critical);
                connectedPlayer.SwitchState(new CreationManager(director, CreationManager.CreationState.Completed)); //Just skip to the next step;
            }
            
            foreach (Type type in genders)
            {
                //Our classes are called "GenderMale" etc. If user typed "Male", we need to add "Gender" to the front
                //so that we can make sure we choose the right gender
                if (type.Name.ToLower() == "gender" + input)
                {
                    connectedPlayer.Gender = (IGender)ScriptFactory.GetScript(type.FullName);
                    
                    //We have the gender, move on to the next step in the character creation process.
                    connectedPlayer.SwitchState(new CreationManager(director, CreationManager.CreationState.Completed));
                    break;
                }
            }

            return new NoOpCommand();
        }
    }
}
