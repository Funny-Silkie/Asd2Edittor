using Asd2UI.Altseed2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Asd2UI.Xml.Converters
{
    /// <summary>
    /// デフォルトの<see cref="AsdXmlConverter{T}"/>のクラス
    /// </summary>
    /// <typeparam name="T">変換先の型</typeparam>
    public class DefaultAsdXmlConverter<T> : AsdXmlConverter<T>
    {
        private const BindingFlags reflectionFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
        /// <summary>
        /// <see cref="DefaultAsdXmlConverter{T}"/>の新しいインスタンスを初期化する
        /// </summary>
        public DefaultAsdXmlConverter() { }
        /// <summary>
        /// <typeparamref name="T"/>の新しいインスタンスを生成する
        /// </summary>
        /// <param name="reader">使用する<see cref="AsdXmlReader"/>のインスタンス</param>
        /// <exception cref="InvalidOperationException"><typeparamref name="T"/>に引数無しコンストラクタが定義されていない</exception>
        /// <returns><typeparamref name="T"/>の新しいインスタンス</returns>
        protected virtual T CreateInstance(AsdXmlReader reader)
        {
            try
            {
                return Activator.CreateInstance<T>();
            }
            catch (MissingMemberException e)
            {
                throw new InvalidOperationException($"{typeof(T).FullName}の引数無しコンストラクタが見つかりませんでした", e);
            }
        }
        /// <inheritdoc/>
        public override bool Convert(XmlEntry xml, AsdXmlReader reader, out T result)
        {
            result = CreateInstance(reader);
            SetFields(result, reader, xml.Fields);
            var children = xml.Children
                .Select(x =>
                {
                    var childType = reader.NameToType(x.Name);
                    reader.AsdXmlConverterProvider.GetConverter(childType).Convert(x, childType, reader, out var ret);
                    return ret;
                });
            SetChildren(result, reader, children);
            return true;
        }
        /// <summary>
        /// 子要素の設定を行う
        /// </summary>
        /// <param name="value">子要素の設定を行う<typeparamref name="T"/>のインスタンス</param>
        /// <param name="reader">使用する<see cref="AsdXmlReader"/>のインスタンス</param>
        /// <param name="children"><paramref name="value"/>に設定される子要素のコレクション</param>
        /// <remarks>デフォルトだと何も行われない</remarks>
        protected virtual void SetChildren(in T value, AsdXmlReader reader, IEnumerable<object> children) { }
        /// <summary>
        /// フィールドを設定する
        /// </summary>
        /// <param name="value">フィールドの設定を行う<typeparamref name="T"/>のインスタンス</param>
        /// <param name="reader">使用する<see cref="AsdXmlReader"/>のインスタンス</param>
        /// <param name="fields"><paramref name="value"/>に設定されるフィールドのコレクション</param>
        protected virtual void SetFields(in T value, AsdXmlReader reader, IDictionary<string, string> fields)
        {
            foreach (var (fieldName, fieldString) in fields)
            {
                var fieldInfo = typeof(T).GetField(fieldName, reflectionFlags);
                var fieldConverter = reader.TextValueConverterProvider.GetConverter(fieldInfo.FieldType);
                if (!fieldConverter.Convert(fieldString, fieldInfo.FieldType, out var fieldValue)) throw new XmlParseException("フィールドの復元に失敗しました");
                fieldInfo.SetValue(value, fieldValue);
            }
        }
    }
}
