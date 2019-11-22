using System;
using NUnit.Framework;
using CircularList;
using System.Linq;

namespace NCurcularListTests
{
    [TestFixture]
    public class CircularListNUnitTest
    {
        [Test]
        public void NEmptyListCheck()
        {
            CircularList<string> list = new CircularList<string>();
            Assert.AreEqual(0,list.Count,"Не создался список с 0 элементов");
        }
        [Test]
        public void NFillingListCheck()
        {
            CircularList<string> list = new CircularList<string>();
            list.Add("something");
            int equal = list.Count;
            int expected = 1;
            int equal2 = list.GetSlice().Length;
            Assert.AreEqual(expected, equal, "После добавления элемента количество не изменилось на 1");
            Assert.AreEqual(expected, equal2, "Срез кольцевого списка возвращает не корректное количество элементов");
        }
        [Test]
        public void NFillingListCheck2()
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
        [Test]
        public void NAddAfterTest()
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
        [Test]
        public void NAddAfterEachTest()
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
        [Test]
        public void NCheckEnumerator()
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
        [Test]
        public void NCheckBubbleSorting()
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
            int checkPoint = 8 * 8 + 6; //  (n*k) + (l-1)
            int expectedVal = 6;
            Assert.AreEqual(expectedVal, list[checkPoint], "Список не работает, как кольцевой");
        }
        [Test]
        public void NCheckCircular()
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
        [Test]
        public void NCheckGetSlice()
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
            Assert.AreEqual(4, t[t.Length-1], "Не совпадает последний элемент ожидаемой последовательности");
        }
        [Test]
        public void NCheckQuickSorting()
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
            list.QuickSort();
            int[] expected = { 2, 2, 3, 5, 5, 5, 6, 7 };
            int[] result = list.GetSlice();
            Assert.AreEqual(expected.Length, result.Length, "Не совпадает количество элементов в массивах");
            bool isDif = false;
            for (int i = 0; i < expected.Length; i++)
                if (result[i] != expected[i])
                    isDif = true;
            Assert.AreEqual(false, isDif, "Есть несовпадающие элементы");
            int checkPoint = 8 * 6 + 6; //  (n*k) + (l-1)
            int expectedVal = 6;
            Assert.AreEqual(expectedVal, list[checkPoint], "Список не работает, как кольцевой");

        }
        [Test]
        public void NCheckIndexator()
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
            int checkPoint = 8 * 6 + 1;
            int expectedVal = 2;
            Assert.AreEqual(expectedVal, list[checkPoint], "Не совпадает последний элемент ожидаемой последовательности");
        }
    }
}
