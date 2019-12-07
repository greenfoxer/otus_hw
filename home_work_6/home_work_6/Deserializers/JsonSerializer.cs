using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace home_work_6
{
    class NewtonsoftSerializer : Serializer
    {
        public override string Serialize(F instance)
        {
            string serialise = default(string);
            for (int i = 0; i < 1000000; i++)
                serialise = JsonConvert.SerializeObject(instance);
            return string.IsNullOrEmpty(serialise) ? null : serialise;
        }

        public override F DeSerialize(string data)
        {
            return JsonConvert.DeserializeObject<F>(data);
        }
    }
}
