namespace PubSubExample.Domain
{
    public class Publisher
    {
        EventAggregator EventAggregator;
        public Publisher(EventAggregator eventAggregator)
        {
            EventAggregator = eventAggregator;
        }

        public void PublishMessage<T>(T message) => EventAggregator.Publish<T>(message);
    }
}
