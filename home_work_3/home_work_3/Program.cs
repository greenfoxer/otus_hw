using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CircularList;

namespace home_work_3
{
    class Program
    {
        static void Main(string[] args)
        {
            CircularList.CircularList<string> test = new CircularList.CircularList<string>();
            test.Add("atata");
            Node<string> fromhere = test.Add("good boy");
            string s = "let's go walking";
            test.Add("let's go walking");
            string[] t = test.GetSlice(s);
            bool o;
            o=test.IsContain(s);
            o=test.IsContain("1234");
            t = test.GetSlice("1234");
            List<object> l = new List<object>();
            l.Sort();

            CircularList.CircularList<int> test2 = new CircularList.CircularList<int>();
            test2.Add(5);
            test2.Add(2);
            test2.Add(3);
            test2.Add(5);
            Node<int> testNode = test2.Add(6);
            test2.Add(2);
            test2.Add(5);
            test2.Add(7);
            foreach (int item in test2)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("$$$$$$$$$$$$$$$$$$");
            //test2.RemoveAll(5);
            test2.BubleSort();
            //test2.QuickSort();
            foreach (int item in test2)
            {
                Console.WriteLine(item);
            }
            //}
            //test2.RemoveAll(5);
            //foreach (int item in test2)
            //{
            //    Console.WriteLine(item);
            //}
            Console.ReadKey();
        }
    }
}
