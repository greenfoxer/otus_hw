using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Sockets;
using MessageLib;

namespace ServerApp
{
    public class ChatServer
    {
        static TcpListener tcpListener;
        List<ChatClient> clients = new List<ChatClient>();
        protected internal void AddConnections(ChatClient chatClient)
        {
            clients.Add(chatClient);
        }
        protected internal void DisconnectClient(string id)
        {
            ChatClient client = clients.FirstOrDefault(c => string.Compare(c.Id, id) == 0);
            if (client != null)
                clients.Remove(client);
        }
        protected internal void BroadcastMessage(Message message, string senderId="server")
        {
            // зафиксируем объект для отправки в виде массива байт
            byte[] data = Utils.Serialize(message).ToArray();// Encoding.Unicode.GetBytes(message.Serialize());
            Console.WriteLine(message.Serialize());//выведем сообщение как JSON
            Console.WriteLine(string.Join("",data));//выведем сообщение как массив байт
            foreach (var client in clients.Where(c => string.Compare(c.Id, senderId) != 0))
            {
                client.Stream.Write(data, 0, data.Length);
                //Utils.serialize(message).CopyTo(client.Stream); //неэффективно выполнять для каждого пользователя, поэтому используем буфер
            }
        }
        protected internal void StopServer()
        {
            tcpListener.Stop();
            foreach (var client in clients)
                client.Close();
            Environment.Exit(0);
        }
        protected internal Message ServiceMessage(string body)
        {
            Message message = new Message(body);
            message.SetMessageInfo(MessageType.ServerMessage);
            return message;
        }
        protected internal void StartServer()
        {
            try
            {
                tcpListener = new TcpListener(System.Net.IPAddress.Any, 8888);
                tcpListener.Start();
                Console.WriteLine("Server has started!");
                while (true)
                {
                    TcpClient tcpClient = tcpListener.AcceptTcpClient();
                    ChatClient chatClient= new ChatClient(tcpClient, this);
                    Thread clientThread = new Thread(new ThreadStart(chatClient.CommunicationProtocol));
                    clientThread.Start();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                StopServer();
            }
        }
    }
}
