using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace MudEngine.Core
{
    public abstract class BaseEnvironment : BaseObject
    {
        [Category("Environment Information")]
        [Description("If a user asks to use his/her senses to investigate an area, this is one of the results that will be displayed. Senses can be used to assist blind characters.")]
        [DefaultValue("You don't smell anything unsual.")]
        public String Smell { get; set; }

        [Category("Environment Information")]
        [Description("If a user asks to use his/her senses to investigate an area, this is one of the results that will be displayed. Senses can be used to assist blind characters.")]
        [DefaultValue("You hear nothing of interest.")]
        public String Listen { get; set; }

        [Category("Environment Information")]
        [Description("If a user asks to use his/her senses to investigate an area, this is one of the results that will be displayed. Senses can be used to assist blind characters.")]
        [DefaultValue("You feel nothing.")]
        public String Feel { get; set; }

        public BaseEnvironment(BaseGame game) : base(game)
        {
            this.Feel = "You feel nothing.";
            this.Listen = "You hear nothing of interest.";
            this.Smell = "You don't smell anything unsual.";
        }

        public virtual void OnEnter()
        {
        }

        public virtual void OnExit()
        {
        }
    }
}
