using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MudEngine.Game.Characters
{
    /// <summary>
    /// Stats that are used by the Character
    /// </summary>
    public struct CharacterStats
    {
        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Constitution { get; set; }
        public int Intelligence { get; set; }
        public int Wisdom { get; set; }
        public int Charisma { get; set; }
        public int Experience { get; set; }
    }
}
