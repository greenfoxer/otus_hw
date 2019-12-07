using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace home_work_6
{
    class F
    {
        [JsonRequired]
        int i1, i2, i3, i4, i5;
        [JsonRequired]
        string s1;
        public F()
        {
            i1 = 1; i2 = 2; i3 = 3; i4 = 4; i5 = 5; s1 = "example";
        }
    }
}
