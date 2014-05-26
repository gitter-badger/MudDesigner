//-----------------------------------------------------------------------
// <copyright file="DefaultPlayer.cs" company="Sully">
//     Copyright (c) Johnathon Sullinger. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MudEngine.Engine.Core;

namespace MudEngine.Engine.GameObjects.Mob
{
    public class DefaultPlayer : IPlayer
    {
        public DefaultPlayer()
        {
            this.Name = "New Player";
        }

        public Core.IGame Game { get; set; }

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

        public int Id
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

        public string Name { get; set; }

        public string Description
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

        public bool IsEditable
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

        public bool IsEnabled
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

        public bool IsPermanent
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

        public bool IsDestroyed
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

        public DateTime LastUpdated
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

        public DateTime CreatedDate
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

        public event EventHandler<MovementEventArgs> Move;

        public event EventHandler<IMessage> SendMessage;

        public event EventHandler<IMessage> ReceivedMessage;

        public virtual void Initialize(IGame game)
        {
            this.Game = game;
        }

        public void CopyState(IGameObject copyFrom, bool ignoreExistingPropertyValues = false)
        {
            throw new NotImplementedException();
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

        public virtual void ReceiveInput(IMessage message)
        {
            if (message.Message.StartsWith("hello"))
            {
                this.Send(message);
            }

            this.OnMessageReceived(message);
        }

        public virtual void Send(IMessage message)
        {
            this.OnSendMessage(message);
        }
    }
}
