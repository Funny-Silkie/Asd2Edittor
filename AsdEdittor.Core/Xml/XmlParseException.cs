using System;
using System.Runtime.Serialization;

namespace Asd2UI.Xml
{
    /// <summary>
    /// Xmlの読み取り中の例外を表すクラス
    /// </summary>
    [Serializable]
    public class XmlParseException : Exception
    {
        /// <summary>
        /// 行番号
        /// </summary>
        public int Line { get; set; }
        /// <inheritdoc/>
        public override string Message => Line < 0 ? base.Message : $"{Line}：{base.Message}";
        /// <summary>
        /// <see cref="XmlParseException"/>の新しいインスタンスを初期化する
        /// </summary>
        public XmlParseException() { }
        /// <summary>
        /// <see cref="XmlParseException"/>の新しいインスタンスを初期化する
        /// </summary>
        /// <param name="message">使用するメッセージ</param>
        /// <param name="line">行番号</param>
        public XmlParseException(string message, int line = -1) : base(message)
        {
            Line = line;
        }
        /// <summary>
        /// <see cref="XmlParseException"/>の新しいインスタンスを初期化する
        /// </summary>
        /// <param name="message">使用するメッセージ</param>
        /// <param name="line">行番号</param>
        /// <param name="inner">内部例外</param>
        public XmlParseException(string message, int line, Exception inner) : base(message, inner)
        {
            Line = line;
        }
        /// <summary>
        /// <see cref="XmlParseException"/>の新しいインスタンスを初期化する
        /// </summary>
        /// <param name="info">フィールド情報</param>
        /// <param name="context">使用するシリアライズ用のコンテキスト</param>
        protected XmlParseException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            Line = info.GetInt32(nameof(Line));
        }
        /// <inheritdoc/>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue(nameof(Line), Line);
        }
    }
}
