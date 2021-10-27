using System;

namespace Asd2Edittor.Messangers
{
    public static class MessangerEx
    {
        public static void Send(this MessangerBase messanger, MessageType messageType)
        {
            if (messanger == null) throw new ArgumentNullException(nameof(messanger), "引数がnullです");
            messanger.Send(new TypedMessage(messanger, messageType));
        }
    }
}
