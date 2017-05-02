using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace SiteSpeedAgent
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");        
            
            var cf = new ConnectionFactory();
            cf.UserName = "guest";
            cf.Password = "guest";
            cf.HostName = "localhost";

            var connection = cf.CreateConnection();
            var model = connection.CreateModel();
            model.ExchangeDeclare("TestExchange", ExchangeType.Direct);
            model.QueueDeclare("TestQueue", false, false, false, null);
            model.QueueBind("TestQueue", "TestExchange", "abc", null);

            var consumer = new EventingBasicConsumer(model);

            consumer.Received += (sender, eventArgs) =>
            {
                Console.WriteLine(Encoding.ASCII.GetString(eventArgs.Body));
                Console.WriteLine("=========");
            };

            model.BasicConsume("TestQueue", false, consumer);

            Console.ReadKey();

            model.Close();
            connection.Close();
        }
    }
}