using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.Generic;

namespace MUDEngine.Objects
{
    public class Currency : BaseObject
    {
        [Category("Currency Settings")]
        /// <summary>
        /// The value of this currency. It should be how many 'base currency' it takes to equal 1 of this currency
        /// </summary>
        public int Value
        {
            get;
            set;
        }

        public Currency()
        {
            this.Name = "New Currency";
            this.Value = 100;
        }
    }
}
