using System;
using System.ComponentModel;

namespace Asd2UI.Xml.Converters
{
    /// <summary>
    /// 文字列を特定の型に変換するパーザのクラス
    /// </summary>
    public abstract class TextValueConverter
    {
        /// <summary>
        /// <see cref="TextValueConverter"/>の新しいインスタンスを初期化する
        /// </summary>
        protected TextValueConverter() { }
        /// <summary>
        /// 指定した型に変換可能かどうかを取得する
        /// </summary>
        /// <param name="type">変換先の型</param>
        /// <exception cref="ArgumentNullException"><paramref name="type"/>がnull</exception>
        /// <returns><paramref name="type"/>が変換可能だったらtrue，それ以外でfalse</returns>
        public abstract bool CanParse(Type type);
        /// <summary>
        /// 変換を行う
        /// </summary>
        /// <param name="value">変換する文字列</param>
        /// <param name="resultType">変換先の型</param>
        /// <param name="result">変換された値</param>
        /// <exception cref="ArgumentNullException"><paramref name="value"/>または<paramref name="resultType"/>がnull</exception>
        /// <returns><paramref name="value"/>を変換出来たらtrue，それ以外でfalse</returns>
        public abstract bool Convert(string value, Type resultType, out object result);
    }
    /// <summary>
    /// 文字列を特定の型に変換するパーザのクラス
    /// </summary>
    /// <typeparam name="T">変換先の型</typeparam>
    public abstract class TextValueConverter<T> : TextValueConverter
    {
        /// <summary>
        /// <see cref="TextValueConverter{T}"/>の新しいインスタンスを初期化する
        /// </summary>
        protected TextValueConverter() { }
        /// <inheritdoc/>
        public override bool CanParse(Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type), "引数がnullです");
            return type == typeof(T);
        }
        /// <summary>
        /// 変換を行う
        /// </summary>
        /// <param name="value">変換する文字列</param>
        /// <param name="result">変換された値</param>
        /// <exception cref="ArgumentNullException"><paramref name="value"/>がnull</exception>
        /// <returns><paramref name="value"/>を変換出来たらtrue，それ以外でfalse</returns>
        public abstract bool Convert(string value, out T result);
        /// <inheritdoc/>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool Convert(string value, Type resultType, out object result)
        {
            if (value == null) throw new ArgumentNullException(nameof(value), "引数がnullです");
            if (resultType == null) throw new ArgumentNullException(nameof(resultType), "引数がnullです");
            if (!CanParse(resultType))
            {
                result = null;
                return false;
            }
            var ret = Convert(value, out var t);
            result = t;
            return ret;
        }
    }
}
