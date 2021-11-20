using Asd2Edittor.Messangers;
using Asd2Edittor.Models;
using fslib3.WPF;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Asd2Edittor.ViewModels
{
    public class FilePathViewModel : FsViewModelBase
    {
        private static readonly Comparer<FilePathViewModel> comparer;
        private readonly MainWindowViewModel main;
        public ReactiveCollection<FilePathViewModel> Children { get; } = new ReactiveCollection<FilePathViewModel>();
        public bool IsFolder { get; set; }
        public ReactiveProperty<string> Name { get; } = new ReactiveProperty<string>();
        public ReadOnlyReactiveProperty<FilePathViewModel> Parent { get; }
        private readonly ReactiveProperty<FilePathViewModel> _Parent = new ReactiveProperty<FilePathViewModel>();
        public string FullPath => $"{MainWindowViewModel.Current.WatchPath.Value}\\{string.Join('\\', EnumerateAncestors().SkipLast(1).Reverse())}";
        static FilePathViewModel()
        {
            comparer = Comparer<FilePathViewModel>.Create((x, y) => string.Compare(x?.Name?.Value, y?.Name?.Value));
        }
        public FilePathViewModel(MainWindowViewModel main, string name)
        {
            this.main = main;
            Name.Value = name;
            Parent = _Parent.ToReadOnlyReactiveProperty();
        }
        private IEnumerable<string> EnumerateAncestors()
        {
            yield return Name.Value;
            if (Parent.Value != null)
                foreach (var current in Parent.Value.EnumerateAncestors())
                    yield return current;
        }
        private static int GetInsertPosition(IEnumerable<FilePathViewModel> source, FilePathViewModel vm)
        {
            var result = 0;
            foreach (var current in source)
            {
                if (current.IsFolder == vm.IsFolder && current.Name.Value.CompareTo(vm.Name.Value) >= 0) return result;
                if (vm.IsFolder && !current.IsFolder) return result;
                result++;
            }
            return result;
        }
        public void AddChild(FilePathViewModel vm)
        {
            if (vm.Parent.Value != null) throw new ArgumentException("Parent exists");
            vm._Parent.Value = this;
            Children.InsertOnScheduler(GetInsertPosition(Children, vm), vm);
        }
        public bool Contains(FilePathViewModel vm) => vm?.Parent.Value == this;
        public FilePathViewModel FindChild(string childName)
        {
            for (int i = 0; i < Children.Count; i++)
                if (Children[i].Name.Value == childName)
                    return Children[i];
            return null;
        }
        public void RemoveChild(FilePathViewModel vm)
        {
            if (vm.Parent.Value != this) throw new ArgumentException("Parent is not this instance");
            vm._Parent.Value = null;
            Children.RemoveOnScheduler(vm);
        }
        public void Reset(string path)
        {
            Children.ClearOnScheduler();
            var folders = new SortedSet<FilePathViewModel>(comparer);
            foreach (var directory in Directory.GetDirectories(path))
            {
                var vm = new FilePathViewModel(main, directory.Split('\\')[^1])
                {
                    IsFolder = true,
                };
                vm._Parent.Value = this;
                vm.Reset(directory);
                folders.Add(vm);
            }
            Children.AddRangeOnScheduler(folders);
            var files = new SortedSet<FilePathViewModel>(comparer);
            foreach (var file in Directory.GetFiles(path))
            {
                var vm = new FilePathViewModel(main, file.Split('\\')[^1]);
                vm._Parent.Value = this;
                files.Add(vm);
            }
            Children.AddRangeOnScheduler(files);
        }
        public override string ToString() => Name.Value;
        #region Commands
        protected override void InitializeCommands()
        {
            base.InitializeCommands();
            DeleteFile.Subscribe(CommandDeleteFile);
            OpenFile.Subscribe(CommandOpenFile);
        }
        public ReactiveCommand DeleteFile { get; } = new ReactiveCommand();
        private void CommandDeleteFile() => File.Delete(FullPath);
        public ReactiveCommand OpenFile { get; } = new ReactiveCommand();
        private void CommandOpenFile()
        {
            if (!File.Exists(FullPath)) return;
            main.OpenAsdXml(FullPath);
        }
        #endregion
    }
}
