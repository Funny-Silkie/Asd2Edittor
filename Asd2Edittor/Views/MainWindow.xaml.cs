using Asd2Edittor.Altseed2;
using Asd2Edittor.ViewModels;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Asd2Edittor.Views
{
    public partial class MainWindow : Window
    {
        [DllImport("user32")]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
        [DllImport("user32")]
        private static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
        [DllImport("user32")]
        private static extern int MoveWindow(IntPtr hwnd, int x, int y, int nWidth, int nHeight, int bRepaint);

        [DllImport("kernel32.dll")]
        static extern uint FormatMessage(
          uint dwFlags, IntPtr lpSource,
          uint dwMessageId, uint dwLanguageId,
          StringBuilder lpBuffer, int nSize,
          IntPtr Arguments);
        private const uint FORMAT_MESSAGE_FROM_SYSTEM = 0x00001000;

        private const int GWL_STYLE = -16;
        private const int WS_THICKFRAME = 0x00040000;
        private const int WS_CAPTION = 0x00C00000;
        private const int WS_CHILD = 0x40000000;
        public MainWindow()
        {
            InitializeComponent();
            if (AltseedManager.Current.Initialize(726, 500))
            {
                var p = Process.GetProcessesByName("Asd2Edittor")[0];
                p.WaitForInputIdle();
                Thread.Sleep(100);

                var style = GetWindowLong(p.MainWindowHandle, GWL_STYLE);
                style = style & ~WS_CAPTION & ~WS_THICKFRAME;
                //style |= WS_CHILD; // メニュー有無
                SetWindowLong(p.MainWindowHandle, GWL_STYLE, style);

                SetParent(p.MainWindowHandle, asdViewer.Handle);

                AltseedManager.Current.Loop();

                asdViewer.SizeChanged += (s, e) => MoveWindow(p.MainWindowHandle, 0, 0, asdViewer.Width, asdViewer.Height, 1);
                Closed += (x, y) => p.Kill();
            }
        }
        private void TreeViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
        private void TreeViewItem_KeyDown(object sender, KeyEventArgs e)
        {
            var treeViewItem = (TreeViewItem)sender;
            var dataContext = (FilePathViewModel)treeViewItem.DataContext;
            if (e.Key == Key.Delete)
            {
                dataContext.DeleteFile.Execute();
            }
        }
    }
}
