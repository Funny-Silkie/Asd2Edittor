using System;

namespace Asd2Edittor.Messangers
{
    public abstract class MessageInfo
    {
        public MessangerBase Sender { get; }
        protected MessageInfo(MessangerBase sender)
        {
            Sender = sender ?? throw new ArgumentNullException(nameof(sender), "引数がnullです");
        }
    }
}
