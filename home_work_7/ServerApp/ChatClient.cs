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
            // Получение сообщений в виде JSON 
            //byte[] data = new byte[512];
            //StringBuilder builder = new StringBuilder();
            //int bytes = 0;
            //do
            //{
            //    bytes = Stream.Read(data, 0, data.Length);
            //    builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
            //}
            //while (Stream.DataAvailable);

            //Message message = Message.DeSerialize(builder.ToString());

            // Получение сообщений из потока как массива байт
            byte[] data = new byte[512];

            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                int bytes = 0;
                do
                {
                    bytes = Stream.Read(data, 0, data.Length);
                    ms.Write(data, 0, bytes);
                }
                while (Stream.DataAvailable);
                data = ms.ToArray();
            }

            Message message = Utils.DeSerialize(data);
            if (message.type == MessageType.ErrorMessage)
            {
                SendBackMessage(message);
                throw new Exception(message.body);
            }
            message.SetMessageInfo(MessageType.UserMessage, this.UserName);
            return message;
        }
        // Отправка сообщения только самому себе
        protected internal void  SendBackMessage(Message message)
        {
            Utils.Serialize(message).CopyTo(Stream);
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
                //Todo проверку на уникальнотсь имени

                message = server.ServiceMessage(UserName + " joined chat!");
                server.BroadcastMessage(message);
                while (true)
                {
                    try
                    {
                        message = GetMessage();
                        server.BroadcastMessage(message, Id);
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
