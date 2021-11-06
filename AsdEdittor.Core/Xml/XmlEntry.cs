using System;
using System.Collections.Generic;

namespace Asd2UI.Xml
{
    /// <summary>
    /// 解析したxmlのエントリのクラス
    /// </summary>
    public sealed class XmlEntry
    {
        /// <summary>
        /// 子要素を取得する
        /// </summary>
        public IList<XmlEntry> Children { get; }
        /// <summary>
        /// フィールドを取得する
        /// </summary>
        public IDictionary<string, string> Fields { get; }
        /// <summary>
        /// 名前を取得する
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// <see cref="XmlEntry"/>の新しいインスタンスを初期化する
        /// </summary>
        /// <param name="name">名前</param>
        /// <exception cref="ArgumentException"><paramref name="name"/>が空文字</exception>
        /// <exception cref="ArgumentNullException"><paramref name="name"/>がnull</exception>
        public XmlEntry(string name)
        {
            if (name == null) throw new ArgumentNullException(nameof(name), "引数がnullです");
            if (name.Length == 0) throw new ArgumentException("空文字です", nameof(name));
            Name = name;
            Children = new List<XmlEntry>();
            Fields = new Dictionary<string, string>();
        }
        /// <summary>
        /// <see cref="XmlEntry"/>の新しいインスタンスを初期化する
        /// </summary>
        /// <param name="name">名前</param>
        /// <param name="children">子要素</param>
        /// <param name="fields">フィールド</param>
        /// <exception cref="ArgumentException"><paramref name="name"/>が空文字</exception>
        /// <exception cref="ArgumentNullException"><paramref name="name"/>がnull</exception>
        public XmlEntry(string name, IList<XmlEntry> children, IDictionary<string, string> fields)
        {
            if (name == null) throw new ArgumentNullException(nameof(name), "引数がnullです");
            if (name.Length == 0) throw new ArgumentException("空文字です", nameof(name));
            Name = name;
            Children = children ?? new List<XmlEntry>();
            Fields = fields ?? new Dictionary<string, string>();
        }
        /// <summary>
        /// xmlのテキストから<see cref="XmlEntry"/>の新しいインスタンスを生成する
        /// </summary>
        /// <param name="xml">読み込むxml</param>
        /// <returns><paramref name="xml"/>をもとに生成された<see cref="XmlEntry"/>の新しいインスタンス<</returns>
        internal static XmlEntry Create(string xml)
        {
            var headEnd = xml.IndexOf('>');
            var single = headEnd - 1 >= 0 && xml[headEnd - 1] == '/';
            var tailStart = xml.LastIndexOf('<');
            var head = single ? xml[1..(headEnd - 1)] : xml[1..headEnd];
            var tail = single ? xml[(tailStart + 1)..^2].TrimStart('/') : xml[(tailStart + 1)..^1].TrimStart('/');
            var values = head.SplitWithoutDoubleQuotation(' ');
            var name = values[0];
            if (head != tail && name != tail) throw new XmlParseException("示しているアイテムが異なります");
            var result = new XmlEntry(name);
            for (int i = 1; i < values.Count; i++)
            {
                var attribute = values[i].SplitWithoutDoubleQuotation('=');
                if (attribute.Count != 2) throw new XmlParseException("フィールドの記法が無効です");
                result.Fields.Add(attribute[0], attribute[1].Trim('"'));
            }
            if (name == tail)
            {
                var children = StringHandler.GetXmlUnits(xml[(headEnd + 1)..tailStart].Trim(), 0);
                foreach (var child in children) result.Children.Add(Create(child));
            }
            return result;
        }
    }
}
