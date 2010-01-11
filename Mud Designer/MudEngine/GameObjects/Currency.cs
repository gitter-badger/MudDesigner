using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace MudDesigner.MudEngine.GameObjects
{
    public class Currency : BaseObject
    {
        [Category("Currency Settings")]
        [Description("The value of the currency is based off the BaseCurrencyValue set in the Project Information. If BaseCurrencyValue is 1, and a new Currency is 10, then it will take 10 BaseCurrency to equal 1 of the new Currencies.")]
        [DefaultValue(100)]
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
