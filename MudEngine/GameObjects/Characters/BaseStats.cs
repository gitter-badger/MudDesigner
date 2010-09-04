using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MudEngine.GameObjects.Characters
{
    /// <summary>
    /// This class is the base class that handles the managing of a Characters stats. 
    /// It was decided to place stats within their own class, to allow for developers to easily add additional
    /// stats or adjust how stats are used within their MUD's without having to modify the Character code themselves.
    /// </summary>
    public class BaseStats
    {
        /// <summary>
        /// Strength is a measure of muscle, endurance and stamina combined. 
        /// Strength affects the ability of characters to lift and carry weights, melee attack rolls, 
        /// damage rolls (for both melee and ranged weapons,) the Jump, Climb, and Swim skills, 
        /// several combat actions, and general checks involving moving or breaking stubborn objects.
        /// </summary>
        public Int32 Strength { get; set; }

        /// <summary>
        /// Dexterity encompasses a number of physical attributes including hand-eye coordination, agility, 
        /// reflexes, fine motor skills, balance and speed of movement; a high dexterity score indicates 
        /// superiority in all these attributes. Dexterity affects characters with regard to initiative in combat, 
        /// ranged attack rolls, Armor Class, Reflex saves, and the Balance, Escape Artist, Hide, Move Silently, 
        /// Open Lock, Ride, Sleight of Hand, Tumble, and Use Rope skills. It also affects the number of additional 
        /// attacks of opportunity granted by the Combat Reflexes feat. Dexterity is the ability most influenced by 
        /// outside influences (such as armor).
        /// </summary>
        public Int32 Dexterity { get; set; }

        /// <summary>
        /// Constitution is a term which encompasses the character's physique, toughness, health and resistance to disease and poison. 
        /// The higher a character's Constitution, the more hit points that character will have. 
        /// Constitution also is important for Fortitude saves, the Concentration skill, and fatigue-based general checks. 
        /// Constitution also determines the duration of a barbarian's rage. 
        /// Unlike the other ability scores, which render the character unconscious or immobile when they hit 0, 
        /// having 0 Constitution is fatal.
        /// </summary>
        public Int32 Constitution { get; set; }

        /// <summary>
        /// Intelligence is similar to IQ, but also includes mnemonic ability, reasoning and learning ability outside 
        /// those measured by the written word. Intelligence dictates the number of languages a character can learn, 
        /// and it influences the number of spells a preparation-based arcane spellcaster (like a Wizard) may cast per 
        /// day, and the effectiveness of said spells. It also affects how many skill points a character gains per level, 
        /// the Appraise, Craft, Decipher Script, Disable Device, Forgery, Knowledge, Search, and Spellcraft skills, 
        /// and bardic knowledge checks.
        /// </summary>
        public Int32 Intelligence { get; set; }

        /// <summary>
        /// Wisdom is a composite term for the characters enlightenment, judgement, wile, willpower and intuitiveness. 
        /// Wisdom influences the number of spells a divine spellcaster (like clerics, druids, paladins, and rangers) 
        /// can cast per day, and the effectiveness of said spells. It also affects Will saving throws, the Heal, Listen, 
        /// Profession, Sense Motive, Spot, and Survival skills, the effectiveness of the Stunning Fist feat, and a 
        /// monk's quivering palm attack.
        /// </summary>
        public Int32 Wisdom { get; set; }

        /// <summary>
        /// Charisma is the measure of the character's combined physical attractiveness, persuasiveness, and personal magnetism. 
        /// A generally non-beautiful character can have a very high charisma due to strong measures of the other two aspects of charisma. 
        /// Charisma influences how many spells spontaneous arcane spellcasters (like sorcerers and bards) can cast per day, and the 
        /// effectiveness of said spells. It also affects Bluff, Diplomacy, Disguise, Gather Information, Handle Animal, 
        /// Intimidate, Perform, and Use Magic Device checks, how often and how effectively clerics and paladins can turn 
        /// undead, the wild empathy of druids and rangers, and a paladin's lay on hands ability.
        /// </summary>
        public Int32 Charisma { get; set; }

        /// <summary>
        /// Experiance is given to the player based off activities that they perform.
        /// </summary>
        public Int32 Experiance { get; set; }

        
    }
}
