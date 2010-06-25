using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Pipes;

namespace NamedPipeClient
{
    class Program
    {
        public const string PIPE_NAME = @"\\.\pipe\examplePipe";

        static void Main(string[] args)
        {
            Console.WriteLine("Opening pipe {0}", PIPE_NAME);
            NamedPipeClientStream pipeClient = new NamedPipeClientStream(
                ".",
                PIPE_NAME,
                PipeDirection.Out,
                PipeOptions.Asynchronous
                );
            pipeClient.Connect(10000);

            Console.WriteLine("Writing message.");

            string messageText = "Hello World";
            byte[] message = Encoding.UTF8.GetBytes(messageText);
            pipeClient.Write(message, 0, message.Length);

            string messageText2 = "This is a second message";
            byte[] message2 = Encoding.UTF8.GetBytes(messageText2);
            pipeClient.Write(message2, 0, message2.Length);

            Console.WriteLine("Done.");
        }
    }
}
