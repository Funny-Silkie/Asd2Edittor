using Altseed2;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Asd2UI.Xml.Converters
{
    internal class DefaultAsdXmlConverterProvider : AsdXmlConverterProvider
    {
        internal DefaultAsdXmlConverterProvider()
        {

        }
        public override AsdXmlConverter GetConverter(Type type)
        {
            switch (type)
            {
                case null: throw new ArgumentNullException(nameof(type), "引数がnullです");
                case Type t when t.IsArray: return new ArrayAsdXmlConverter(type.GetElementType());
                case Type t when t.IsBaseType<Node>(): return (AsdXmlConverter)ReflectionHelper.CreateGenericInstance(typeof(NodeAsdXmlConverter<>), t);
                case Type t when t.IsInterface && t.HasInterface(typeof(IEnumerable<>), true):
                    var ifs = t.GetInterfaces();
                    var sg = ifs.Single(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IEnumerable<>));
                    return (AsdXmlConverter)ReflectionHelper.CreateGenericInstance(typeof(CollectionAsdXmlConverter<>), sg);
                default: return (AsdXmlConverter)ReflectionHelper.CreateGenericInstance(typeof(DefaultAsdXmlConverter<>), type);
            }
        }
    }
}
