using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMBusinessLayer
{
    public class User
    {
        public long Id { get; private set; }
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public string MiddleName { get; private set; }
        public string Phone { get; private set; }
        public string IdentityData { get; private set; }
        public DateTime Datereg { get; private set; }
        public string Login { get; private set; }
        public string Password { get; private set; }
        public override string ToString()
        {
            return Name+" "+MiddleName + " " + Surname+ " Id: "+Id;
        }

        public List<Account> GetUserAccount()
        {
            throw new Exception();
        }
    }
}
