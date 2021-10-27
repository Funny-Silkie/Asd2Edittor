using Asd2Edittor.Messangers;
using System.Windows.Controls;

namespace Asd2Edittor.Views.Behaviors
{
    public class MainTextBoxBehavior : RxMessageBehavior<TextBox>
    {
        public MainTextBoxBehavior()
        {

        }
        protected override void MessangerOnNext(MessageInfo message)
        {
            if (message is not TypedMessage m) return;
            switch (m.MessageType)
            {
                case MessageType.UpdateText:
                    AssociatedObject.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                    break;
            }
        }
    }
}
