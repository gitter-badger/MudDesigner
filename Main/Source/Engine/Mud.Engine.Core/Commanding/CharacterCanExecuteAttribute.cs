using Mud.Engine.Core.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mud.Engine.Core.Commanding
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
