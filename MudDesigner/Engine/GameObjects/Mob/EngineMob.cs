//-----------------------------------------------------------------------
// <copyright file="EngineMob.cs" company="Sully">
//     Copyright (c) Johnathon Sullinger. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Xml.Serialization;
using MudEngine.Engine.Core;
using MudEngine.Engine.GameObjects.Mob.States;

namespace MudEngine.Engine.GameObjects.Mob
{
    /// <summary>
    /// The base engine mob object that all Mob related objects inherit from.
    /// </summary>
    [Serializable]
    public class EngineMob : GameObject, IMob
    {
        public event EventHandler<MovementEventArgs> Move;

        public event EventHandler<IMessage> SendMessage;

        public event EventHandler<IMessage> ReceivedMessage;

        public IGender Gender
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public Environment.IRoom Location
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public bool IsMute
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public int MaximumInventorySize
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Gets the state of the current mob object.
        /// </summary>
        public StateManager StateManager { get; private set; }

        public override void Initialize(IGame game)
        {
            base.Initialize(game);

            this.StateManager = new StateManager();
            this.StateManager.Initialize(this);
        }

        public virtual void ReceiveInput(IMessage message)
        {
            this.StateManager.PerformCommand(message);

            this.OnMessageReceived(message);
        }

        public virtual void Send(IMessage message)
        {
            this.OnSendMessage(message);
        }

        protected virtual void OnMessageReceived(IMessage args)
        {
            EventHandler<IMessage> handler = ReceivedMessage;
            if (handler != null)
            {
                handler(this, args);
            }
        }

        protected virtual void OnSendMessage(IMessage args)
        {
            EventHandler<IMessage> handler = SendMessage;
            if (handler != null)
            {
                handler(this, args);
            }
        }
    }
}
