using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace PubSubExample.Domain
{
    public class EventAggregator
    {
        private Dictionary<Type, IList> subscribers;

        public EventAggregator()
        {
            subscribers = new Dictionary<Type, IList>();
        }

        public void Publish<TMessageType>(TMessageType message)
        {
            Type t = typeof(TMessageType);
            IList actionlst;
            if (subscribers.ContainsKey(t))
            {
                actionlst = new List<Subscription<TMessageType>>(subscribers[t].Cast<Subscription<TMessageType>>());

                foreach (Subscription<TMessageType> a in actionlst)
                {
                    a.Action(message);
                }
            }
        }

        public Subscription<TMessageType> Subscribe<TMessageType>(Action<TMessageType> action)
        {
            Type t = typeof(TMessageType);
            IList actionlst;
            var actiondetail = new Subscription<TMessageType>(action, this);

            if (!subscribers.TryGetValue(t, out actionlst))
            {
                actionlst = new List<Subscription<TMessageType>>();
                actionlst.Add(actiondetail);
                subscribers.Add(t, actionlst);
            }
            else
            {
                actionlst.Add(actiondetail);
            }

            return actiondetail;
        }

        public void UnSbscribe<TMessageType>(Subscription<TMessageType> subscription)
        {
            Type t = typeof(TMessageType);
            if (subscribers.ContainsKey(t))
            {
                subscribers[t].Remove(subscription);
            }
        }

    }
}
