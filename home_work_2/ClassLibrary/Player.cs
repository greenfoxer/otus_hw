using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    /// <summary>
    /// Класс, представляющий некоторого персонажа игры.
    /// Для полноты примера были добавлены классы разрушаемого здания и достижений.
    /// Персонаж может наносить повреждения или восстанавливать других персонажей или здания.
    /// Определен оператор true/false
    /// </summary>
    public class Player : IChangedHealth
    {
        //Блок пееменных, характеризующих персонажа.
        public string Name { get; set; }
        public int Health { get; set; }
        int Strength { get; set; }
        int BaseAttack { get; set; }
        int BaseRecover { get; set; }
        //На персонаже могут быть ачивки (до 5 штук). По ним будет итератор.
        Achievment[] Achievments = new Achievment[5];

        //  Конструктор для класса игрока. Содержит обящательный параметр name и ряд необязательных параметров.       
        public Player(string name, int health = 100, int strength = 10, int baseAttack = 50, int recover = 25)
        {
            this.Health = health;
            if (name == null)
                name = "NoName";
            this.Name = name;
            this.Strength = strength;
            this.BaseAttack = baseAttack;
            this.BaseRecover = recover;
        }
        //Блок перегруженных методов действий атаки/восстановления
        #region overload methods
        //по типу объекта
        public void Attack(Player target)
        {
            target.ReduceHealth(BaseAttack);
        }
        public void Attack(Building target)
        {
            target.ReduceHealth(BaseAttack);
        }
        //через интерфейс
        public void Recover(IChangedHealth target)
        {
            target.RecoverHealth(BaseRecover);
        }
        #endregion
        //Операторы true/false , показывающие "жив" ли персонаж (Health>0)
        #region true/false_overload
        public static bool operator true(Player p)
        {
            return p.Health > 0;
        }
        public static bool operator false(Player p)
        {
            return !(p.Health > 0);
        }
        #endregion
        //Блок методов реализующих нанесение повреждения или восстановление персонажа.
        #region IChangedHealth_Realisation
        public void ReduceHealth(int damage)
        {
            Health -= damage;
        }
        public void RecoverHealth(int healthup)
        {
            this.Health += healthup;
        }
        #endregion
        //Индексатор по классу игрока. Будет возвращать наложенные на него баффы.
        #region achievments indexator
        public Achievment this[int index]
        {
            get 
            {
                return Achievments[index%5];
            }
            set
            {
                Achievments[index] = value;
            }
        }
        #endregion
    }
}
