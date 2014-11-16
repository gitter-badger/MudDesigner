using System;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MudEngine.Engine.Commands;
using MudEngine.Engine.Core;
using MudEngine.Engine.Factories;
using MudEngine.Engine.GameObjects;
using MudEngine.Engine.GameObjects.Mob;
using MudEngine.Engine.GameObjects.Mob.States;

namespace MudEngine.Tests.Win.States
{
    [TestClass]
    public class StateManagerTests
    {
        [TestMethod]
        public void ExecuteConventionBasedCommand()
        {
            var stateManager = new StateManager();
            var game = new TestGameImplementation();
            game.Initialize<DefaultPlayer>(null);

            var commands = CommandFactory.GetCommands(new Assembly[] { Assembly.GetExecutingAssembly() });

            if (commands.Count() == 0)
            {
                Assert.Fail("No commands were fetched from the factory.");
            }

            stateManager.Initialize(game.Player, commands);

            stateManager.PerformCommand(new ReceivedInputMessage("CommandForTesting"));

            Assert.IsTrue(game.Messages.Count > 0);
            Assert.IsNotNull(game.Player.StateManager.CurrentState);
            Assert.IsTrue(game.Player.StateManager.CurrentState.GetType() == typeof(TestState));
        }

        [TestMethod]
        public void ExecuteContinousCommand()
        {
            var stateManager = new StateManager();
            var game = new TestGameImplementation();
            game.Initialize<DefaultPlayer>(null);

            var commands = CommandFactory.GetCommands(new Assembly[] { Assembly.GetExecutingAssembly() });

            if (commands.Count() == 0)
            {
                Assert.Fail("No commands were fetched from the factory.");
            }

            stateManager.Initialize(game.Player, commands);

            stateManager.PerformCommand(new ReceivedInputMessage("CommandWithInput"));

            Assert.IsTrue(game.Messages.Count > 0);
            Assert.IsNotNull(game.Player.StateManager.CurrentState);
            Assert.IsTrue(game.Player.StateManager.CurrentState.GetType() == typeof(ReceivingInputState));

            stateManager.PerformCommand(new ReceivedInputMessage("someRandomStuff"));

            Assert.IsTrue(game.Messages.Count > 0);
            Assert.IsNotNull(game.Player.StateManager.CurrentState);
            Assert.IsTrue(game.Player.StateManager.CurrentState.GetType() == typeof(TestState));
        }

        [TestMethod]
        public void ExecuteContinousInvalidCommand()
        {
            var stateManager = new StateManager();
            var game = new TestGameImplementation();
            game.Initialize<DefaultPlayer>(null);

            var commands = CommandFactory.GetCommands(new Assembly[] { Assembly.GetExecutingAssembly() });

            if (commands.Count() == 0)
            {
                Assert.Fail("No commands were fetched from the factory.");
            }

            stateManager.Initialize(game.Player, commands);

            stateManager.PerformCommand(new ReceivedInputMessage("CommandWithInput"));

            Assert.IsTrue(game.Messages.Count > 0);
            Assert.IsNotNull(game.Player.StateManager.CurrentState);
            Assert.IsTrue(game.Player.StateManager.CurrentState.GetType() == typeof(ReceivingInputState));

            stateManager.PerformCommand(new ReceivedInputMessage("someRandomStuff"));

            Assert.IsTrue(game.Messages.Count > 0);
            Assert.IsNotNull(game.Player.StateManager.CurrentState);
            Assert.IsTrue(game.Player.StateManager.CurrentState.GetType() == typeof(TestState));

            stateManager.PerformCommand(new ReceivedInputMessage("randomMessage"));

            Assert.IsTrue(game.Messages.Pop() == "Invalid command used!" + Environment.NewLine);
        }

        [TestMethod]
        public void ExecuteShorthandCommand()
        {
            var stateManager = new StateManager();
            var game = new TestGameImplementation();
            game.Initialize<DefaultPlayer>(null);

            var commands = CommandFactory.GetCommands(new Assembly[] { Assembly.GetExecutingAssembly() });

            if (commands.Count() == 0)
            {
                Assert.Fail("No commands were fetched from the factory.");
            }

            stateManager.Initialize(game.Player, commands);

            stateManager.PerformCommand(new ReceivedInputMessage("mtc"));

            Assert.IsTrue(game.Messages.Count > 0);
            Assert.IsNotNull(game.Player.StateManager.CurrentState);
            Assert.IsTrue(game.Player.StateManager.CurrentState.GetType() == typeof(TestState));
        }

        [TestMethod]
        public void ExecuteNamedCommand()
        {
            var stateManager = new StateManager();
            var game = new TestGameImplementation();
            game.Initialize<DefaultPlayer>(null);

            var commands = CommandFactory.GetCommands(new Assembly[] { Assembly.GetExecutingAssembly() });

            if (commands.Count() == 0)
            {
                Assert.Fail("No commands were fetched from the factory.");
            }

            stateManager.Initialize(game.Player, commands);

            stateManager.PerformCommand(new ReceivedInputMessage("MyTestCommand"));

            Assert.IsTrue(game.Messages.Count > 0);
            Assert.IsNotNull(game.Player.StateManager.CurrentState);
            Assert.IsTrue(game.Player.StateManager.CurrentState.GetType() == typeof(TestState));
        }
    }
}