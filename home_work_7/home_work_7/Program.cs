using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Sockets;
using MessageLib;

namespace home_work_7
{
    class Program
    {
        static string userName;
        private const string host = "127.0.0.1";
        private const int port = 8888;
        static TcpClient client;
        static NetworkStream stream;
        static void Main(string[] args)
        {
            Console.WriteLine("Enter your name: ");
            userName = Console.ReadLine();
            client = new TcpClient();
            try
            {
                client.Connect(host, port);
                stream = client.GetStream();
                Message message = new Message(userName);
                byte[] data = Encoding.Unicode.GetBytes(message.Serialize());
                stream.Write(data, 0, data.Length);
                Thread receiveThread = new Thread(new ThreadStart(ReceiveMessage));
                receiveThread.Start();
                SendMessage();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Disconnect();
            }
        }

        private static void Disconnect()
        {
            if (stream != null)
                stream.Close();
            if (client != null)
                client.Close();
            Environment.Exit(0);
        }

        static void ReceiveMessage()
        {
            while (true)
            {
                try
                {
                    byte[] data = new byte[512];
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0;
                    do
                    {
                        bytes = stream.Read(data, 0, data.Length);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (stream.DataAvailable);

                    Message message = Message.DeSerialize(builder.ToString());
                    Console.WriteLine(message.sender+"> "+message.body);
                }
                catch 
                {
                    Console.WriteLine("Connection lost!");
                    Console.ReadKey();
                    Disconnect();
                }
            }
        }

        static void SendMessage()
        {
            Console.WriteLine("Type the message: ");
            while (true)
            {
                Message message = new Message(Console.ReadLine());
                byte[] data = Encoding.Unicode.GetBytes(message.Serialize());
                stream.Write(data, 0, data.Length);
            }
        }
    }
}
