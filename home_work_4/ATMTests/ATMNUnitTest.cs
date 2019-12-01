using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using ATMBusinessLayer;

namespace ATMTests
{
    [TestFixture]
    class ATMNUnitTest
    {
        [Test]
        public void GetInfoTest() // Проверим, что из таблиц подтянулись корректные значения
        {
            ATMBusinessLayer.DataAccessLayer dal = new ATMBusinessLayer.DataAccessLayer();
            ATMBusinessLayer.User test = dal.GetInfoAbout("test", "1234");
            Assert.AreEqual(typeof(User), test.GetType(), "Типы не совпадают!");
            Assert.AreEqual("Иван", test.Name, "Данные не совпадают!");
            Assert.AreEqual(1, test.Id, "Данные не совпадают!");
            ATMBusinessLayer.User test2 = dal.GetInfoAbout("3421", "1463"); // такого пользователя заранее нет
            Assert.AreEqual(null, test2, "Такого объекта быть не должно");
        }
        [Test]
        public void IsModelFullTest() // Проверим, что таблицы подтянулись полностью
        {
            ATMBusinessLayer.DataAccessLayer dal = new ATMBusinessLayer.DataAccessLayer();
            bool expected = true;
            Assert.AreEqual(expected, dal.Users.Count == 5 && dal.Histories.Count == 29 && dal.Accounts.Count == 12, "База загружена неверно!");
        }
        [Test]
        public void CountAccountsTest() // проверим, что пункт 2 возвращает правильное количество счетов
        {
            ATMBusinessLayer.DataAccessLayer dal = new ATMBusinessLayer.DataAccessLayer();
            ATMBusinessLayer.User test = dal.GetInfoAbout("petro", "qwerty");
            int expected = 5;
            Assert.AreEqual(expected, dal.GetUserAccount(test).Count, "Неверное количество счетов для пользователя");
        }
        [Test]
        public void CountDetalisationItemsTest() // проверим, что детализация для пункта 3 возвращает правильное количество строк
        {
            ATMBusinessLayer.DataAccessLayer dal = new ATMBusinessLayer.DataAccessLayer();
            ATMBusinessLayer.User test = dal.GetInfoAbout("test", "1234");
            List<object> a = dal.GetDetalisationByUser(test);
            int expected = 6;
            Assert.AreEqual(expected, a.Count, "Количество не совпадает!");
        }
        string GetAnonimousObjectPropertyValue(object item, string property)
        {
            return item.GetType().GetProperty(property).GetValue(item, null).ToString();
        }
        object GetAnonimousObjectPropertyObject(object item, string property)
        {
            return item.GetType().GetProperty(property).GetValue(item, null);
        }
        [Test]
        public void RefillsTest() // 
        {
            ATMBusinessLayer.DataAccessLayer dal = new ATMBusinessLayer.DataAccessLayer();
            int expected = 11;
            List<object> refills = dal.GetRefillsWithUsers();
            Assert.AreEqual(expected, refills.Count, "Количество не совпадает с ожидаемым");
            foreach (var item in refills)
            {
                object user = GetAnonimousObjectPropertyObject(item, "user");
                Assert.AreEqual(true, user != null, "В пополнении не указан профиль пользователя");
                Assert.AreEqual(true, user is User, "В пополнении объект не профиль пользователя");
                long result;
                Assert.AreEqual(true, Int64.TryParse(GetAnonimousObjectPropertyValue(item, "account"), out result), "Неверный идентификатор счета");
            }
        }
        [Test]
        public void MoreThanNAccountsTest() // 
        {
            int N = 11000;
            ATMBusinessLayer.DataAccessLayer dal = new ATMBusinessLayer.DataAccessLayer();
            List<object> moreThanN = dal.GetUsersAndAccountsWithTotal(N);
            int expected = 7;
            Assert.AreEqual(expected, moreThanN.Count, "Количество не совпадает с ожидаемым");
            foreach (var item in moreThanN)
            {
                long result;
                Assert.AreEqual(true, Int64.TryParse(GetAnonimousObjectPropertyValue(item, "account"), out result), "Неверный идентификатор счета");
            }
        }
        [Test]
        public void MoreThanNTotalTest() // 
        {
            int N = 100000;
            ATMBusinessLayer.DataAccessLayer dal = new ATMBusinessLayer.DataAccessLayer();
            List<object> moreThanN = dal.GetUserHavingTotalMoreThanN(N);
            int expected = 3;
            Assert.AreEqual(expected, moreThanN.Count, "Количество не совпадает с ожидаемым");
        }
    }
}
