using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary;

namespace TestApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            string name = null;
            Player hero1;
            if (name.IsUnToxic()) //протестируем метод расширения для string
                hero1 = new Player(name);
            hero1 = new Player("Vasya"); 
            Player hero2 = new Player("Varya"); //создадим двух игроков
            hero2.Attack(hero1);//вызовем метод одного игрока
            Console.WriteLine(hero1.Health);//проверим результат
            hero1[0] = new Achievment("First blood");//выдадим достижение
            Console.WriteLine(hero1[0]);//с помощью индексатора проверим, что мы можем обратиться к достижению
            if (hero1) //проверим жив ли игрок, если жив, подлечимся
                hero1.Recover(hero1);
            Console.ReadKey();
        }
    }
}
