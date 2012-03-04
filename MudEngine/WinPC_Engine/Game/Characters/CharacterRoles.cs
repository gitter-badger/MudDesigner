using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MudEngine.Game.Characters
{
    /// <summary>
    /// Various server roles that a character can have. 
    /// </summary>
    public enum CharacterRoles
    {
        Admin,
        Immortal,
        GM,
        Builder,
        QuestGiver,
        Player,
        NPC
    }
}
