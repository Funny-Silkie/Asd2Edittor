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
    }
}
