using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MudEngine.Engine.Commands;
using MudEngine.Engine.Core;
using MudEngine.Engine.Factories;
using MudEngine.Engine.GameObjects;
using MudEngine.Engine.GameObjects.Mob;
using MudEngine.Engine.GameObjects.Mob.States;

namespace MudEngine.Tests.Win
{
    public class TestGameImplementation : DefaultGame
    {
        /// <summary>
        /// Gets or sets the messages.
        /// </summary>
        public Stack<string> Messages { get; set; }

        public override void Initialize<T>(IPersistedStorage storageSource)
        {
            this.Messages = new Stack<string>();

            this.Player = new T();
            this.Player.Initialize(this);
            this.Player.SendMessage += (target, message) => this.Messages.Push(message.FormatMessage());
        }
    }
}
