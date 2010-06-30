using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using RabbitMQ.Client;
using RabbitMQ.Client.Framing.v0_8;

namespace RabbitMQPub
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Creating factory...");
            ConnectionFactory factory = new ConnectionFactory();

            Console.WriteLine("Creating connection...");
            factory.Protocol = Protocols.FromEnvironment();
            factory.HostName = "localhost";         
            IConnection conn = factory.CreateConnection();

            Console.WriteLine("Creating channel...");
            IModel ch = conn.CreateModel();

            Console.WriteLine("Creating exchange...");
            ch.ExchangeDeclare("exch", ExchangeType.Direct);

            Console.WriteLine("Creating queue...");
            ch.QueueDeclare("queue");
            ch.QueueBind("queue", "exch", "key", false, null);

            while (true)
            {
                Console.WriteLine("Sending message.");
                byte[] messageBody = Encoding.UTF8.GetBytes("Hello world");
                ch.BasicPublish("exch", "key", null, messageBody);
                Thread.Sleep(1000);
            }

            ch.Close(Constants.ReplySuccess, "Closing the channel");
            conn.Close(Constants.ReplySuccess, "Closing the connection");

        }
    }
}
