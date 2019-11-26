using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMBusinessLayer
{
    public class Account
    {
        public long Id { get; private set; }
        public DateTime DateCreation { get; private set; }
        public decimal Total { get; private set; }
        public long IdOwner { get; private set; }
    }
}
