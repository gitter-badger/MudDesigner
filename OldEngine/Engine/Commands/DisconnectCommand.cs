    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using MudEngine.Engine.Core;
    using MudEngine.Engine.Networking;

    namespace MudEngine.Engine.Commands
    {
        [ShorthandName("Disconnect", "/dc")]
        public class DisconnectCommand : ICommand
        {
            public string CommandInput { get; private set; }

            public bool IsIncomplete { get; private set; }

            public void Execute(GameObjects.Mob.IMob mob, string input)
            {
                if (mob is ServerPlayer)
                {
                    var player = mob as ServerPlayer;

                    mob.Send(new InformationalMessage("Disconnecting."));
                    player.Disconnect();
                }
            }
        }
    }
