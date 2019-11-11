using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    /// <summary>
    /// Интерфейс описывающий способы разрушения и восстановления объектов: игроков и зданий.
    /// </summary>
    public interface IChangedHealth
    {
        void ReduceHealth(int damage);
        void RecoverHealth(int healthup);
    }
}
