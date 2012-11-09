using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MudDesigner.Engine.Core
{
    public class SerializationContracts : DefaultContractResolver
    {
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
                Logger.WriteLine("SerializationContracts failed to create a required property for '" + member.Name + "'!", Logger.Importance.Critical);
                Logger.WriteLine("SerializationContracts failed with the following message: " + ex.Message, Logger.Importance.Critical);
                Logger.WriteLine("SerializationContract failures could be caused by corrupt save files.", Logger.Importance.Critical);
            }

            return null;
        }
    }
}
