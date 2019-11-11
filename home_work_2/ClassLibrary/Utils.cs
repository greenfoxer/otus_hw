using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    /// <summary>
    /// Класс, содержащий метод расширения для класса string
    /// Смысл - проверить, не является ли имя игрока тосичным, путем сравнения с черным списком.
    /// </summary>
    public static class Utils
    {
        static List<string> blackList = new List<string>() { "gangsta", "asshole", "bitch", "shit" };
        public static bool IsUnToxic(this string str)
        {
            if (str == null)
                return false;
            return !blackList.Contains(str.ToLower());
        }
    }
}
