using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace home_work_6
{
    class F
    {
        [JsonRequired]
        int i1, i2, i3, i4, i5;
        [JsonRequired]
        string s1;
        public F()
        {
            i1 = 1; i2 = 2; i3 = 3; i4 = 4; i5 = 5; s1 = "example";
        }
    }
    abstract class Serializer
    {
        public TimeSpan DurationDeSerialization;
        public TimeSpan DurationSerialization;
        public abstract string Serialize(F instance);
        public abstract F DeSerialize(string data);
    }
    class CustomSerializer: Serializer
    {

        public string CustomSerialize(F instance)
        {
            string result = string.Join(",", instance.GetType().GetFields(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.DeclaredOnly | System.Reflection.BindingFlags.Instance).Select(t => "\"" + t.Name + "\":\"" + t.GetValue(instance).ToString() + "\""));
            return "{" + result + "}";
        }
        public F CustomDeSerialize(string data)
        {
            List<string> pairs = data.Remove(data.Length - 1, 1).Remove(0, 1).Split(',').ToList();
            Dictionary<string, string> param = new Dictionary<string, string>();
            foreach (var pair in pairs)
            {
                string name = pair.Split(':')[0];
                string value = pair.Split(':')[1];
                param.Add(name.Remove(name.Length - 1, 1).Remove(0, 1), value.Remove(value.Length - 1, 1).Remove(0, 1));
            }
            F instance = new F();
            try
            {
                foreach (string prop in param.Keys)
                {
                    System.Reflection.FieldInfo field = instance.GetType().GetField(prop, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.DeclaredOnly | System.Reflection.BindingFlags.Instance);
                    field.SetValue(instance, Convert.ChangeType(param[prop], field.FieldType));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return instance;
        }

        public override string Serialize(F instance)
        {
            string serialise = default(string);
            for (int i = 0; i < 1000000; i++)
                serialise = CustomSerialize(instance);
            return string.IsNullOrEmpty(serialise) ? null : serialise;
        }

        public override F DeSerialize(string data)
        {
            return CustomDeSerialize(data);
        }
    }
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
    abstract class DecoratorForSerializer : Serializer
    {
        protected Serializer _serializer;
        public DecoratorForSerializer(Serializer ser)
        {
            this._serializer = ser;
        }
    }
    class TimerDecorator : DecoratorForSerializer
    {
        public TimerDecorator(Serializer ser)
            : base(ser)
        { }
        public override string Serialize(F instance)
        {
            System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();
            timer.Start();
            string result = _serializer.Serialize(instance);
            timer.Stop();
            this.DurationSerialization = timer.Elapsed;
            return result;
        }
        public override F DeSerialize(string data)
        {
            System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();
            timer.Start();
            F result = _serializer.DeSerialize(data);
            timer.Stop();
            this.DurationDeSerialization = timer.Elapsed;
            return result;
        }
    }
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


            //string customPath = "custom.json";
            //string newtonsoftPath = "newtonsoft.json";
            //string serialise = "";
            //System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();

            //Console.WriteLine("CUSTOM".PadRight(20,'*').PadLeft(30,'*'));
            //timer.Start();
            //for(int i = 0; i<1000000 ; i++)
            //    serialise = CustomSerialize(instance);
            //timer.Stop();
            //TimeSpan customT = timer.Elapsed;
            //timer.Reset();
            //WriteFileOnDisk(customPath,serialise);
            //timer.Start();
            //Console.WriteLine(serialise);
            //Console.WriteLine("Custom serialization: " + customT);
            //timer.Stop();
            //Console.WriteLine("Printing time: "+timer.Elapsed);
            //timer.Reset();
            //timer.Start();
            //F deserializeCustom = CustomDeSerialize(serialise);
            //timer.Stop();
            //Console.WriteLine("Deserialized by CustomDeserializer time: " + timer.Elapsed);
            //timer.Reset();

            //Console.WriteLine();
            //Console.WriteLine("NEWTONSOFT".PadRight(20, '*').PadLeft(30, '*'));
            //timer.Start();
            //for (int i = 0; i < 1000000; i++)
            //    serialise = JsonConvert.SerializeObject(instance);
            //timer.Stop();
            //TimeSpan newtonT = timer.Elapsed;
            //timer.Reset();
            //WriteFileOnDisk(newtonsoftPath, serialise);

            //timer.Start();
            //Console.WriteLine(serialise);
            //Console.WriteLine("Newtonsoft serialization: " + newtonT);
            //timer.Stop();
            //Console.WriteLine("Printing time: " + timer.Elapsed);
            //timer.Reset();
            //timer.Start();
            //F deserializeNewtonsoft = JsonConvert.DeserializeObject<F>(serialise);
            //timer.Stop();
            //Console.WriteLine("Deserialized by Newtonsoft time: " + timer.Elapsed);
            //timer.Reset();

            //Console.WriteLine((newtonT > customT ? "\nNewtonSoft takes more time. Difference: " + (newtonT - customT) : "\nCustom takes more time. Difference: " + (customT-newtonT))); // 4) Разница
            Console.ReadKey();
        }
    }
}

