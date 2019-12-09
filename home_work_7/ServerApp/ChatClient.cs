using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Threading;
using MessageLib;

namespace ServerApp
{
    public class ChatClient
    {
        protected internal string Id { get; private set; }
        protected internal NetworkStream Stream { get; private set; }
        string UserName;
        TcpClient client;
        ChatServer server;

        public ChatClient(TcpClient tcpClient, ChatServer serverObj)
        {
            Id = Guid.NewGuid().ToString();
            this.client = tcpClient;
            this.server = serverObj;
            serverObj.AddConnections(this);
        }

        private Message GetMessage()
        {
            byte[] data = new byte[512];
            StringBuilder builder = new StringBuilder();
            int bytes = 0;
            do
            {
                bytes = Stream.Read(data, 0, data.Length);
                builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
            }
            while (Stream.DataAvailable);

            Message message = Message.DeSerialize(builder.ToString());
            message.SetMessageInfo(MessageType.UserMessage, this.UserName, this.Id);
            return message;
        }

        protected internal void Close()
        {
            if (Stream != null)
                Stream.Close();
            if (client != null)
                client.Close();
        }
        void LeftChat()
        {
            Message message = server.ServiceMessage(UserName + " has left chat!");
        }
        public void CommunicationProtocol()
        {
            try
            {
                Stream = client.GetStream();
                Message message = GetMessage();
                UserName = message.body;

                message = server.ServiceMessage(UserName + " joined chat!");
                server.BroadcastMessage(message);
                while (true)
                {
                    try
                    {
                        message = GetMessage();
                        server.BroadcastMessage(message);
                    }
                    catch
                    {
                        message = server.ServiceMessage(UserName + " has left chat!");
                        server.DisconnectClient(Id);
                        server.BroadcastMessage(message);
                        break;
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Protocol failure: "+ex.Message);
            }
            finally
            {
                server.DisconnectClient(this.Id);
                Close();
            }
        }
    }
}
