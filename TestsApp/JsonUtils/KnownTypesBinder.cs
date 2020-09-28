using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Serialization;

namespace TestsApp.JsonUtils
{
    public class KnownTypesBinder : DefaultSerializationBinder
    {
        public bool UseDefaultIfUnknown = false;

        public List<(Type, string)> TypeNames { get; set; }

        public KnownTypesBinder(List<(Type, string)> typeNames)
        {
            TypeNames = typeNames;
        }

        public override Type BindToType(string assemblyName, string typeName)
        {
            var res = TypeNames.SingleOrDefault(t => t.Item2 == typeName).Item1;

            if (res != null)
            {
                return res;
            }
            else
            {
                if (UseDefaultIfUnknown)
                {
                    return base.BindToType(assemblyName, typeName);
                }
                else
                {
                    throw new Exception("Unknown type");
                }
            }

//            return res != null
//                ? res
//                : (UseDefaultIfUnknown
//                    ? base.BindToType(assemblyName, typeName)
//                    : throw new Exception());
        }

        public override void BindToName(Type serializedType, out string assemblyName, out string typeName)
        {
            var res = TypeNames.SingleOrDefault(t => t.Item1 == serializedType).Item2;
            if (!string.IsNullOrEmpty(res))
            {
                assemblyName = null;
                typeName = res;
            }
            else
            {
                if (UseDefaultIfUnknown)
                {
                    base.BindToName(serializedType, out assemblyName, out typeName);
                }
                else
                {
                    throw new Exception("Unknown type");
                }
            }
        }
    }
}
