using System;

namespace Asd2UI.Xml
{
    /// <summary>
    /// リフレクションを扱うクラス
    /// </summary>
    internal static class ReflectionHelper
    {
        /// <summary>
        /// 指定した型を継承しているかどうかを評価する
        /// </summary>
        /// <typeparam name="T">継承する側の型</typeparam>
        /// <typeparam name="TBase">継承される側の型</typeparam>
        /// <returns><typeparamref name="T"/>が<typeparamref name="TBase"/>を継承していたらtrue，それ以外でfalse</returns>
        internal static bool IsBaseType<T, TBase>() => IsBaseType(typeof(T), typeof(TBase));
        /// <summary>
        /// 指定した型を継承しているかどうかを評価する
        /// </summary>
        /// <param name="type">継承する側の型</param>
        /// <typeparam name="TBase">継承される側の型</typeparam>
        /// <exception cref="ArgumentNullException"><paramref name="type"/>がnull</exception>
        /// <returns><paramref name="type"/>が<typeparamref name="TBase"/>を継承していたらtrue，それ以外でfalse</returns>
        internal static bool IsBaseType<TBase>(this Type type) => IsBaseType(type, typeof(TBase));
        /// <summary>
        /// 指定した型を継承しているかどうかを評価する
        /// </summary>
        /// <param name="type">継承する側の型</param>
        /// <param name="baseType">継承される側の型</param>
        /// <exception cref="ArgumentNullException"><paramref name="type"/>または<paramref name="baseType"/>がnull</exception>
        /// <returns><paramref name="type"/>が<paramref name="baseType"/>を継承していたらtrue，それ以外でfalse</returns>
        internal static bool IsBaseType(this Type type, Type baseType)
        {
            if (type == null) throw new ArgumentNullException(nameof(type), "引数がnullです");
            if (baseType == null) throw new ArgumentNullException(nameof(baseType), "引数がnullです");
            for (; type != null; type = type.BaseType)
                if (type == baseType)
                    return true;
            return false;
        }
        /// <summary>
        /// 指定したインターフェイスを実装しているかどうかを評価する
        /// </summary>
        /// <typeparam name="TInterface">インターフェイスの型</typeparam>
        /// <param name="type">実装する側の型</param>
        /// <param name="byDefType">ジェネリックを抜きで検証するかどうか</param>
        /// <exception cref="ArgumentNullException"><paramref name="type"/>がnull</exception>
        /// <returns><typeparamref name="TInterface"/>が実装されていたらtrue，それ以外でfalse</returns>
        internal static bool HasInterface<TInterface>(this Type type, bool byDefType) => HasInterface(type, typeof(TInterface), byDefType);
        /// <summary>
        /// 指定したインターフェイスを実装しているかどうかを評価する
        /// </summary>
        /// <param name="type">実装する側の型</param>
        /// <param name="interfaceType">インターフェイスの型</param>
        /// <param name="byDefType">ジェネリックを抜きで検証するかどうか</param>
        /// <exception cref="ArgumentNullException"><paramref name="type"/>または<paramref name="interfaceType"/>がnull</exception>
        /// <returns><paramref name="interfaceType"/>が実装されていたらtrue，それ以外でfalse</returns>
        internal static bool HasInterface(this Type type, Type interfaceType, bool byDefType)
        {
            if (type == null) throw new ArgumentNullException(nameof(type), "引数がnullです");
            if (interfaceType == null) throw new ArgumentNullException(nameof(interfaceType), "引数がnullです");
            foreach (var current in type.GetInterfaces())
            {
                var c = current;
                if (byDefType) c = c.GetGenericTypeDefinition();
                if (c == interfaceType)
                    return true;
            }
            return false;
        }
        /// <summary>
        /// ジェネリック型のインスタンスを生成する
        /// </summary>
        /// <param name="instanceType">インスタンスを生成する型</param>
        /// <param name="genericTypes">ジェネリックパラメータ</param>
        /// <exception cref="ArgumentNullException"><paramref name="instanceType"/>または<paramref name="genericTypes"/>がnull</exception>
        /// <returns><paramref name="instanceType"/>型の新しいインスタンス</returns>
        internal static object CreateGenericInstance(Type instanceType, params Type[] genericTypes)
        {
            if (instanceType == null) throw new ArgumentNullException(nameof(instanceType), "引数がnullです");
            if (genericTypes == null) throw new ArgumentNullException(nameof(genericTypes), "引数がnullです");
            return Activator.CreateInstance(instanceType.MakeGenericType(genericTypes));
        }
    }
}
