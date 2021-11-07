using Altseed2;
using System;
using System.Globalization;

namespace Asd2UI.Xml.Converters
{
    internal class FuncConverter<T> : TextValueConverter<T>
    {
        private readonly Converter<string, (bool, T)> converter;
        internal FuncConverter(Converter<string, (bool, T)> converter)
        {
            this.converter = converter ?? throw new ArgumentNullException(nameof(converter), "引数がnullです");
        }
        public override bool Convert(string value, out T result)
        {
            var (ret, _result) = converter.Invoke(value);
            result = _result;
            return ret;
        }
    }
    internal class SByteTextValueConverter : TextValueConverter<sbyte>
    {
        public override bool Convert(string value, out sbyte result) => sbyte.TryParse(value, out result);
    }
    internal class ByteTextValueConverter : TextValueConverter<byte>
    {
        public override bool Convert(string value, out byte result) => byte.TryParse(value, out result);
    }
    internal class Int16TextValueConverter : TextValueConverter<short>
    {
        public override bool Convert(string value, out short result) => short.TryParse(value, out result);
    }
    internal class UInt16TextValueConverter : TextValueConverter<ushort>
    {
        public override bool Convert(string value, out ushort result) => ushort.TryParse(value, out result);
    }
    internal class Int32TextValueConverter : TextValueConverter<int>
    {
        public override bool Convert(string value, out int result) => int.TryParse(value, out result);
    }
    internal class UInt32TextValueConverter : TextValueConverter<uint>
    {
        public override bool Convert(string value, out uint result) => uint.TryParse(value, out result);
    }
    internal class Int64TextValueConverter : TextValueConverter<long>
    {
        public override bool Convert(string value, out long result) => long.TryParse(value, out result);
    }
    internal class UInt64TextValueConverter : TextValueConverter<ulong>
    {
        public override bool Convert(string value, out ulong result) => ulong.TryParse(value, out result);
    }
    internal class SingleTextValueConverter : TextValueConverter<float>
    {
        public override bool Convert(string value, out float result) => float.TryParse(value, out result);
    }
    internal class DoubleTextValueConverter : TextValueConverter<double>
    {
        public override bool Convert(string value, out double result) => double.TryParse(value, out result);
    }
    internal class DecimalTextValueConverter : TextValueConverter<decimal>
    {
        public override bool Convert(string value, out decimal result) => decimal.TryParse(value, out result);
    }
    internal class CharTextValueConverter : TextValueConverter<char>
    {
        public override bool Convert(string value, out char result) => char.TryParse(value, out result);
    }
    internal class EnumTextValueConverter : TextValueConverter<Enum>
    {
        private readonly Type enumType;
        internal EnumTextValueConverter(Type enumType)
        {
            this.enumType = enumType ?? throw new ArgumentNullException(nameof(enumType), "引数がnullです");
        }
        public override bool Convert(string value, out Enum result)
        {
            var ret = Enum.TryParse(enumType, value, out var _result);
            result = (Enum)_result;
            return ret;
        }
    }
    internal class Vector2FTextValueConverter : TextValueConverter<Vector2F>
    {
        public override bool Convert(string value, out Vector2F result)
        {
            if (value == null)
            {
                result = default;
                return false;
            }
            if (float.TryParse(value, out var single))
            {
                result = new Vector2F(single, single);
                return true;
            }
            var values = value.Split(',');
            if (values.Length != 2 || !float.TryParse(values[0], out var x) || !float.TryParse(values[1], out var y))
            {
                result = default;
                return false;
            }
            result = new Vector2F(x, y);
            return true;
        }
    }
    internal class Vector2ITextValueConverter : TextValueConverter<Vector2I>
    {
        public override bool Convert(string value, out Vector2I result)
        {
            if (value == null)
            {
                result = default;
                return false;
            }
            if (int.TryParse(value, out var single))
            {
                result = new Vector2I(single, single);
                return true;
            }
            var values = value.Split(',');
            if (values.Length != 2 || !int.TryParse(values[0], out var x) || !int.TryParse(values[1], out var y))
            {
                result = default;
                return false;
            }
            result = new Vector2I(x, y);
            return true;
        }
    }
    internal class Vector3FTextValueConverter : TextValueConverter<Vector3F>
    {
        public override bool Convert(string value, out Vector3F result)
        {
            if (value == null)
            {
                result = default;
                return false;
            }
            if (float.TryParse(value, out var single))
            {
                result = new Vector3F(single, single, single);
                return true;
            }
            var values = value.Split(',');
            if (values.Length != 3 || !float.TryParse(values[0], out var x) || !float.TryParse(values[1], out var y) || !float.TryParse(values[2], out var z))
            {
                result = default;
                return false;
            }
            result = new Vector3F(x, y, z);
            return true;
        }
    }
    internal class Vector3ITextValueConverter : TextValueConverter<Vector3I>
    {
        public override bool Convert(string value, out Vector3I result)
        {
            if (value == null)
            {
                result = default;
                return false;
            }
            if (int.TryParse(value, out var single))
            {
                result = new Vector3I(single, single, single);
                return true;
            }
            var values = value.Split(',');
            if (values.Length != 3 || !int.TryParse(values[0], out var x) || !int.TryParse(values[1], out var y) || !int.TryParse(values[2], out var z))
            {
                result = default;
                return false;
            }
            result = new Vector3I(x, y, z);
            return true;
        }
    }
    internal class Vector4FTextValueConverter : TextValueConverter<Vector4F>
    {
        public override bool Convert(string value, out Vector4F result)
        {
            if (value == null)
            {
                result = default;
                return false;
            }
            if (float.TryParse(value, out var single))
            {
                result = new Vector4F(single, single, single, single);
                return true;
            }
            var values = value.Split(',');
            if (values.Length != 4 || !float.TryParse(values[0], out var x) || !float.TryParse(values[1], out var y) || !float.TryParse(values[2], out var z) || !float.TryParse(values[3], out var w))
            {
                result = default;
                return false;
            }
            result = new Vector4F(x, y, z, w);
            return true;
        }
    }
    internal class Vector4ITextValueConverter : TextValueConverter<Vector4I>
    {
        public override bool Convert(string value, out Vector4I result)
        {
            if (value == null)
            {
                result = default;
                return false;
            }
            if (int.TryParse(value, out var single))
            {
                result = new Vector4I(single, single, single, single);
                return true;
            }
            var values = value.Split(',');
            if (values.Length != 4 || !int.TryParse(values[0], out var x) || !int.TryParse(values[1], out var y) || !int.TryParse(values[2], out var z) || !int.TryParse(values[2], out var w))
            {
                result = default;
                return false;
            }
            result = new Vector4I(x, y, z, w);
            return true;
        }
    }
    internal class ColorTextValueConverter : TextValueConverter<Color>
    {
        public override bool Convert(string value, out Color result)
        {
            if (value == null)
            {
                result = default;
                return false;
            }
            if (value.LastIndexOf('#') == 1)
            {
                if (value.Length == 7)
                {
                    if (byte.TryParse(value.Substring(1, 2), NumberStyles.HexNumber, null, out var r) &&
                        byte.TryParse(value.Substring(3, 2), NumberStyles.HexNumber, null, out var g) &&
                        byte.TryParse(value.Substring(5, 2), NumberStyles.HexNumber, null, out var b))
                    {
                        result = new Color(r, b, g);
                        return true;
                    }
                }
                if (value.Length == 9)
                {
                    if (int.TryParse(value.Substring(1, 2), NumberStyles.HexNumber, null, out var r) &&
                        int.TryParse(value.Substring(3, 2), NumberStyles.HexNumber, null, out var g) &&
                        int.TryParse(value.Substring(5, 2), NumberStyles.HexNumber, null, out var b) &&
                        int.TryParse(value.Substring(7, 2), NumberStyles.HexNumber, null, out var a))
                    {
                        result = new Color(r, b, g, a);
                        return true;
                    }
                }
                result = default;
                return false;
            }
            var values = value.Split(',');
            if (values.Length == 3)
            {
                if (byte.TryParse(values[0], out var r) && byte.TryParse(values[1], out var g) && byte.TryParse(values[2], out var b))
                {
                    result = new Color(r, g, b);
                    return true;
                }
            }
            if (values.Length == 4)
            {
                if (int.TryParse(values[0], out var r) && int.TryParse(values[1], out var g) && int.TryParse(values[2], out var b) && int.TryParse(values[3], out var a))
                {
                    result = new Color(r, g, b, a);
                    return true;
                }
            }
            result = default;
            return false;
        }
    }
    internal class RectFTextValueConverter : TextValueConverter<RectF>
    {
        public override bool Convert(string value, out RectF result)
        {
            if (value == null)
            {
                result = default;
                return false;
            }
            if (float.TryParse(value, out var single))
            {
                result = new RectF(single, single, single, single);
                return true;
            }
            var values = value.Split(',');
            if (values.Length == 2)
            {
                if (float.TryParse(values[0], out var p) && float.TryParse(values[2], out var s))
                {
                    result = new RectF(p, p, s, s);
                    return true;
                }
            }
            if (values.Length == 4)
            {
                if (float.TryParse(values[0], out var x) && float.TryParse(values[1], out var y) && float.TryParse(values[2], out var width) && float.TryParse(values[3], out var height))
                {
                    result = new RectF(x, y, width, height);
                    return true;
                }
            }
            result = default;
            return false;
        }
    }
    internal class RectITextValueConverter : TextValueConverter<RectI>
    {
        public override bool Convert(string value, out RectI result)
        {
            if (value == null)
            {
                result = default;
                return false;
            }
            if (int.TryParse(value, out var single))
            {
                result = new RectI(single, single, single, single);
                return true;
            }
            var values = value.Split(',');
            if (values.Length == 2)
            {
                if (int.TryParse(values[0], out var p) && int.TryParse(values[2], out var s))
                {
                    result = new RectI(p, p, s, s);
                    return true;
                }
            }
            if (values.Length == 4)
            {
                if (int.TryParse(values[0], out var x) && int.TryParse(values[1], out var y) && int.TryParse(values[2], out var width) && int.TryParse(values[3], out var height))
                {
                    result = new RectI(x, y, width, height);
                    return true;
                }
            }
            result = default;
            return false;
        }
    }
    internal class StringTextValueConverter : TextValueConverter<string>
    {
        public override bool Convert(string value, out string result)
        {
            result = value;
            return true;
        }
    }
}
