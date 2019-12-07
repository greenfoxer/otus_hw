using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace home_work_6
{
    class CustomSerializer : Serializer
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
}
