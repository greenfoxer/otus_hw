using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace home_work_6
{
    abstract class Serializer
    {
        public TimeSpan DurationDeSerialization;
        public TimeSpan DurationSerialization;
        public abstract string Serialize(F instance);
        public abstract F DeSerialize(string data);
    }
}
