using Asd2Edittor.ViewModels;
using System;
using System.Collections.Generic;
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
        public MainWindow()
        {
            InitializeComponent();
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
