namespace Asd2Edittor.Messangers
{
    public abstract class MessangerBase
    {
        protected MessangerBase() { }
        public abstract void Send(MessageInfo massage);
    }
}
