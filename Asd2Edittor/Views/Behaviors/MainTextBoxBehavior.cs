using Asd2Edittor.Messangers;
using System;
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
        }
        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.PreviewTextInput -= OnPreviewTextInput;
        }
        private void OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            currentEntered = e.Text;
            switch (currentEntered)
            {
                case "<": InsertText(">"); break;
                case "\"": InsertText("\""); break;
            }
        }
        private void InsertText(string inserted)
        {
            var position = AssociatedObject.SelectionStart;
            var length = AssociatedObject.SelectionLength;
            var text = AssociatedObject.Text;
            AssociatedObject.BeginChange();
            AssociatedObject.Clear();
            AssociatedObject.AppendText(text[0..position]);
            AssociatedObject.AppendText(inserted);
            AssociatedObject.AppendText(text[position..]);
            AssociatedObject.EndChange();
            AssociatedObject.SelectionStart = position;
        }
    }
}
