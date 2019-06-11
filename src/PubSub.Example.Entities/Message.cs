using System;

namespace PubSubExample.Domain
{
    public class Message<T> : EventArgs
    {
        public T MessageArg { get; set; }
        public Message(T message) => MessageArg = message;
    }
}
