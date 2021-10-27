using Asd2Edittor.Messangers;
using fslib3.WPF.Behaviors;
using System;
using System.Windows;

namespace Asd2Edittor.Views.Behaviors
{
    public abstract class RxMessageBehavior<T> : BehaviorBase<T> where T : DependencyObject
    {
        private IDisposable disposable;
        protected override void OnAttached()
        {
            base.OnAttached();
            disposable = RxMessanger.Default.Subscribe(MessangerOnNext);
        }
        protected override void OnDetaching()
        {
            base.OnDetaching();
            disposable?.Dispose();
        }
        #region Messanger
        protected abstract void MessangerOnNext(MessageInfo message);
        #endregion
    }
}
