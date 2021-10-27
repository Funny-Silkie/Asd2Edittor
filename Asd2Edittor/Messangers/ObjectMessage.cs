namespace Asd2Edittor.Messangers
{
    public sealed class ObjectMessage : MessageInfo
    {
        public object Message { get; }
        public ObjectMessage(MessangerBase sender, object message) : base(sender)
        {
            Message = message;
        }
    }
}
