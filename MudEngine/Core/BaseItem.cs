using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MudEngine.Core
{
    public abstract class BaseItem : BaseObject, IGameComponent
    {
        #region Properties
        /// <summary>
        /// Gets a reference to the currently active game.
        /// </summary>
        [Browsable(false)]
        public BaseGame ActiveGame { get; private set; }
        #endregion 

        public BaseItem(BaseGame game)
        {
            this.ActiveGame = game;
            this.ID = this.ActiveGame.GetAvailableID();
        }

        /// <summary>
        /// Initializes the item
        /// </summary>
        public abstract void Initialize();

        public abstract void Update();

        BaseGame IGameComponent.ActiveGame
        {
            get { throw new NotImplementedException(); }
        }

        void IGameComponent.Initialize()
        {
            throw new NotImplementedException();
        }

        void IGameComponent.Update()
        {
            throw new NotImplementedException();
        }

        string IObject.Name
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

        string IObject.Filename
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

        string IObject.Description
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
    }
}
