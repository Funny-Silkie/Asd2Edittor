using System;
using System.Collections.Generic;
using System.Reactive.Disposables;

namespace Asd2Edittor.Messangers
{
    public class RxMessanger : MessangerBase, IObservable<MessageInfo>
    {
        private readonly List<IObserver<MessageInfo>> messangers = new List<IObserver<MessageInfo>>();
        public static RxMessanger Default { get; } = new RxMessanger();
        public RxMessanger()
        {

        }
        public override void Send(MessageInfo massage)
        {
            foreach (var current in messangers) current.OnNext(massage);
        }
        public IDisposable Subscribe(IObserver<MessageInfo> observer)
        {
            if (observer == null) throw new ArgumentNullException(nameof(observer), "引数がnullです");
            messangers.Add(observer);
            return Disposable.Create(() => messangers.Remove(observer));
        }
    }
}
