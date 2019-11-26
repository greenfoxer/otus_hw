using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMBusinessLayer
{
    public class History
    {
        public long Id { get; private set; }
        public DateTime DateOperation { get; private set; }
        public OperationType OpType {get; private set;}
        public decimal Amount { get; private set; }
        public long IdAccount { get; private set; }
    }
}
