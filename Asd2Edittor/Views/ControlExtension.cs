namespace System.Windows.Controls
{
    public static class ControlExtension
    {
        public static (int x, int y) GetCaretPosition(this TextBox textBox)
        {
            if (textBox == null) throw new ArgumentNullException(nameof(textBox), "引数がnullです");
            var position = textBox.CaretIndex;
            var lastNewLine = 0;
            var y = 0;
            for (int i = 0; i + 1 < position; i++)
            {
                var c1 = textBox.Text[i];
                var c2 = textBox.Text[i + 1];
                if (c1 == '\r' && c2 == '\n')
                {
                    y++;
                    lastNewLine = i + 2;
                }
            }
            return (position - lastNewLine, y);
        }
        public static void InsertText(this TextBox textBox, string inserted)
        {
            var position = textBox.SelectionStart;
            var length = textBox.SelectionLength;
            var text = textBox.Text;
            textBox.BeginChange();
            textBox.Clear();
            textBox.AppendText(text[0..position]);
            textBox.AppendText(inserted);
            textBox.AppendText(text[position..]);
            textBox.EndChange();
            textBox.SelectionStart = position;
        }
    }
}
