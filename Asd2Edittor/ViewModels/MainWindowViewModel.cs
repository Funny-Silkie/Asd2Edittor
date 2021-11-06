using Asd2Edittor.Messangers;
using Asd2Edittor.Models;
using Asd2UI.Xml;
using fslib3.WPF;
using Microsoft.WindowsAPICodePack.Dialogs;
using Reactive.Bindings;
using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reactive.Linq;

namespace Asd2Edittor.ViewModels
{
    public class MainWindowViewModel : FsViewModelBase
    {
        private readonly FileSystemWatcher watcher = new FileSystemWatcher
        {
            IncludeSubdirectories = true
        };
        public static MainWindowViewModel Current { get; } = new MainWindowViewModel();
        public FilePathViewModel Root => Files.FirstOrDefault();
        public ReactiveCollection<FilePathViewModel> Files { get; } = new ReactiveCollection<FilePathViewModel>();
        public ReactiveProperty<string> Text { get; } = new ReactiveProperty<string>(default, ReactivePropertyMode.None);
        public ReactiveProperty<string> WatchPath { get; } = new ReactiveProperty<string>();
        public MainWindowViewModel()
        {
            WatchPath.Subscribe(OnWatchPathChanged);
            RxMessanger.Default.Subscribe(OnGetMessage);
            Common.SubscribeEvent<ErrorEventHandler, ErrorEventArgs>(x => x.Invoke, x => watcher.Error += x, x => watcher.Error -= x, x => throw new InvalidOperationException(x.ToString()));
            Common.SubscribeEvent<FileSystemEventHandler, FileSystemEventArgs>(x => x.Invoke, x => watcher.Created += x, x => watcher.Created -= x, WatcherCreated);
            Common.SubscribeEvent<FileSystemEventHandler, FileSystemEventArgs>(x => x.Invoke, x => watcher.Deleted += x, x => watcher.Deleted -= x, WatcherDeleted);
            Common.SubscribeEvent<RenamedEventHandler, RenamedEventArgs>(x => x.Invoke, x => watcher.Renamed += x, x => watcher.Renamed -= x, WatcherRenamed);
        }
        private FilePathViewModel FindFile(string path)
        {
            var names = Path.GetFullPath(path)
                .Replace('/', '\\')
                .Replace(Root.FullPath, string.Empty)
                .Split('\\')
                .ToArray();
            var fp = Root;
            var i = 0;
            while (true)
            {
                if (i < 0 || names.Length <= i) return fp;
                foreach (var current in fp.Children)
                {
                    if (current.Name.Value == names[i])
                    {
                        fp = current;
                        i++;
                        break;
                    }
                }
            }
        }
        private void OnCreateFile(string path)
        {
            var names = Path.GetFullPath(path)
                .Replace('/', '\\')
                .Replace(Root.FullPath, string.Empty)
                .Split('\\')
                .ToArray();
            if (names.Any(x => x.Contains(':'))) return;
            var i = 0;
            var fm = Root;
            while (true)
            {
                if (i < 0 || names.Length <= i) return;
                var child = fm.Children.FirstOrDefault(x => x.Name.Value == names[i]);
                if (child == null)
                {
                    var vm = new FilePathViewModel(this, names[i]);
                    fm.AddChild(vm);
                    if (vm.FullPath.EndsWith('\\')) vm.Reset(vm.FullPath);
                    return;
                }
                else
                {
                    i++;
                    fm = child;
                    continue;
                }
            }
        }
        private void OnDeleteFile(string path)
        {
            path = Path.GetFullPath(path)
                .Replace('/', '\\')
                .Replace(Root.FullPath, string.Empty);
            var vm = FindFile(path);
            if (vm == Root) return;
            vm.Parent.Value.RemoveChild(vm);
        }
        private void WatcherCreated(FileSystemEventArgs e) => OnCreateFile(e.FullPath);
        private void WatcherDeleted(FileSystemEventArgs e) => OnDeleteFile(e.FullPath);
        private void WatcherRenamed(RenamedEventArgs e)
        {
            OnCreateFile(e.FullPath);
            OnDeleteFile(e.OldFullPath);
        }
        private void OnWatchPathChanged(string value)
        {
            if (value == null) return;
            watcher.EnableRaisingEvents = false;
            var fp = Path.GetFullPath(value)
                .Replace('/', '\\')
                .TrimEnd('\\');
            Files.Clear();
            if (Root == null) Files.Add(new FilePathViewModel(this, fp.Split('\\')[^1]) { IsFolder = true });
            else Root.Name.Value = fp.Split('\\')[^1];
            watcher.Path = fp;
            Root.Reset(fp);
            watcher.EnableRaisingEvents = true;
        }
        private void OnGetMessage(MessageInfo info)
        {
            if (info is TypedMessage t)
            {
                switch (t.MessageType)
                {
                    case MessageType.OnFinishUpdateText:
                        var reader = new AsdXmlReader();
                        var entry = reader.ToXmlEntry(Text.Value);
                        break;
                }
            }
        }
        #region Commands
        protected override void InitializeCommands()
        {
            base.InitializeCommands();
            UpdateText.Subscribe(CommandUpdateText);
            MenuFileOpen.Subscribe(CommandMenuFileOpen);
            CloseWindow.Subscribe(CommandCloseWindow);
            OnWindowClosing.Subscribe(CommandOnWindowClosing);
        }
        public ReactiveCommand UpdateText { get; } = new ReactiveCommand();
        private void CommandUpdateText()
        {
            RxMessanger.Default.Send(MessageType.UpdateText);
        }
        public ReactiveCommand MenuFileOpen { get; } = new ReactiveCommand();
        private void CommandMenuFileOpen()
        {
            var dialog = new CommonOpenFileDialog
            {
                EnsurePathExists = true,
                IsFolderPicker = true,
                Multiselect = false,
            };
            if (dialog.ShowDialog() != CommonFileDialogResult.Ok) return;
            WatchPath.Value = dialog.FileName;
        }
        public ReactiveCommand CloseWindow { get; } = new ReactiveCommand();
        private void CommandCloseWindow()
        {
            RxMessanger.Default.Send(MessageType.CloseWindow);
        }
        public ReactiveCommand<CancelEventArgs> OnWindowClosing { get; } = new ReactiveCommand<CancelEventArgs>();
        private void CommandOnWindowClosing(CancelEventArgs value)
        {
            watcher.EnableRaisingEvents = false;
            watcher.Dispose();
        }
        #endregion
    }
}
