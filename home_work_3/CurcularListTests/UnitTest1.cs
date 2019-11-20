using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CircularList;
using home_work_3;
using System.Linq;

namespace CurcularListTests
{
    [TestClass]
    public class CircularListUnitTest
    {
        [TestMethod]
        public void EmptyListCheck()
        {
            CircularList<string> list = new CircularList<string>();
            Assert.AreEqual(0,list.Count,"Не создался список с 0 элементов");
        }
        [TestMethod]
        public void FillingListCheck()
        {
            CircularList<string> list = new CircularList<string>();
            list.Add("something");
            int equal = list.Count;
            int expected = 1;
            int equal2 = list.GetSlice().Length;
            Assert.AreEqual(expected, equal, "После добавления элемента количество не изменилось на 1");
            Assert.AreEqual(expected, equal2, "Срез кольцевого списка возвращает не корректное количество элементов");
        }
        [TestMethod]
        public void FillingListCheck2()
        {
            CircularList<int> list = new CircularList<int>();
            list.Add(5);
            list.Add(2);
            list.Add(3);
            list.Add(5);
            list.Add(6);
            list.Add(2);
            list.Add(5);
            list.Add(7);
            int expected = 8;
            Assert.AreEqual(expected, list.Count, "Количество переданных в список элементов и количество элементов списка не совпадают");
        }
        [TestMethod]
        public void AddAfterTest()
        {
            CircularList<int> list = new CircularList<int>();
            list.Add(5);
            list.Add(2);
            list.Add(3);
            list.Add(5);
            list.Add(6);
            list.Add(2);
            list.Add(5);
            list.Add(7);
            int elementToAdd = 10;
            int expected = 9;
            list.AddAfterFirst(elementToAdd, 5);
            int equal = list.Count;
            Assert.AreEqual(expected, equal, "Количество элементов в списке не совпало с ожидаемым");
            int[] slice = list.GetSlice();
            bool contains = slice.Contains(elementToAdd);
            Assert.AreEqual(true, contains, "Список не содержит добавленного элемента");
        }
        [TestMethod]
        public void AddAfterEachTest()
        {
            CircularList<int> list = new CircularList<int>();
            list.Add(5);
            list.Add(2);
            list.Add(3);
            list.Add(5);
            list.Add(6);
            list.Add(2);
            list.Add(5);
            list.Add(7);
            int elementToAdd = 10;
            int expected = 11;
            list.AddAfterAll(elementToAdd, 5);
            int equal = list.Count;
            Assert.AreEqual(expected, equal, "Количество элементов в списке не совпало с ожидаемым");
        }
        [TestMethod]
        public void CheckEnumerator()
        {
            CircularList<int> list = new CircularList<int>();
            list.Add(5);
            list.Add(2);
            list.Add(3);
            list.Add(5);
            list.Add(6);
            list.Add(2);
            list.Add(5);
            list.Add(7);
            int counter = 0;
            foreach (var item in list)
                counter++;
            int expected = 8;
            Assert.AreEqual(expected, counter, "Энумератор вывел меньшее количесво элементов");
        }
        [TestMethod]
        public void CheckSorting()
        {
            CircularList<int> list = new CircularList<int>();
            list.Add(5);
            list.Add(2);
            list.Add(3);
            list.Add(5);
            list.Add(6);
            list.Add(2);
            list.Add(5);
            list.Add(7);
            list.BubleSort();
            int[] expected = {2,2,3,5,5,5,6,7};
            int[] result = list.GetSlice();
            Assert.AreEqual(expected.Length, result.Length, "Не совпадает количество элементов в массивах");
            bool isDif = false;
            for (int i = 0; i < expected.Length; i++)
                if (result[i] != expected[i])
                    isDif = true;
            Assert.AreEqual(false, isDif, "Есть несовпадающие элементы");
        }
        [TestMethod]
        public void CheckCircular()
        {
            CircularList<int> list = new CircularList<int>();
            Node<int> head = list.Add(1);
            list.Add(2);
            list.Add(3);
            list.Add(4);
            list.Add(5);
            list.Add(6);
            list.Add(7);
            list.Add(8);
            int rounds = 6;
            int cnt = list.Count;
            int current = 0;
            Node<int> currentNode = head;
            while(current < ( rounds * cnt))  // пройдем по всему списку rounds раз, чтобы проверить, придем ли обратно
            {
                currentNode = currentNode.Next;
                current++;
            }
            Assert.AreEqual(head, currentNode, "Входная и выходная ноды не совпадают");
        }
        [TestMethod]
        public void CheckGetSclice()
        {
            CircularList<int> list = new CircularList<int>();
            list.Add(1);
            list.Add(2);
            list.Add(3);
            list.Add(4);
            list.Add(5);
            list.Add(6);
            list.Add(7);
            list.Add(8);
            int[] t = list.GetSlice(5);
            Assert.AreEqual(5, t[0], "Не совпадает первый элемент ожидаемой последовательности");
            Assert.AreEqual(4, t[t.Length-1], "Не совпадает ппоследний элемент ожидаемой последовательности");
        }
    }
}
