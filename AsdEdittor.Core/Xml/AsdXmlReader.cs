using Altseed2;
using Asd2UI.Altseed2;
using Asd2UI.Xml.Converters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Asd2UI.Xml
{
    public class AsdXmlReader
    {
        private readonly List<XmlParseException> errors = new List<XmlParseException>();
        private Dictionary<string, string> usingMap;
        /// <summary>
        /// <see cref="AsdXmlConverter"/>のプロバイダを取得または設定する
        /// </summary>
        public IAsdXmlConverterProvider Provider { get; set; }
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
        /// <returns>読み込まれたxmlをもとにした<see cref="UINode"/>のインスタンス</returns>
        public UINode Read(string xml) => ToNode(ReadPrivate(xml));
        private XmlEntry ReadPrivate(string xml)
        {
            if (xml == null) throw new ArgumentNullException(nameof(xml), "引数がnullです");
            if (xml.Length == 0) throw new ArgumentException("空文字です", nameof(xml));
            errors.Clear();
            var content = false;
            var name = string.Empty;
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
                    var units = GetXmlUnits(xml, start);
                }
            }
            return default;
        }
        private static string[] GetXmlUnits(string xml, int start)
        {
            var list = new List<string>();
            var nestLevel = 0;
            var lv0Start = 0;
            for (int i = start; i < xml.Length; i++)
            {
                var c = xml[i];
                if (c == '<')
                {
                    var firstEnd = xml.IndexOf('>', i);
                    if ((firstEnd > 0 && xml[firstEnd - 1] == '/') || (i + 1 < xml.Length && xml[i + 1] == '/'))
                    {
                        if (nestLevel == 0) lv0Start = i;
                        nestLevel++;
                        continue;
                    }
                }
                if (c == '>')
                {
                    var lastStart = xml.LastIndexOf('<', i);
                    if ((lastStart >= 0 && xml[lastStart + 1] == '/') || (i > 0 && xml[i - 1] == '/'))
                    {
                        if (nestLevel == 0)
                        {
                            list.Add(xml[lv0Start..(i + 1)]);
                            continue;
                        }
                        else nestLevel--;
                    }
                }
            }
            return list.ToArray();
        }
        private UINode ToNode(XmlEntry entry)
        {
            return default;
        }
    }
}
