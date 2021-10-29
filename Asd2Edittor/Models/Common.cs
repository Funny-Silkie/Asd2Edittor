using fslib3;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;

namespace Asd2Edittor.Models
{
    public static class Common
    {
        public static IDisposable SubscribeEvent<TDelegate, TEventArgs>(Func<EventHandler<TEventArgs>, TDelegate> conversion, Action<TDelegate> add, Action<TDelegate> remove, Action<TEventArgs> onNext)
        {
            return Observable.FromEventPattern(conversion, add, remove)
                .Select(x => x.EventArgs)
                .Subscribe(onNext);
        }
    }
}
