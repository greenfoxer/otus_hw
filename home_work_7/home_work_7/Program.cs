using System;
using System.Threading;

namespace home_work_7
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Communicator communicator;
            try
            {
                //Взаимодействие производится через экземпляр класса Communicator
                communicator = new Communicator();
                // Выделил отдельный метод для установки имени пользователя - с предварительной верификацией домтупности имени. 
                // но пока не реализовано на стороне сервера.
                communicator.Login();
                // Старт потока для получения сообщений
                Thread receiveThread = new Thread(new ThreadStart(communicator.ReceiveMessage));
                receiveThread.Start();
                // Старт процесса ожиданияи отправки сообщений.
                communicator.SendMessage();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Communicator.Disconnect();
            }
        }
    }
}
