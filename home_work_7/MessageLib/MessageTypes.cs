using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageLib
{
    //Типы сообщений. Сообщения от сервера - широковещательное сообщение. Ошибка - генерируется при ошибке. Пользовательское - тип сообщения по умолчанию
    public enum MessageType
    {
        ServerMessage,
        ErrorMessage,
        UserMessage
    }
}
