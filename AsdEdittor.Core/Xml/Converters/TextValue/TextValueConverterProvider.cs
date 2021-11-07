using System;

namespace Asd2UI.Xml.Converters
{
    /// <summary>
    /// <see cref="TextValueConverter"/>を提供する基底クラス
    /// </summary>
    public abstract class TextValueConverterProvider
    {
        /// <summary>
        /// <see cref="TextValueConverterProvider"/>の新しいインスタンスを初期化する
        /// </summary>
        protected TextValueConverterProvider() { }
        /// <summary>
        /// 型に応じた<see cref="TextValueConverter"/>を取得する
        /// </summary>
        /// <param name="type">変換する要素の型</param>
        /// <exception cref="ArgumentNullException"><paramref name="type"/>がnull</exception>
        /// <returns><paramref name="type"/>に対応する<see cref="TextValueConverter"/>のインスタンス<br/>取得出来なかったら<see langword="null"/></returns>
        public abstract TextValueConverter GetConverter(Type type);
    }
}
