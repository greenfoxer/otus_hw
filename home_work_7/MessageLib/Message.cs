using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MessageLib
{
    public class Message
    {
        [JsonRequired]
        public string sender;
        [JsonRequired]
        public string body;
        [JsonRequired]
        public MessageType type;
        public static Message DeSerialize(string raw)
        {
            return JsonConvert.DeserializeObject<Message>(raw);
        }
        public string Serialize()
        {
            return JsonConvert.SerializeObject(this);
        }
        public void SetMessageInfo(MessageType type=MessageType.ServerMessage, string sender = "server")
        {
            this.sender = sender;
        }

        public Message() { }
        public Message(string body,string sender="")
        {
            this.body = body;
            this.sender = sender;
            this.type = MessageType.UserMessage;
        }
    }
}
