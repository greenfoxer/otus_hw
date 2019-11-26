using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATMBusinessLayer;

namespace home_work_4
{
    class Program
    {
        static string GetAnonimousObjectPropertyValue(object item, string property)
        {
            return item.GetType().GetProperty(property).GetValue(item, null).ToString();
        }
        static object GetAnonimousObjectPropertyObject(object item, string property)
        {
            return item.GetType().GetProperty(property).GetValue(item, null);
        }
        static void Main(string[] args)
        {
            //ATMDataModel.DataSetModel db = new ATMDataModel.DataSetModel();
            ATMBusinessLayer.DataAccessLayer dal = new ATMBusinessLayer.DataAccessLayer();
            ATMBusinessLayer.User test = dal.GetInfoAbout("test", "1234");
            ATMBusinessLayer.User test2 = dal.GetInfoAbout("3421", "1213");

            // Мы точно знаем, какие поля есть в анонимном объекте
            Console.WriteLine("Детализация для пользователя "+test.Name+ " " +test.MiddleName+" "+test.Surname);
            foreach (var item in dal.GetDetalisationByUser(test))
            {
                Console.WriteLine(GetAnonimousObjectPropertyValue(item, "account") 
                                    + '\t' + GetAnonimousObjectPropertyValue(item, "date")
                                    + '\t' + GetAnonimousObjectPropertyValue(item, "amount")
                                    + '\t' + GetAnonimousObjectPropertyValue(item, "type"));                
            }

            Console.WriteLine("Список пополнений: ");
            Console.WriteLine("Счет \t\t Дата \t\t Сумма \t Владелец");
            foreach (var item in dal.GetRefillsWithUsers())
            {
                User itemUser = GetAnonimousObjectPropertyObject(item, "user") as User; // получим доступ к полю пользователя
                Console.WriteLine(GetAnonimousObjectPropertyValue(item, "account")
                                    + "\t" + GetAnonimousObjectPropertyValue(item, "date")
                                    + "\t" + GetAnonimousObjectPropertyValue(item, "amount")
                                    + '\t' + itemUser.ToString());    
            }

            decimal N = 10000;
            Console.WriteLine("Счет \t Сумма \t Владелец");
            foreach (var item in dal.GetUsersAndAccountsWithTotal(N))
            {
                Console.WriteLine(GetAnonimousObjectPropertyValue(item, "account")
                                    + "\t" + GetAnonimousObjectPropertyValue(item, "total")
                                    + '\t' + GetAnonimousObjectPropertyValue(item,"user"));    
            }


            Console.ReadKey();
        }
    }
}
