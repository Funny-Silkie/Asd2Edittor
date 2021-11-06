using Asd2Edittor.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
            {
                var p = Process.Start(new ProcessStartInfo
                {
                    FileName = "AsdViewer.exe",

                    // HiddenとしたいがMainWindowHandleが0になる
                    // FindWindowで探す必要あり 面倒なのでMinimized
                    //WindowStyle = ProcessWindowStyle.Hidden,
                    WindowStyle = ProcessWindowStyle.Minimized,
                });
                p.WaitForInputIdle();

                var style = GetWindowLong(p.MainWindowHandle, GWL_STYLE);
                style = style & ~WS_CAPTION & ~WS_THICKFRAME;
                //style |= WS_CHILD; // メニュー有無
                SetWindowLong(p.MainWindowHandle, GWL_STYLE, style);

                SetParent(p.MainWindowHandle, asdViewer.Handle);

                var errCode = Marshal.GetLastWin32Error();
                var message = new StringBuilder(255);

                FormatMessage(FORMAT_MESSAGE_FROM_SYSTEM, IntPtr.Zero, (uint)errCode, 0, message, message.Capacity, IntPtr.Zero);
                MessageBox.Show(message.ToString());

                asdViewer.SizeChanged += (s, e) => MoveWindow(p.MainWindowHandle, 0, 0, asdViewer.Width, asdViewer.Height, 1);
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
