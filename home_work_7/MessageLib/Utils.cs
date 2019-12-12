using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace MessageLib
{
    public static class Utils
    {
        static BinaryFormatter formatter = new BinaryFormatter();
        /// <summary>
        /// BinaryFormatter сериализирует данные в поток. 
        /// Для экономии времени не будем производить над полученным потоком никаких дополнительных действий.
        /// А просто скопируем все данные из потока MemoryStream в NetworkStream методом CopyTo
        /// <returns></returns>
        public static MemoryStream Serialize(Message message) 
        {
            MemoryStream stream = new MemoryStream();
            formatter.Serialize(stream, message);
            stream.Position = 0;
            return stream;
        }
        /// <summary>
        /// Так как NetworkStream не закрывается, то нет возможности просто скопировать входящие данные из него в MemoryStream.
        /// Поэтому считываем данные сначала в массив байт.
        /// Для тестирования обработки ошибок ввведем условие на проброс ошибки, если сообщение начинается с '/'
        /// <returns></returns>
        public static Message DeSerialize(byte[] stream)
        {
            Message obj;
            try
            {
                MemoryStream receivedStream = new MemoryStream(stream);
                receivedStream.Position = 0;
                obj = (Message) formatter.Deserialize(receivedStream);
                if (obj.body.StartsWith("/"))
                    throw new Exception("Serialization exception!");
            }
            catch (Exception ex)
            {
                obj = new Message(ex.Message);
                obj.SetMessageInfo(MessageType.ErrorMessage);
            }
            return obj;
        }
    }
}
