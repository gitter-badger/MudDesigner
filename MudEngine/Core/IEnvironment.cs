using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MudEngine.Core
{
    public interface IEnvironment : IObject
    {
        bool IsSafe { get; set; }
        List<string> CurrentOccupants { get; }

        string Smell { get; set; }
        string Feel { get; set; }
        string Listen { get; set; }

        void OnOccupantEnter(ICharacter character);

        void OnOccupantExit(ICharacter character);
    }
}
