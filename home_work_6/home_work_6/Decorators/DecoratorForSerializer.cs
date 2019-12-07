using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace home_work_6
{
    abstract class DecoratorForSerializer : Serializer
    {
        protected Serializer _serializer;
        public DecoratorForSerializer(Serializer ser)
        {
            this._serializer = ser;
        }
    }
}
