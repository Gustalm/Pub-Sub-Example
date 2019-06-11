using NUnit.Framework;
using PubSubExample.Domain;

namespace Tests
{
    public class EventAggregatorTest
    {
        [Test]
        public void Should_SubscribeDelegate_CaseIsNotSubscribed_ForType()
        {
            var stub = new EventAggregator();

            stub.Subscribe<string>(x => x.ToLower());

            Assert.AreEqual(stub.Subscribers.Count, 1);
        }

        [Test]
        public void Should_NotDuplicateSubscription_Case_TypeIsALreadySubscribed()
        {
            var stub = new EventAggregator();

            stub.Subscribe<string>(x => x.ToLower());
            stub.Subscribe<string>(x => x.ToUpper());

            Assert.Multiple(() =>
            {
                Assert.AreEqual(stub.Subscribers.Count, 1);
                Assert.AreEqual(stub.Subscribers[typeof(string)].Count, 2);
            });
        }

        [Test]
        public void Should_Unsubscribe()
        {
            var stub = new EventAggregator();

            var firstSubscription = stub.Subscribe<string>(x => x.ToLower());
            var secondSubscription = stub.Subscribe<string>(x => x.ToUpper());

            stub.Unsubscribe(secondSubscription);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(stub.Subscribers.Count, 1);
                Assert.AreEqual(stub.Subscribers[typeof(string)].Count, 1);
            });
        }

        [Test]
        public void Should_PublishMessage_ToSubscribers()
        {
            bool isPublished = false;
            var stub = new EventAggregator();

            var firstSubscription = stub.Subscribe<bool>(x => isPublished = true);

            stub.Publish<bool>(true);

            Assert.IsTrue(isPublished);
        }
    }
}