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
    }
}
