using Mud.Engine.Runtime.Character;
using Mud.Engine.Shared.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mud.Engine.Runtime.Commanding
{
    public sealed class CharacterCanExecuteAttribute : Attribute
    {
        public CharacterCanExecuteAttribute(ICharacter character)
        {
            this.CharacterType = character.GetType();
        }

        public Type CharacterType { get; private set; }
    }
}
