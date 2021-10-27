using Asd2Edittor.Messangers;
using fslib3;
using fslib3.WPF;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace Asd2Edittor.ViewModels
{
    public class MainWindowViewModel : FsViewModelBase
    {
        public ReactiveProperty<string> Text { get; } = new ReactiveProperty<string>();
        public MainWindowViewModel()
        {

        }
        #region Commands
        protected override void InitializeCommands()
        {
            base.InitializeCommands();
            CloseWindow.Subscribe(CommandCloseWindow);
        }
        public ReactiveCommand CloseWindow { get; } = new ReactiveCommand();
        private void CommandCloseWindow()
        {
            RxMessanger.Default.Send(MessageType.CloseWindow);
        }
        #endregion
    }
}
