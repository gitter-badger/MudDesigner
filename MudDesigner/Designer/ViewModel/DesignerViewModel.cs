using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MudEngine.Engine.Core;
using MudEngine.Engine.Factories;
using GalaSoft.MvvmLight;

namespace Designer.ViewModel
{
    /// <summary>
    /// The view model associated with the editor.
    /// </summary>
    public class DesignerViewModel : ViewModelBase
    {
        /// <summary>
        /// The game backing field
        /// </summary>
        private IGame game;

        /// <summary>
        /// Initializes a new instance of the <see cref="DesignerViewModel"/> class.
        /// </summary>
        public DesignerViewModel()
        {
            if (IsInDesignMode)
            {
                this.game = new EngineGame();
            }
            else
            {
                this.game = GameFactory.GetGame<EngineGame>();
            }
        }

        /// <summary>
        /// Gets or sets the game object.
        /// </summary>
        public IGame Game
        {
            get
            {
                return this.game;
            }
            set
            {
                this.game = value;
                this.RaisePropertyChanged();
            }
        }
        /// <summary>
        /// Raises the PropertyChanged event if needed.
        /// </summary>
        /// <param name="propertyName">The name of the property that
        ///             changed.</param>
        /// <remarks>
        /// If the propertyName parameter
        ///             does not correspond to an existing property on the current class, an
        ///             exception is thrown in DEBUG configuration only.
        /// </remarks>
        protected override void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            base.RaisePropertyChanged(propertyName);
        }

    }
}
