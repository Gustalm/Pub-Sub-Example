using System;

namespace PubSubExample.Domain
{
    public class Subscription<Tmessage> : IDisposable
    {
        public Action<Tmessage> Action { get; private set; }
        private readonly EventAggregator EventAggregator;
        private bool isDisposed;
        public Subscription(Action<Tmessage> action, EventAggregator eventAggregator)
        {
            Action = action;
            EventAggregator = eventAggregator;
        }

        public void Dispose()
        {
            EventAggregator.UnSbscribe(this);
            isDisposed = true;
        }
    }
}
