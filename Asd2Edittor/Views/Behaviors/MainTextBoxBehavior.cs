using Asd2Edittor.Messangers;
using System.Windows.Controls;
using System.Windows.Input;

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
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.PreviewTextInput += OnPreviewTextInput;
            AssociatedObject.PreviewKeyDown += OnPreviewKeyDown;
        }
        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.PreviewTextInput -= OnPreviewTextInput;
            AssociatedObject.PreviewKeyDown -= OnPreviewKeyDown;
        }
        private void OnPreviewKeyDown(object sender, KeyEventArgs e)
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
                case ModifierKeys.None:
                    switch (e.Key)
                    {
                        case Key.Enter:
                            var (x, y) = AssociatedObject.GetCaretPosition();
                            var line = AssociatedObject.GetLineText(y);
                            var c = 0;
                            for (int i = 0; i < line.Length; i++)
                            {
                                if (line[i] != ' ') break;
                                c++;
                            }
                            AssociatedObject.InsertText($"\r\n{new string(' ', c)}");
                            AssociatedObject.CaretIndex += 2 + c;
                            e.Handled = true;
                            break;
                        case Key.Tab:
                            AssociatedObject.InsertText("    ");
                            AssociatedObject.CaretIndex += 4;
                            e.Handled = true;
                            break;
                    }
                    break;
            }
        }
        private void OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            switch (e.Text)
            {
                case "/":
                    AssociatedObject.InsertText("/>");
                    AssociatedObject.CaretIndex += 2;
                    e.Handled = true;
                    break;
                case "\"": AssociatedObject.InsertText("\""); break;
                case "{": AssociatedObject.InsertText("}"); break;
                case ">":
                    var start = AssociatedObject.CaretIndex;
                    for (int i = start - 1; i >= 0; i--)
                        if (AssociatedObject.Text[i] == '<')
                        {
                            start = i;
                            break;
                        }
                    var count = 0;
                    for (int i = start + 1; i < AssociatedObject.CaretIndex; i++)
                    {
                        if (AssociatedObject.Text[i] is '>' or ' ') break;
                        if (AssociatedObject.Text[i] == '/') return;
                        count++;
                    }
                    if (count == 0) return;
                    var name = AssociatedObject.Text.Substring(start + 1, count);
                    AssociatedObject.InsertText($"></{name}>");
                    AssociatedObject.CaretIndex++;
                    e.Handled = true;
                    break;
                case "=":
                    AssociatedObject.InsertText("=\"\"");
                    AssociatedObject.CaretIndex += 2;
                    e.Handled = true;
                    break;
                case "\r\n":

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
