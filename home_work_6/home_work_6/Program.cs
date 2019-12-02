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
    class Program
    {
        static string CustomSerialize<T>(T instance)
        {
            string result = string.Join(",", instance.GetType().GetFields(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.DeclaredOnly | System.Reflection.BindingFlags.Instance).Select(t => "\"" + t.Name + "\":\"" + t.GetValue(instance).ToString() + "\""));
            return "{"+result+"}";
        }
        static F CustomDeSerialize(string data)
        {
            List<string> pairs = data.Remove(data.Length - 1,1).Remove(0,1).Split(',').ToList();
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

            string customPath = "custom.json";
            string newtonsoftPath = "newtonsoft.json";
            string serialise = "";
            System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();

            Console.WriteLine("CUSTOM".PadRight(20,'*').PadLeft(30,'*'));
            timer.Start();
            for(int i = 0; i<1000000 ; i++)
                serialise = CustomSerialize(instance);
            timer.Stop();
            TimeSpan customT = timer.Elapsed;
            timer.Reset();
            WriteFileOnDisk(customPath,serialise);
            timer.Start();
            Console.WriteLine(serialise);
            Console.WriteLine("Custom serialization: " + customT);
            timer.Stop();
            Console.WriteLine("Printing time: "+timer.Elapsed);
            timer.Reset();
            timer.Start();
            F deserializeCustom = CustomDeSerialize(serialise);
            timer.Stop();
            Console.WriteLine("Deserialized by CustomDeserializer time: " + timer.Elapsed);
            timer.Reset();

            Console.WriteLine();
            Console.WriteLine("NEWTONSOFT".PadRight(20, '*').PadLeft(30, '*'));
            timer.Start();
            for (int i = 0; i < 1000000; i++)
                serialise = JsonConvert.SerializeObject(instance);
            timer.Stop();
            TimeSpan newtonT = timer.Elapsed;
            timer.Reset();
            WriteFileOnDisk(newtonsoftPath, serialise);

            timer.Start();
            Console.WriteLine(serialise);
            Console.WriteLine("Newtonsoft serialization: " + newtonT);
            timer.Stop();
            Console.WriteLine("Printing time: " + timer.Elapsed);
            timer.Reset();
            timer.Start();
            F deserializeNewtonsoft = JsonConvert.DeserializeObject<F>(serialise);
            timer.Stop();
            Console.WriteLine("Deserialized by Newtonsoft time: " + timer.Elapsed);
            timer.Reset();

            Console.WriteLine((newtonT > customT ? "\nNewtonSoft takes more time. Difference: " + (newtonT - customT) : "\nCustom takes more time. Difference: " + (customT-newtonT))); // 4) Разница
            Console.ReadKey();
        }
    }
}
