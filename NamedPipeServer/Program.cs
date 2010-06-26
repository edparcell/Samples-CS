using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Pipes;
using System.IO;

namespace NamedPipeServer
{
    class Program
    {
        public const string PIPE_NAME = @"\\.\examplePipe";

        static void Main(string[] args)
        {
            Console.WriteLine("Creating pipe {0}", PIPE_NAME);
            NamedPipeServerStream pipeServer = new NamedPipeServerStream(
                PIPE_NAME,
                PipeDirection.InOut,
                10,
                PipeTransmissionMode.Message,
                PipeOptions.None
                );

            Console.WriteLine("Waiting for connection.");
            pipeServer.WaitForConnection();

            Console.WriteLine("Client connected.");

            int bytes_read;
            while(true)
            {
                Byte[] buffer = new Byte[4096];
                StringBuilder sb = new StringBuilder();
                do
                {
                    bytes_read = pipeServer.Read(buffer, 0, buffer.Length);
                    sb.Append(Encoding.UTF8.GetChars(buffer, 0, bytes_read));
                    if (pipeServer.IsMessageComplete)
                    {
                        break;
                    }
                }
                while (!pipeServer.IsMessageComplete);

                if (bytes_read == 0)
                {
                    Console.WriteLine("Received empty message, quitting.");
                    break;
                }
                
                Console.WriteLine("Message received: {0}", sb.ToString());
            }

            Console.WriteLine("Done");
        }
    }
}
