using System;

namespace PubSubExample.Domain
{
    public class Subscriber<T>
    {
        EventAggregator eventAggregator;
        private Subscription<T> subscription;

        public Subscriber(EventAggregator eve, Action<T> action)
        {
            eventAggregator = eve;
            if (action != null)
                subscription = eve.Subscribe<T>(action);
        }

        public void Unsubscribe() => eventAggregator.Unsubscribe(subscription);
    }
}
