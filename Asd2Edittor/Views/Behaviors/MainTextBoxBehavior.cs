using Asd2Edittor.Messangers;
using System.Windows.Controls;
using System.Windows.Input;

namespace Asd2Edittor.Views.Behaviors
{
    public class MainTextBoxBehavior : RxMessageBehavior<TextBox>
    {
        private string currentEntered;
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
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.PreviewTextInput += OnPreviewTextInput;
            AssociatedObject.KeyDown += KeyDown;
        }
        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.PreviewTextInput -= OnPreviewTextInput;
            AssociatedObject.KeyDown -= KeyDown;
        }
        private void OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            currentEntered = e.Text;
            switch (currentEntered)
            {
                case "<":
                case "/":
                    AssociatedObject.InsertText(">"); break;
                case "\"": AssociatedObject.InsertText("\""); break;
            }
        }
        private void KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyboardDevice.Modifiers)
            {
                case ModifierKeys.Control:
                    switch (e.Key)
                    {
                        case Key.D:
                            CtrlD();
                            break;
                    }
                    break;
            }
        }
        private void CtrlD()
        {
            var (x, y) = AssociatedObject.GetCaretPosition();
            var line = AssociatedObject.GetLineText(y);
            var endPos = AssociatedObject.CaretIndex + line.Length;
            AssociatedObject.BeginChange();
            AssociatedObject.CaretIndex += line.Length - x;
            AssociatedObject.InsertText(line);
            AssociatedObject.CaretIndex = endPos;
            AssociatedObject.EndChange();
        }
    }
}
