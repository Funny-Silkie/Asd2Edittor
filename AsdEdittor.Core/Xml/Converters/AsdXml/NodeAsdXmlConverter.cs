using Altseed2;
using Asd2UI.Altseed2;
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
        protected override void SetChildren(in object value, AsdXmlReader reader, IEnumerable<object> children)
        {
            base.SetChildren(value, reader, children);
            if (value == null) return;
            if (value is UINode ui)
            {
                foreach (var current in children)
                    if (current is Node child)
                        ui.InnerTransformNode.AddChildNode(child);
            }
            else
                foreach (var current in children)
                    if (current is Node child)
                        ((Node)value).AddChildNode(child);
        }
    }
}
