using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace home_work_6
{
    class Program
    {
        static void WriteFileOnDisk(string path, string data)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(path, false, System.Text.Encoding.Default))
                {
                    writer.Write(data);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        static void Main(string[] args)
        {
            F instance = new F();

            Serializer cs = new CustomSerializer();

            string serialise = default(string);
            Console.WriteLine("CUSTOM".PadRight(20, '*').PadLeft(30, '*'));
            cs = new TimerDecorator(cs);
            serialise = cs.Serialize(instance);
            Console.WriteLine("Custom serialization: " + cs.DurationSerialization);

            F deserializeCustom = cs.DeSerialize(serialise);
            Console.WriteLine("Deserialized by CustomDeserializer time: " + cs.DurationDeSerialization);
            
            Console.WriteLine();
            
            Console.WriteLine("NEWTONSOFT".PadRight(20, '*').PadLeft(30, '*'));
            Serializer ns = new NewtonsoftSerializer();
            ns = new TimerDecorator(ns);
            serialise = ns.Serialize(instance);
            Console.WriteLine("Custom serialization: " + ns.DurationSerialization);

            F deserializeNewtonsoft = ns.DeSerialize(serialise);
            Console.WriteLine("Deserialized by Newtonsoft time: " + ns.DurationDeSerialization);

            Console.WriteLine();
            Console.WriteLine((ns.DurationSerialization > cs.DurationSerialization ? "\nNewtonSoft serialisation takes more time. Difference: " + (ns.DurationSerialization - cs.DurationSerialization) : "\nCustom serialisation takes more time. Difference: " + (cs.DurationSerialization - ns.DurationSerialization))); // 4) Разница
            Console.WriteLine((ns.DurationDeSerialization > cs.DurationDeSerialization ? "\nNewtonSoft deserialisation takes more time. Difference: " + (ns.DurationDeSerialization - cs.DurationDeSerialization) : "\nCustom deserialisation takes more time. Difference: " + (cs.DurationDeSerialization - ns.DurationDeSerialization))); // 4) Разница

            Console.ReadKey();
        }
    }
}

