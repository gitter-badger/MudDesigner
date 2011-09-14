using System;
using System.Reflection;
using System.ComponentModel;
namespace MudEngine.Attributes
{
    [System.AttributeUsage(System.AttributeTargets.Property)]
    public class ParseProperty
        : System.Attribute
    {
        Object obj;

        public ParseProperty(Object var)
        {
            obj = var;
        }

        public object GetObject()
        {
            return obj;
        }
    }

}