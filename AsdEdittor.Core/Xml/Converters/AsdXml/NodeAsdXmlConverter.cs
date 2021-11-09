using Altseed2;
using System;
using System.Collections.Generic;

namespace Asd2UI.Xml.Converters
{
    /// <summary>
    /// <see cref="Node"/>の<see cref="AsdXmlConverter{T}"/>のクラス
    /// </summary>
    /// <typeparam name="TNode">変換先の<see cref="Node"/>の型</typeparam>
    public class NodeAsdXmlConverter<TNode> : DefaultAsdXmlConverter<TNode> where TNode : Node
    {
        /// <summary>
        /// <see cref="NodeAsdXmlConverter{TNode}"/>の新しいインスタンスを初期化する
        /// </summary>
        public NodeAsdXmlConverter() { }
        /// <inheritdoc/>
        public override bool CanParse(Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type), "引数がnullです");
            for (; type != null; type = type.BaseType)
                if (type == typeof(Node))
                    return true;
            return false;
        }
        /// <inheritdoc/>
        protected override void SetChildren(in TNode value, AsdXmlReader reader, IEnumerable<object> children)
        {
            if (value == null) return;
            foreach (var current in children)
                if (current is Node child)
                    value.AddChildNode(child);
        }
    }
}
