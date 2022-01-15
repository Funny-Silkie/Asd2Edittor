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
            switch (message)
            {
                case TypedMessage m:
                    switch (m.MessageType)
                    {
                        case MessageType.ClearText:
                            AssociatedObject.Clear();
                            break;
                        case MessageType.SaveText:
                            AssociatedObject.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                            RxMessanger.Default.Send(MessageType.OnSaveTextFinish);
                            break;
                        case MessageType.UpdateText:
                            AssociatedObject.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                            RxMessanger.Default.Send(MessageType.OnUpdateTextFinish);
                            break;
                    }
                    break;
                case DictionaryMessage m:
                    if (m.Values.TryGetValue("Type", out var _d_type))
                    {
                        if (_d_type is MessageType d_type && d_type == MessageType.GetTextBoxValue)
                        {
                            m.Values["Text"] = AssociatedObject.Text;
                            m.Values["Type"] = MessageType.OnGetTextBoxValueFinish;
                            RxMessanger.Default.Send(m.Values);
                            break;
                        }
                    }
                    break;
            }
        }
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.PreviewTextInput += OnPreviewTextInput;
            AssociatedObject.PreviewKeyDown += OnPreviewKeyDown;
            AssociatedObject.TextChanged += OnTextChanged;
        }
        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.PreviewTextInput -= OnPreviewTextInput;
            AssociatedObject.PreviewKeyDown -= OnPreviewKeyDown;
            AssociatedObject.TextChanged -= OnTextChanged;
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
                        case Key.Enter:
                            CtrlEnter();
                            break;
                    }
                    break;
                case ModifierKeys.None:
                    switch (e.Key)
                    {
                        case Key.Enter:
                            Enter();
                            e.Handled = true;
                            break;
                        case Key.Tab:
                            Tab();
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
        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            RxMessanger.Default.Send(("Type", MessageType.TextBoxChanged), ("Text", AssociatedObject.Text));
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
        private void CtrlEnter()
        {
            var (x, _) = AssociatedObject.GetCaretPosition();
            AssociatedObject.CaretIndex -= x;
            if (AssociatedObject.CaretIndex == 0) AssociatedObject.InsertText("\r\n");
            else
            {
                AssociatedObject.CaretIndex -= 2;
                Enter();
            }
        }
        private int Enter(bool tab = true)
        {
            var (x, y) = AssociatedObject.GetCaretPosition();
            var line = AssociatedObject.GetLineText(y);
            var tabbed = x > 0 && line[x - 1] == '>' && (x <= 1 || line[x - 2] != '/');
            var c = 0;
            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] != ' ') break;
                c++;
            }
            AssociatedObject.InsertText($"\r\n{new string(' ', c)}");
            AssociatedObject.CaretIndex += 2 + c;
            if (tab && tabbed)
            {
                Enter(false);
                AssociatedObject.CaretIndex -= c + 2;
                Tab();
            }
            return c;
        }
        private void Tab()
        {
            AssociatedObject.InsertText("    ");
            AssociatedObject.CaretIndex += 4;
        }
    }
}
