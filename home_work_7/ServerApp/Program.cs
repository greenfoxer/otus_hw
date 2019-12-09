using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ServerApp
{
    class Program
    {
        static ChatServer server;
        static Thread listener;
        static void Main(string[] args)
        {
            try
            {
                server = new ChatServer();
                listener = new Thread(new ThreadStart(server.StartServer));
                listener.Start();
            }
            catch (Exception ex)
            {
                server.StopServer();
                Console.WriteLine(ex.Message);
            }
        }
    }
}
