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
                default: return null;
            }
        }
    }
}
