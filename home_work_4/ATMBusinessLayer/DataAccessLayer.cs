using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using AutoMapper;
using ATMDataModel;

namespace ATMBusinessLayer
{
    public class DataAccessLayer
    {
        public List<User> Users;
        public List<Account> Accounts;
        public List<History> Histories;
        public DataAccessLayer()
        {
            ATMDataModel.DataSetModel db = new DataSetModel();

            IMapper mapper = new MapperConfiguration(a => { a.AddProfile(new AutoMapperUserProfile()); }).CreateMapper();
            Users = mapper.Map<List<DataRow>, List<User>>(new List<DataRow>(db.ATMModel.Tables["tUser"].Rows.OfType<DataRow>()));

            mapper = new MapperConfiguration(a => { a.AddProfile(new AutoMapperAccountProfile()); }).CreateMapper();
            Accounts = mapper.Map<List<DataRow>, List<Account>>(new List<DataRow>(db.ATMModel.Tables["tAccount"].Rows.OfType<DataRow>()));

            mapper = new MapperConfiguration(a => { a.AddProfile(new AutoMapperHistoryProfile()); }).CreateMapper();
            Histories = mapper.Map<List<DataRow>, List<History>>(new List<DataRow>(db.ATMModel.Tables["tHistory"].Rows.OfType<DataRow>()));
        }
        
        // LINQ - home work
        // 1) получение информации об аккаунте по логину и паролю
        public User GetInfoAbout(string login, string pass)
        {
            return Users.Where(t => string.Compare(t.Login,login, StringComparison.InvariantCulture) == 0 && string.Compare(t.Password, pass, StringComparison.InvariantCulture) == 0).FirstOrDefault();
        }
        // 2) Вывод данных о все счетах пользователя
        public List<Account> GetUserAccount(User user)
        {
            return Accounts.Where(t => t.IdOwner == user.Id).ToList();
        }
        // 3) Вывод данных о все счетах пользователя с детализацией
        public List<object> GetDetalisationByUser(User user)
        {
            return new List<object>(Accounts.Where(t => t.IdOwner == user.Id).Join(Histories, a => a.Id, b => b.IdAccount, (a, b) => new { account = a.Id, date = b.DateOperation, amount = b.Amount, type = b.OpType }));
        }
        // 4) Пополнения всех счетов с информацией по пользователю
        public List<object> GetRefillsWithUsers()
        {
            return new List<object>(Histories.Where(h => h.OpType == OperationType.Refill).Join(Accounts, h => h.IdAccount, a => a.Id, (h, a) => new { date = h.DateOperation, amount = h.Amount, account = a.Id, idowner = a.IdOwner }).Join(Users, d => d.idowner, u => u.Id, (d, u) => new { d.date, d.amount, d.account, user = u }));
        }
        // 5) Данные о пользователях и счетах, где сумма больше N
        public List<object> GetUsersAndAccountsWithTotal(decimal N)
        {
            return new List<object>(Accounts.Where(a => a.Total >= N).Join(Users, a => a.IdOwner, u => u.Id, (a, u) => new { account = a.Id, total = a.Total, user = u.ToString()}));
        }
        // 5.1) Данные о пользователях, где сумма всех счетов больше N
        public List<object> GetUserHavingTotalMoreThanN(decimal N)
        {
            return new List<object>(Accounts.GroupBy(p => p.IdOwner).Select(t => new { total = t.Sum(r => r.Total) , owner = t.Key}).Where(u => u.total >= N).Join(Users,d=>d.owner,u => u.Id, (d,u) => new { total = d.total, user = u.ToString()}));
        }
    }
}
