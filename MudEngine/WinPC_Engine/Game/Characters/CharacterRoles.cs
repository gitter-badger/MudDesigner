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

    public static class CharacterRole
    {
        public static CharacterRoles GetRole(String role)
        {
            //Blow all of the available values up into an array.
            Array values = Enum.GetValues(typeof(CharacterRoles));

            //Loop through each available value, converting it into a string.
            foreach (Int32 value in values)
            {
                //Get the string representation of the current value
                String displayName = Enum.GetName(typeof(CharacterRoles), value);

                //Check if this value matches that of the supplied one.
                //If so, return it as a enum
                if (displayName.ToLower() == role.ToLower())
                    return (CharacterRoles)Enum.Parse(typeof(CharacterRoles), displayName);
            }

            return CharacterRoles.Player;
        }
    }
}
