
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.GCJ.APAC2017.Round2
{
    class Spare
    {
        //return Math.Min(L, R) + 1;
        /*var set = new HashSet<string>();
        Recurse(L, R, 0, string.Empty, set);
        set.Remove(string.Empty);
        return set.Count;*/
        private static void Recurse(int L, int R, int ropen, string acc, HashSet<string> set)
        {
            if (L > 0)
            {
                Recurse(L - 1, R, ropen + 1, acc + '(', set);
            }

            if (ropen > 0 && R > 0)
            {
                Recurse(L, R - 1, ropen - 1, acc + ')', set);
            }

            if (ropen == 0)
            {
                set.Add(acc);
            }
        }
    }
}
