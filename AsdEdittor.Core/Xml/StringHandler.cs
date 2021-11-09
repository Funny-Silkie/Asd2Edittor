using System.Collections.Generic;

namespace Asd2UI.Xml
{
    /// <summary>
    /// 文字列を扱うクラス
    /// </summary>
    internal static class StringHandler
    {
        /// <summary>
        /// ダブルクオーテーションで囲まれた領域以外を分割する
        /// </summary>
        /// <param name="value">分割するテキスト</param>
        /// <param name="separator">区切り文字</param>
        /// <returns><paramref name="separator"/>で区切られた文字</returns>
        internal static List<string> SplitWithoutDoubleQuotation(this string value, char separator)
        {
            if (string.IsNullOrEmpty(value)) return new List<string>() { value };
            var result = new List<string>();
            var skip = false;
            var start = 0;
            for (int i = 0; i < value.Length; i++)
            {
                var c = value[i];
                switch (c)
                {
                    case '"':
                        skip = !skip;
                        break;
                    case char _c when _c == separator:
                        if (skip) break;
                        result.Add(value[start..i]);
                        start = i + 1;
                        break;
                }
            }
            result.Add(value[start..]);
            return result;
        }
        /// <summary>
        /// xmlのユニットに分割する
        /// </summary>
        /// <param name="xml">読み込むxml</param>
        /// <param name="start"><paramref name="xml"/>における読込開始インデックス</param>
        /// <returns><paramref name="xml"/>を分割したもの</returns>
        internal static string[] GetXmlUnits(string xml, int start)
        {
            var list = new List<string>();
            var nestLevel = 0;
            var lv0Start = 0;
            for (int i = start; i < xml.Length; i++)
            {
                var c = xml[i];
                switch (c)
                {
                    case '<':
                        var firstEnd = xml.IndexOf('>', i);
                        if ((firstEnd == 0 || xml[firstEnd - 1] != '/') && (i + 1 >= xml.Length || xml[i + 1] != '/'))
                        {
                            if (nestLevel == 0) lv0Start = i;
                            nestLevel++;
                            continue;
                        }
                        else if (nestLevel == 0 && firstEnd > 0 && xml[firstEnd - 1] == '/') lv0Start = i;
                        break;
                    case '>':
                        var lastStart = LastIndexOf(xml, '<', i);
                        if ((lastStart >= 0 && xml[lastStart + 1] == '/')) nestLevel--;
                        if (nestLevel == 0)
                        {
                            list.Add(xml[lv0Start..(i + 1)]);
                            continue;
                        }
                        break;
                }
            }
            return list.ToArray();
        }
        private static int LastIndexOf(string value, char searchFor, int start)
        {
            for (int i = start; i >= 0; i--)
                if (value[i] == searchFor)
                    return i;
            return -1;
        }
    }
}
