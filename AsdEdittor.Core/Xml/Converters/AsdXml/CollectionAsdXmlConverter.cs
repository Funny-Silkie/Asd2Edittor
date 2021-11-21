using System.Collections.Generic;

namespace Asd2UI.Xml.Converters
{
    /// <summary>
    /// コレクションを変換する<see cref="AsdXmlConverter{T}"/>のクラス
    /// </summary>
    /// <typeparam name="TElement">要素の型</typeparam>
    public class CollectionAsdXmlConverter<TElement> : DefaultAsdXmlConverter<IEnumerable<TElement>>
    {
        /// <summary>
        /// <see cref="CollectionAsdXmlConverter{TElement}"/>の新しいインスタンスを初期化する
        /// </summary>
        public CollectionAsdXmlConverter() { }
        /// <inheritdoc/>
        protected override object CreateInstance(AsdXmlReader reader) => new List<TElement>();
        /// <inheritdoc/>
        protected override void SetChildren(in object value, AsdXmlReader reader, IEnumerable<object> children)
        {
            base.SetChildren(value, reader, children);
            var list = (List<TElement>)value;
            foreach (var current in children) list.Add((TElement)current);
        }
    }
}
