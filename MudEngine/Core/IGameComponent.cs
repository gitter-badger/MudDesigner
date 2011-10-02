using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MudEngine.Core
{
    public interface IGameComponent : IObject
    {
        BaseGame ActiveGame { get; }

        void Initialize();

        void Update();
    }
}
