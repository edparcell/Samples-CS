using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Framing.v0_8;
using RabbitMQ.Client.Events;

namespace RabbitMQSub
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

            Console.WriteLine("Creating queue consumer...");
            QueueingBasicConsumer consumer = new QueueingBasicConsumer(ch);
            ch.BasicConsume("queue", null, consumer);

            Console.WriteLine("Listening...");
            while (true)
            {
                BasicDeliverEventArgs e = (BasicDeliverEventArgs)consumer.Queue.Dequeue();
                IBasicProperties props = e.BasicProperties;
                byte[] body = e.Body;
                Console.WriteLine("Received message: " + Encoding.UTF8.GetString(body));
                ch.BasicAck(e.DeliveryTag, false);
            }

            ch.Close(Constants.ReplySuccess, "Closing the channel");
            conn.Close(Constants.ReplySuccess, "Closing the connection");
        }
    }
}
