using System;

namespace Asd2UI.Xml.Converters
{
    /// <summary>
    /// <see cref="AsdXmlConverter"/>を提供するインターフェイス
    /// </summary>
    public interface IAsdXmlConverterProvider
    {
        /// <summary>
        /// 型に応じた<see cref="AsdXmlConverter"/>を取得する
        /// </summary>
        /// <param name="type">変換する要素の型</param>
        /// <exception cref="ArgumentNullException"><paramref name="type"/>がnull</exception>
        /// <returns><paramref name="type"/>に対応する<see cref="AsdXmlConverter"/>のインスタンス<br/>取得出来なかったら<see langword="null"/></returns>
        public abstract AsdXmlConverter GetConverter(Type type);
    }
}
