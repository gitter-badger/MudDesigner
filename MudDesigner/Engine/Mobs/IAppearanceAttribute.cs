using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MudDesigner.Engine.Mobs
{
    public interface IAppearanceAttribute
    {
        //Name of this appearance attribute. Example: Hair
        string Name { get; set; }

        //Description of this appearance attribute.  Example: With long flowing hair that shines brightly in the sunlight.
        string Description { get; set; }

        //Value that overrides the default description. Used for custom values that the player sets.
        string Value { get; set; }
    }   
}
