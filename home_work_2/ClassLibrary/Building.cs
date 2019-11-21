using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    /// <summary>
    /// Класс разрушаемого здания
    /// </summary>
    public class Building : IChangedHealth
    {
        string Name { get; set; }
        public int Durability { get; private set; }
        public void ReduceHealth(int damage)
        {
            Durability -= damage; 
        }
        public void RecoverHealth(int healthup)
        {
            Durability += healthup; 
        }
    }
}
