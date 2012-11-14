/* SerializationContracts
 * Product: Mud Designer Engine
 * Copyright (c) 2012 AllocateThis! Studios. All rights reserved.
 * http://MudDesigner.Codeplex.com
 *  
 * File Description: Used by the FileIO.Save and Load() methods. Ensures that read-only properties will have their values restored during loading.
 */

//Microsoft .NET using statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

//NewtonSoft JSon using statements
using MudDesigner.Engine.Scripting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using log4net;

namespace MudDesigner.Engine.Core
{
    /// <summary>
    /// Used by the FileIO.Save and Load() methods. Ensures that read-only properties will have their values restored during loading.
    /// </summary>
    public class SerializationContracts : DefaultContractResolver
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(SerializationContracts)); 

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            try
            {
                var property = base.CreateProperty(member, memberSerialization);

                if (!property.Writable)
                {
                    var prop = member as PropertyInfo;

                    if (prop != null)
                    {
                        var hasPrivateSetter = prop.GetSetMethod(true) != null;
                        property.Writable = hasPrivateSetter;
                    }
                }

                return property;
            }
            catch(Exception ex)
            {
                Log.Fatal(string.Format("SerializationContracts failed to create a required property for '{0}'!",member.Name));
                Log.Fatal(string.Format("SerializationContracts failed with the following message: {0}", ex.Message));
                Log.Fatal(string.Format("SerializationContract failures could be caused by corrupt save files."));
            }

            return null;
        }
    }
}
