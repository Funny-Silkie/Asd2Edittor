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
        public ReactiveCollection<FilePathViewModel> Children { get; } = new ReactiveCollection<FilePathViewModel>();
        public ReactiveProperty<string> Name { get; } = new ReactiveProperty<string>();
        public ReadOnlyReactiveProperty<FilePathViewModel> Parent { get; }
        private readonly ReactiveProperty<FilePathViewModel> _Parent = new ReactiveProperty<FilePathViewModel>();
        public FilePathViewModel(string name)
        {
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
        public void AddChild(FilePathViewModel vm)
        {
            if (vm.Parent.Value != null) throw new ArgumentException("Parent exists");
            vm._Parent.Value = this;
            Children.AddOnScheduler(vm);
        }
        public bool Contains(FilePathViewModel vm) => vm?.Parent.Value == this;
        public FilePathViewModel FindChild(string childName)
        {
            for (int i = 0; i < Children.Count; i++)
                if (Children[i].Name.Value == childName)
                    return Children[i];
            return null;
        }
        public string FullPath => $"{AppDomain.CurrentDomain.BaseDirectory}{string.Join('\\', EnumerateAncestors().SkipLast(1).Reverse())}";
        public void RemoveChild(FilePathViewModel vm)
        {
            if (vm.Parent.Value != this) throw new ArgumentException("Parent is not this instance");
            vm._Parent.Value = null;
            Children.RemoveOnScheduler(vm);
        }
        public void Reset(string path)
        {
            Children.ClearOnScheduler();
            if (Children.Count == 0)
                foreach (var directory in Directory.GetDirectories(path))
                {
                    var vm = new FilePathViewModel(directory.Split('\\')[^1]);
                    AddChild(vm);
                    vm.Reset(directory);
                }
            foreach (var file in Directory.GetFiles(path))
            {
                var vm = new FilePathViewModel(file.Split('\\')[^1]);
                AddChild(vm);
            }
        }
        protected override void InitializeCommands()
        {
            base.InitializeCommands();
        }
        public override string ToString() => Name.Value;
    }
}
