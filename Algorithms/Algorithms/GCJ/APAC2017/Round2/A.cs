using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Algorithms.GCJ.APAC2017.Round2
{
    public class A
    {
        public static void Parse()
        {
            var inAddress = @"C:\git\CSharpAlgorithms\Algorithms\Algorithms\GCJ\APAC2017\Round2\Files\AInputSmall.txt";
            var outAddress = @"C:\git\CSharpAlgorithms\Algorithms\Algorithms\GCJ\APAC2017\Round2\Files\AOutputSmall.txt";
            var arr = File.ReadAllLines(inAddress);
            var ptr = 0;
            var cases = int.Parse(arr[ptr++]);

            using (var outFile = new StreamWriter(path: outAddress, append: false, encoding: Encoding.ASCII))
            {
                for (var caseNum = 1; caseNum <= cases; caseNum++)
                {
                    var ar = arr[ptr++].Split(' ').Select(int.Parse).ToArray();
                    var L = ar[0];
                    var R = ar[1];
                    var ans = Solve(L, R);
                    outFile.WriteLine("Case #{0}: {1}", caseNum, ans);
                }
            }
        }

        private static int Solve(int L, int R)
        {
            return Enumerable.Range(0, Math.Min(L, R + 1)).Sum();
        }

    }
}
