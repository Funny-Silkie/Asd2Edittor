using Asd2Edittor.Messangers;

namespace Asd2Edittor.Views.Behaviors
{
    public class MainWindowBehavior : RxMessageBehavior<MainWindow>
    {
        public MainWindowBehavior()
        {

        }
        #region Messanger
        protected override void MessangerOnNext(MessageInfo message)
        {
            if (message is not TypedMessage m) return;
            switch (m.MessageType)
            {
                case MessageType.CloseWindow:
                    AssociatedObject.Close();
                    break;
            }
        }
        #endregion
    }
}
