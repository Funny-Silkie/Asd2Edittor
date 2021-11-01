using System;
using System.Collections.Generic;
using System.Linq;

namespace Asd2Edittor.Messangers
{
    public static class MessangerEx
    {
        public static void Send(this MessangerBase messanger, MessageType messageType)
        {
            if (messanger == null) throw new ArgumentNullException(nameof(messanger), "引数がnullです");
            messanger.Send(new TypedMessage(messanger, messageType));
        }
        public static void Send(this MessangerBase messanger, IDictionary<string, object> values)
        {
            if (messanger == null) throw new ArgumentNullException(nameof(messanger), "引数がnullです");
            messanger.Send(new DictionaryMessage(messanger, values));
        }
        public static void Send(this MessangerBase messanger, params ValueTuple<string, object>[] values)
        {
            if (messanger == null) throw new ArgumentNullException(nameof(messanger), "引数がnullです");
            messanger.Send(new DictionaryMessage(messanger, new Dictionary<string, object>(values.Select(x => new KeyValuePair<string, object>(x.Item1, x.Item2)))));
        }
    }
}
