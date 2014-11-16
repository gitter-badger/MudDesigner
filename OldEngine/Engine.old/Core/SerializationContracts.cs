//-----------------------------------------------------------------------
// <copyright file="SerializationContracts.cs" company="AllocateThis!">
//     Copyright (c) AllocateThis! Studio's. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
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
        /// <summary>
        /// The logger
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(SerializationContracts));

        /// <summary>
        /// Creates a 
        /// <see cref="T:Newtonsoft.Json.Serialization.JsonProperty" /> for the given 
        /// <see cref="T:System.Reflection.MemberInfo" />.
        /// </summary>
        /// <param name="member">The member to create a <see cref="T:Newtonsoft.Json.Serialization.JsonProperty" /> for.</param>
        /// <param name="memberSerialization">The member's parent <see cref="T:Newtonsoft.Json.MemberSerialization" />.</param>
        /// <returns>
        /// A created <see cref="T:Newtonsoft.Json.Serialization.JsonProperty" /> for the given <see cref="T:System.Reflection.MemberInfo" />.
        /// </returns>
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
