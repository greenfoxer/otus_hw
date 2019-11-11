using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    /// <summary>
    /// Класс представляет из себя некоторое достижение, которое можел получить игрок.
    /// Имеет поле с названием, и переопределенный метод ToString, возвращающий название достижения.
    /// </summary>
    public class Achievment
    {
        public string Name { get; set; }
        public Achievment(string name)
        {
            this.Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
