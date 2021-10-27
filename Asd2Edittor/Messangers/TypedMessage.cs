namespace Asd2Edittor.Messangers
{
    public sealed class TypedMessage : MessageInfo
    {
        public MessageType MessageType { get; }
        public TypedMessage(MessangerBase sender, MessageType type) : base(sender)
        {
            MessageType = type;
        }
    }
}
