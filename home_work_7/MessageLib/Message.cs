using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MessageLib
{
    [Serializable]
    public class Message: ISerializable
    {
        public string sender;
        public string body;
        public MessageType type;
        
        // Не используется
        public static Message DeSerialize(string raw)
        {
            return JsonConvert.DeserializeObject<Message>(raw);
        }
        //Json сериализация. Для доступного вывода сообщений в консоль.
        public string Serialize()
        {
            return JsonConvert.SerializeObject(this);
        }
        // Редактирование информации о сообщении.
        public void SetMessageInfo(MessageType type=MessageType.ServerMessage, string sender = "server")
        {
            this.type = type;
            this.sender = sender;
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("sender", this.sender);
            info.AddValue("body", this.body);
            info.AddValue("type", this.type);
        }
        //Конструкторы
        public Message() { }
        public Message(string body,string sender="")
        {
            this.body = body;
            this.sender = sender;
            this.type = MessageType.UserMessage;
        }
        public Message(SerializationInfo info, StreamingContext context)
        {
            body = (string)info.GetValue("body", typeof(string));
            sender = (string)info.GetValue("sender", typeof(string));
            type = (MessageType)info.GetValue("type", typeof(MessageType));
        }
    }
}
