using MessageLib;
using System;
using System.Net.Sockets;

namespace home_work_7
{
    public class Communicator
    {
        static string userName;
        private const string host = "127.0.0.1";
        private const int port = 8888;
        static TcpClient client;
        static NetworkStream stream;

        protected internal static void Disconnect()
        {
            if (stream != null)
                stream.Close();
            if (client != null)
                client.Close();
            Environment.Exit(0);
        }
        protected internal void Login()
        {
            client = new TcpClient();
            client.Connect(host, port);
            stream = client.GetStream();
            Message message;
            do
            {
                Console.WriteLine("Enter your name: ");
                userName = Console.ReadLine();
                message = new Message(userName);
                Utils.Serialize(message).CopyTo(stream);

                message = GetMessage();
                Console.WriteLine(message.body);
            }
            while (message.type == MessageType.ErrorMessage);
        }

        Message GetMessage()
        {
            byte[] data = new byte[512];

            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                int bytes = 0;
                do
                {
                    bytes = stream.Read(data, 0, data.Length);
                    ms.Write(data, 0, bytes);
                }
                while (stream.DataAvailable);
                data = ms.ToArray();
            }

            Message message = Utils.DeSerialize(data);
            return message;

        }
        protected internal void ReceiveMessage()
        {
            while (true)
            {
                try
                {
                    Message message = GetMessage();
                    if (message.type == MessageType.ErrorMessage)
                        throw new Exception(message.body);
                    Console.WriteLine(message.sender + "> " + message.body);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Connection lost!");
                    Console.ReadKey();
                    Disconnect();
                }
            }
        }

        protected internal void SendMessage()
        {
            Console.WriteLine("Type the message: ");
            while (true)
            {
                Message message = new Message(Console.ReadLine());
                if(message.body!=null)
                    Utils.Serialize(message).CopyTo(stream);
            }
        }
    }
}
