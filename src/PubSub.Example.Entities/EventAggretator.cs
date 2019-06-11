using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace PubSubExample.Domain
{
    public class EventAggregator
    {
        public Dictionary<Type, IList> Subscribers { get; private set; }

        public EventAggregator()
        {
            Subscribers = new Dictionary<Type, IList>();
        }

        public void Publish<TMessageType>(TMessageType message)
        {
            Type t = typeof(TMessageType);
            IList actionlst;
            if (Subscribers.ContainsKey(t))
            {
                actionlst = new List<Subscription<TMessageType>>(Subscribers[t].Cast<Subscription<TMessageType>>());

                foreach (Subscription<TMessageType> a in actionlst)
                {
                    a.Action(message);
                }
            }
        }

        public Subscription<TMessageType> Subscribe<TMessageType>(Action<TMessageType> action)
        {
            Type t = typeof(TMessageType);
            var actiondetail = new Subscription<TMessageType>(action, this);

            if (!Subscribers.TryGetValue(t,out var actionlst))
            {
                actionlst = new List<Subscription<TMessageType>>();
                actionlst.Add(actiondetail);
                Subscribers.Add(t, actionlst);
            }
            else
            {
                actionlst.Add(actiondetail);
            }

            return actiondetail;
        }

        public void Unsubscribe<TMessageType>(Subscription<TMessageType> subscription)
        {
            Type t = typeof(TMessageType);
            if (Subscribers.ContainsKey(t))
            {
                Subscribers[t].Remove(subscription);
            }
        }

    }
}
