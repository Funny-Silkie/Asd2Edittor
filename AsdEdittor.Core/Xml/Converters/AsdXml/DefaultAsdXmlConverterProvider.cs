using Altseed2;
using System;

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
                case Type t when t.IsArray: return new ArrayAsdXmlConverter(type.GetGenericArguments()[0]);
                case Type t when IsNodeType(t): return (AsdXmlConverter)Activator.CreateInstance(typeof(NodeAsdXmlConverter<>).MakeGenericType(type));
                default: return (AsdXmlConverter)Activator.CreateInstance(typeof(DefaultAsdXmlConverter<>).MakeGenericType(type));
            }
        }
        private bool IsNodeType(Type type)
        {
            for (; type != null; type = type.BaseType)
                if (type == typeof(Node))
                    return true;
            return false;
        }
    }
}
