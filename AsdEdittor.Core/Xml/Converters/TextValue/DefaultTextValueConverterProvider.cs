using Altseed2;
using System;

namespace Asd2UI.Xml.Converters
{
    internal class DefaultTextValueConverterProvider : TextValueConverterProvider
    {
        internal DefaultTextValueConverterProvider()
        {

        }
        public override TextValueConverter GetConverter(Type type)
        {
            switch (type)
            {
                case null: throw new ArgumentNullException(nameof(type), "引数がnullです");
                case Type t when t == typeof(sbyte): return new SByteTextValueConverter();
                case Type t when t == typeof(byte): return new ByteTextValueConverter();
                case Type t when t == typeof(short): return new Int16TextValueConverter();
                case Type t when t == typeof(ushort): return new UInt16TextValueConverter();
                case Type t when t == typeof(int): return new Int32TextValueConverter();
                case Type t when t == typeof(uint): return new UInt32TextValueConverter();
                case Type t when t == typeof(long): return new Int64TextValueConverter();
                case Type t when t == typeof(ulong): return new UInt64TextValueConverter();
                case Type t when t == typeof(float): return new SingleTextValueConverter();
                case Type t when t == typeof(double): return new DoubleTextValueConverter();
                case Type t when t == typeof(decimal): return new DecimalTextValueConverter();
                case Type t when t == typeof(char): return new CharTextValueConverter();
                case Type t when t == typeof(Enum): return new EnumTextValueConverter(t);
                case Type t when t == typeof(Vector2F): return new Vector2FTextValueConverter();
                case Type t when t == typeof(Vector2I): return new Vector2ITextValueConverter();
                case Type t when t == typeof(Vector3F): return new Vector3FTextValueConverter();
                case Type t when t == typeof(Vector3I): return new Vector3ITextValueConverter();
                case Type t when t == typeof(Vector4F): return new Vector4FTextValueConverter();
                case Type t when t == typeof(Vector4I): return new Vector4ITextValueConverter();
                case Type t when t == typeof(Color): return new ColorTextValueConverter();
                case Type t when t == typeof(RectF): return new RectFTextValueConverter();
                case Type t when t == typeof(RectI): return new RectITextValueConverter();
                default: return null;
            }
        }
    }
}
