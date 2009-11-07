using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MUDEngine.Environment
{
    /// <summary>
    /// A Realm contains an unlimited number of Zones, allowing developers to split their worlds up into chunks.
    /// </summary>
    public class Realm
    {
        /// <summary>
        /// The name of the realm.
        /// </summary>
        public string Name
        {
            get;
            set;
        }
        
        /// <summary>
        /// Description of the Realm.
        /// </summary>
        public string Description
        {
            get;
            set;
        }
    }
}
