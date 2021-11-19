using fslib3;
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
            SetTextMembers(result, reader, xml.Members);
            var xmlMembers = xml.Children
                .Where(x =>
                {
                    var array = x.Name.Split('.');
                    return array.Length == 2 && array[0] == xml.Name;
                });
            SetXmlMembers(result, reader, xmlMembers);
            var children = xml.Children
                .Where(x => !x.Name.Contains('.'))
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
        /// メンバを設定する
        /// </summary>
        /// <param name="value">メンバの設定を行う<typeparamref name="T"/>のインスタンス</param>
        /// <param name="reader">使用する<see cref="AsdXmlReader"/>のインスタンス</param>
        /// <param name="members"><paramref name="value"/>に設定されるメンバのコレクション</param>
        protected virtual void SetTextMembers(in T value, AsdXmlReader reader, IDictionary<string, string> members)
        {
            foreach (var (fieldName, fieldString) in members)
            {
                var fieldInfo = typeof(T).GetField(fieldName, reflectionFlags);
                if (fieldInfo == null)
                {
                    var propertyInfo = typeof(T).GetProperty(fieldName, reflectionFlags);
                    var propertyConverter = reader.TextValueConverterProvider.GetConverter(propertyInfo.PropertyType);
                    if (!propertyConverter.Convert(fieldString, propertyInfo.PropertyType, out var propertyValue)) throw new XmlParseException("プロパティの復元に失敗しました");
                    propertyInfo.SetValue(value, propertyValue);
                }
                else
                {
                    var fieldConverter = reader.TextValueConverterProvider.GetConverter(fieldInfo.FieldType);
                    if (!fieldConverter.Convert(fieldString, fieldInfo.FieldType, out var fieldValue)) throw new XmlParseException("フィールドの復元に失敗しました");
                    fieldInfo.SetValue(value, fieldValue);
                }
            }
        }
        /// <summary>
        /// メンバを設定する
        /// </summary>
        /// <param name="value">メンバの設定を行う<typeparamref name="T"/>のインスタンス</param>
        /// <param name="reader">使用する<see cref="AsdXmlReader"/>のインスタンス</param>
        /// <param name="members"><paramref name="value"/>に設定されるメンバのコレクション</param>
        /// <exception cref="ArgumentException">メンバが存在しない，またはメンバがフィールド・プロパティ以外</exception>
        protected virtual void SetXmlMembers(in T value, AsdXmlReader reader, IEnumerable<XmlEntry> members)
        {
            foreach (var member in members)
            {
                var mInfo = typeof(T).GetMember(member.Name, reflectionFlags);
                if (mInfo.IsEmptyValue()) throw new ArgumentException("メンバが存在しません", nameof(members));
                switch (mInfo[0].MemberType)
                {
                    case MemberTypes.Property:
                        var property = (PropertyInfo)mInfo[0];
                        reader.AsdXmlConverterProvider.GetConverter(property.PropertyType).Convert(member, property.PropertyType, reader, out var p);
                        property.SetValue(value, p);
                        break;
                    case MemberTypes.Field:
                        var field = (FieldInfo)mInfo[0];
                        reader.AsdXmlConverterProvider.GetConverter(field.FieldType).Convert(member, field.FieldType, reader, out var f);
                        field.SetValue(value, f);
                        break;
                    default: throw new ArgumentException("メンバのタイプが無効です", nameof(members));
                }
            }
        }
    }
}
