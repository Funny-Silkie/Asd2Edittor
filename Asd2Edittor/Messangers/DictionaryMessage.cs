using System.Collections.Generic;

namespace Asd2Edittor.Messangers
{
    public sealed class DictionaryMessage : MessageInfo
    {
        public IDictionary<string, object> Values { get; }
        public DictionaryMessage(MessangerBase sender, IDictionary<string, object> values) : base(sender)
        {
            Values = values;
        }
    }
}
