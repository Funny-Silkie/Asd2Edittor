using Asd2UI.Altseed2;
using Asd2UI.Xml.Converters;
using System;
using System.Collections.Generic;

namespace Asd2UI.Xml
{
    public class AsdXmlReader
    {
        private readonly List<XmlParseException> errors = new List<XmlParseException>();
        private Dictionary<string, string> usingMap;
        /// <summary>
        /// <see cref="AsdXmlConverter"/>のプロバイダを取得または設定する
        /// </summary>
        public AsdXmlConverterProvider AsdXmlConverterProvider { get; set; } = new DefaultAsdXmlConverterProvider();
        /// <summary>
        /// <see cref="TextValueConverter"/>のプロバイダを取得または設定する
        /// </summary>
        public TextValueConverterProvider TextValueConverterProvider { get; set; } = new DefaultTextValueConverterProvider();
        /// <summary>
        /// <see cref="AsdXmlReader"/>の新しいインスタンスを初期化する
        /// </summary>
        public AsdXmlReader()
        {

        }
        /// <summary>
        /// 読み取りを開始する
        /// </summary>
        /// <param name="xml">読み取るxmlの文字列</param>
        /// <exception cref="ArgumentException"><paramref name="xml"/>が空文字</exception>
        /// <exception cref="ArgumentNullException"><paramref name="xml"/>がnull</exception>
        /// <returns>読み込まれたxmlをもとにした<see cref="XmlEntry"/>のインスタンス</returns>
        public XmlEntry ToXmlEntry(string xml)
        {
            if (xml == null) throw new ArgumentNullException(nameof(xml), "引数がnullです");
            if (xml.Length == 0) throw new ArgumentException("空文字です", nameof(xml));
            errors.Clear();
            var content = false;
            var lines = xml.Split("\r\n");
            for (var i = 0; i < lines.Length; i++)
            {
                var line = lines[i].Trim();
                if (line.StartsWith("using:"))
                {
                    if (!content)
                    {
                        // ToDo using
                    }
                    else errors.Add(new XmlParseException("usingステートメントはドキュメントの頭のみにて有効です", i));
                    continue;
                }
                if (line.Length > 0)
                {
                    if (line[0] != '<')
                    {
                        errors.Add(new XmlParseException("無効な文法です", i));
                        continue;
                    }
                    var start = 0;
                    for (int j = 0; j < i; j++) start += 2 + lines[j].Length;
                    var units = StringHandler.GetXmlUnits(xml, start);
                    if (units.Length == 0) errors.Add(new XmlParseException("宣言がありません"));
                    if (units.Length > 1) errors.Add(new XmlParseException("宣言が多重にあります"));
                    return XmlEntry.Create(units[0]);
                }
            }
            errors.Add(new XmlParseException("宣言がありません"));
            return null;
        }
        /// <summary>
        /// <see cref="UINode"/>に変換する
        /// </summary>
        /// <param name="entry">読み込む<see cref="XmlEntry"/>のインスタンス</param>
        /// <exception cref="ArgumentNullException"><paramref name="entry"/>がnull</exception>
        /// <returns><paramref name="entry"/>をもとに生成された</returns>
        public UINode ToNode(XmlEntry entry)
        {
            if (entry == null) throw new ArgumentNullException(nameof(entry), "引数がnullです");
            var type = NameToType(entry.Name) ?? throw new ArgumentException("認識されていない型です", nameof(entry));
            var converter = AsdXmlConverterProvider.GetConverter(type) ?? throw new ArgumentException("コンバータを取得出来ませんでした", nameof(entry));
            converter.Convert(entry, type, this, out var result);
            return result as UINode;
        }
        /// <summary>
        /// 型名から実際の型を取得する
        /// </summary>
        /// <param name="name">型名</param>
        /// <returns><paramref name="name"/>に応じた型 見つからなかったら<see langword="null"/></returns>
        internal Type NameToType(string name)
        {
            switch (name)
            {
                case nameof(Circle): return typeof(Circle);
                case nameof(Line): return typeof(Line);
                case nameof(Rectangle): return typeof(Rectangle);
                case nameof(Triangle): return typeof(Triangle);
                default: return null;
            }
        }
    }
}
