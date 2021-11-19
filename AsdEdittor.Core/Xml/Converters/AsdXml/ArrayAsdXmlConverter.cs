using System;
using System.Collections.Generic;

namespace Asd2UI.Xml.Converters
{
    /// <summary>
    /// 一次元配列を変換する<see cref="AsdXmlConverter{T}"/>のクラス
    /// </summary>
    public class ArrayAsdXmlConverter : AsdXmlConverter<Array>
    {
        private readonly Type elementType;
        /// <summary>
        /// <see cref="ArrayAsdXmlConverter"/>の新しいインスタンスを初期化する
        /// </summary>
        /// <param name="elementType">要素の型</param>
        /// <exception cref="ArgumentNullException"><paramref name="elementType"/>がnull</exception>
        public ArrayAsdXmlConverter(Type elementType)
        {
            this.elementType = elementType ?? throw new ArgumentNullException(nameof(elementType), "引数がnullです");
        }
        /// <inheritdoc/>
        public override bool CanParse(Type type) => type.IsArray;
        /// <inheritdoc/>
        public override bool Convert(XmlEntry xml, AsdXmlReader reader, out Array result)
        {
            var list = new List<object>();
            var converter = reader.AsdXmlConverterProvider.GetConverter(elementType);
            foreach (var children in xml.Children)
            {
                converter.Convert(children, elementType, reader, out var element);
                list.Add(element);
            }
            result = Array.CreateInstance(elementType, list.Count);
            for (int i = 0; i < list.Count; i++) result.SetValue(list[i], i);
            return true;
        }
    }
}
