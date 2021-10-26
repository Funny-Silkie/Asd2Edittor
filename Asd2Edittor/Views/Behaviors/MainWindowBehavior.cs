using Asd2Edittor.Models;
using fslib3;
using fslib3.WPF;
using fslib3.WPF.Behaviors;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Reactive;
using System.Windows;
using System.Windows.Input;

namespace Asd2Edittor.Views.Behaviors
{
    public class MainWindowBehavior : BehaviorBase<MainWindow>
    {
        public MainWindowBehavior()
        {
            InitializeCommands();
        }
        #region Commands
        private void InitializeCommands()
        {
            CloseWindow = new ReactiveCommand();
            CloseWindow.Subscribe(CommandCloseWindow);
        }
        public ReactiveCommand CloseWindow { get; private set; }
        private void CommandCloseWindow()
        {
            AssociatedObject.Close();
        }
        #endregion
    }
}
