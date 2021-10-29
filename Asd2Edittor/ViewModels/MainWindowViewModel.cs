using Asd2Edittor.Messangers;
using fslib3;
using fslib3.WPF;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Input;

namespace Asd2Edittor.ViewModels
{
    public class MainWindowViewModel : FsViewModelBase
    {
        private readonly FileSystemWatcher watcher = new FileSystemWatcher();
        public FilePathViewModel Root { get; }
        public ReactiveProperty<string> Text { get; } = new ReactiveProperty<string>();
        private ReactiveProperty<string> WatchPath { get; } = new ReactiveProperty<string>("./");
        public MainWindowViewModel()
        {
            //Text.Subscribe(x => MessageBox.Show(x));
            WatchPath.Subscribe(OnWatchPathChanged);
            Observable.FromEventPattern<FileSystemEventHandler, FileSystemEventArgs>(
                x => x.Invoke,
                x => watcher.Created += x,
                x => watcher.Created -= x
            )
            .Select(x => x.EventArgs)
            .Subscribe(WatcherCreated);
            Observable.FromEventPattern<RenamedEventHandler, RenamedEventArgs>(
                x => x.Invoke,
                x => watcher.Renamed += x,
                x => watcher.Renamed -= x
            )
            .Select(x => x.EventArgs)
            .Subscribe(WatcherRenamed);
        }
        private FilePathViewModel FindFile(string path)
        {
            var names = Path.GetFullPath(path)
                .Split('\\')
                .SkipWhile(x => x == WatchPath.Value)
                .ToArray();
            var fp = Root;
            for (int i = 0; i < names.Length; i++)
                if (names[i] == )
        }
        private void WatcherCreated(FileSystemEventArgs e)
        {

        }
        private void WatcherRenamed(RenamedEventArgs e)
        {

        }
        private void OnWatchPathChanged(string value)
        {
            watcher.Path = value;
        }
        #region Commands
        protected override void InitializeCommands()
        {
            base.InitializeCommands();
            UpdateText.Subscribe(CommandUpdateText);
            CloseWindow.Subscribe(CommandCloseWindow);
        }
        public ReactiveCommand UpdateText { get; } = new ReactiveCommand();
        private void CommandUpdateText()
        {
            RxMessanger.Default.Send(MessageType.UpdateText);
        }
        public ReactiveCommand CloseWindow { get; } = new ReactiveCommand();
        private void CommandCloseWindow()
        {
            RxMessanger.Default.Send(MessageType.CloseWindow);
        }
        #endregion
    }
    public class FilePathViewModel : FsViewModelBase
    {
        public ReactiveCollection<FilePathViewModel> Children { get; } = new ReactiveCollection<FilePathViewModel>();
        public ReactiveProperty<string> Name { get; } = new ReactiveProperty<string>();
        public FilePathViewModel(string name)
        {
            Name.Value = name;
        }
        public FilePathViewModel FindChild(string childName)
        {
            for (int i = 0; i < Children.Count; i++)
                if (Children[i].Name.Value == childName)
                    return Children[i];
            return null;
        }
        protected override void InitializeCommands()
        {
            base.InitializeCommands();
        }
    }
}
