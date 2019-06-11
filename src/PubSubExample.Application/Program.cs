using PubSubExample.Domain;
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
            var sub2 = new Subscriber<string>(eve, Test2);

            pub.PublishMessage("test");

            Console.ReadLine();
        }

        //tests method that append x into string
        public static void Test(string param)
        {
            Console.WriteLine("Subscriber1 " + param + "x");
        }

        public static void Test2(string param)
        {
            Console.WriteLine("Subscriber2 " + param + "x2");
        }
    }
}
