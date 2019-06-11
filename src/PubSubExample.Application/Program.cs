using PubSubExample.Domain;
using PubSubExample.Domain.Interfaces;
using System;

namespace PubSubExample.Application
{
    class Program
    { 

        static void Main(string[] args)
        {
            var eve = new EventAggregator();
            var pub = new Publisher(eve);
            var sub = new Subscriber<string>(eve, Test);
            var sub2 = new Subscriber<string>(eve, Test);

            pub.PublishMessage("test");

            Console.ReadLine();
        }

        //method that append x into string
        public static void Test(string param)
        {
            Console.WriteLine(param + "x");
        }
    }
}
