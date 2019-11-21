using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibrary;
using System.Collections.Generic;

namespace PlayerTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CheckPlayerCreation()
        {
            string expectedName = "Julie";
            Player unit = new Player(expectedName);
            int expectedHealth = unit.Health + unit.BaseRecover;
            unit.Recover(unit);
            Assert.AreEqual(expectedName, unit.Name, "Имя игрока установилось неверно");
            Assert.AreEqual(expectedHealth, unit.Health, "Здоровье восстановиось неправильно");
        }
        [TestMethod]
        public void CheckPlayerNaming()
        {
            List<string> blackList = new List<string>() { "gangsta", "asshole", "bitch", "shit" };
            foreach (string val in blackList)
                Assert.AreEqual(false, val.IsUnToxic(), "Имя не отфильтровалось: "+val);
        }
        [TestMethod]
        public void CheckPlayerDamaging()
        {
            Player hero1 = new Player("Vasya", baseAttack: 55);
            Player hero2 = new Player("Varya", baseAttack: 45); //создадим двух игроков
            int expectedHealth = hero1.Health - hero2.BaseAttack;
            hero2.Attack(hero1);//вызовем метод одного игрока
            Assert.AreEqual(expectedHealth, hero1.Health,"Неверное количество урона 1");
            expectedHealth = hero2.Health - hero1.BaseAttack;
            hero1.Attack(hero2);
            Assert.AreEqual(expectedHealth, hero2.Health, "Неверное количество урона 2");
        }
        [TestMethod]
        public void CheckPlayerAchievments()
        {
            Player hero1 = new Player("Vasya");
            string expectedAchieve = "First player";
            hero1[0] = new Achievment(expectedAchieve);
            Assert.AreEqual(expectedAchieve, hero1[0].Name, "Энумератор не работает корректно");
        }
        [TestMethod]
        public void CheckBuildingDamaging()
        {
            Player hero1 = new Player("Vasya", baseAttack: 55);
            Building building1 = new Building(); //создадим двух игроков
            int expectedHealth = building1.Durability - hero1.BaseAttack;
            hero1.Attack(building1);//вызовем метод одного игрока
            Assert.AreEqual(expectedHealth, building1.Durability, "Неверное количество урона");
        }
        [TestMethod]
        public void CheckBuildingRecovering()
        {
            Player hero1 = new Player("Vasya", baseAttack: 55);
            Building building1 = new Building(); //создадим двух игроков
            int expectedHealth = building1.Durability + hero1.BaseRecover;
            hero1.Recover(building1);//вызовем метод одного игрока
            Assert.AreEqual(expectedHealth, building1.Durability, "Неверное количество прочности");
        }
    }
}
