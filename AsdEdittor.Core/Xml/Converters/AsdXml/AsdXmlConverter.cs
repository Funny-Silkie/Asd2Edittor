using System;
using System.ComponentModel;

namespace Asd2UI.Xml.Converters
{
    /// <summary>
    /// xmlを特定の型に変換するパーザのクラス
    /// </summary>
    public abstract class AsdXmlConverter
    {
        /// <summary>
        /// <see cref="AsdXmlConverter"/>の新しいインスタンスを初期化する
        /// </summary>
        protected AsdXmlConverter() { }
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
        /// <param name="xml">変換するxmlのエントリー</param>
        /// <param name="resultType">変換先の型</param>
        /// <param name="reader">読み取り中の<see cref="AsdXmlReader"/>のインスタンス</param>
        /// <param name="result">変換された値</param>
        /// <exception cref="ArgumentNullException"><paramref name="xml"/>または<paramref name="resultType"/>がnull</exception>
        /// <returns><paramref name="xml"/>を変換出来たらtrue，それ以外でfalse</returns>
        public abstract bool Convert(XmlEntry xml, Type resultType, AsdXmlReader reader, out object result);
    }
    /// <summary>
    /// xmlを特定の型に変換するパーザのクラス
    /// </summary>
    /// <typeparam name="T">変換先の型</typeparam>
    public abstract class AsdXmlConverter<T> : AsdXmlConverter
    {
        /// <summary>
        /// <see cref="AsdXmlConverter{T}"/>の新しいインスタンスを初期化する
        /// </summary>
        protected AsdXmlConverter() { }
        /// <inheritdoc/>
        public override bool CanParse(Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type), "引数がnullです");
            return type == typeof(T);
        }
        /// <summary>
        /// 変換を行う
        /// </summary>
        /// <param name="xml">変換するxmlのエントリー</param>
        /// <param name="reader">読み取り中の<see cref="AsdXmlReader"/>のインスタンス</param>
        /// <param name="result">変換された値</param>
        /// <exception cref="ArgumentNullException"><paramref name="xml"/>がnull</exception>
        /// <returns><paramref name="xml"/>を変換出来たらtrue，それ以外でfalse</returns>
        public abstract bool Convert(XmlEntry xml, AsdXmlReader reader, out object result);
        /// <inheritdoc/>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool Convert(XmlEntry xml, Type resultType, AsdXmlReader reader, out object result)
        {
            if (xml == null) throw new ArgumentNullException(nameof(xml), "引数がnullです");
            if (resultType == null) throw new ArgumentNullException(nameof(resultType), "引数がnullです");
            if (!CanParse(resultType))
            {
                result = null;
                return false;
            }
            var ret = Convert(xml, reader, out var t);
            result = t;
            return ret;
        }
    }
}
