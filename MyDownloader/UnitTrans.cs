using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDownloader
{
    enum Unit
    {
        B,KB,MB,GB,TB,
    }
    class UnitTrans
    {
        public static string GetHandSomeNumFromByteNum(long byteNum)
        {
            Unit u = 0;
            float tmp = byteNum;
            while(tmp > 1024 && u != Unit.TB)
            {
                tmp /= 1024.0f;
                u++;
            }
            return tmp.ToString() + u.ToString();
        }
    }
}
