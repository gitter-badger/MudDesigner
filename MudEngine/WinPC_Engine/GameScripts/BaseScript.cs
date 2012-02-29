using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace MudEngine.GameScripts
{
    public class BaseScript
    {
        public String Name { get; set; }

        public String ID { get; set; }

        public String Description { get; set; }

        public BaseScript(String name, String description)
        {
            this.ID = Guid.NewGuid().ToString();
        }

        public override string ToString()
        {
            if (String.IsNullOrEmpty(this.Name))
                return this.GetType().Name + " without Name";
            else
                return this.Name;
        }
    }
}
