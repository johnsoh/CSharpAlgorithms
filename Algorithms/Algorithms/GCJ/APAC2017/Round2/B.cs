using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Algorithms.GCJ.APAC2017.Round2
{
    public class B
    {
        public static void Parse()
        {
            var inAddress = @"C:\git\CSharpAlgorithms\Algorithms\Algorithms\GCJ\APAC2017\Round2\Files\BInputSmall.txt";
            var outAddress = @"C:\git\CSharpAlgorithms\Algorithms\Algorithms\GCJ\APAC2017\Round2\Files\BOutputSmall.txt";
            var arr = File.ReadAllLines(inAddress);
            var ptr = 0;
            var cases = int.Parse(arr[ptr++]);

            using (var outFile = new StreamWriter(path: outAddress, append: false, encoding: Encoding.ASCII))
            {
                for (var caseNum = 1; caseNum <= cases; caseNum++)
                {
                    var words = int.Parse(arr[ptr++]);
                    outFile.WriteLine("Case #{0}: {1}", caseNum, Solve());
                }
            }
        }

        private static int Solve()
        {
            throw new NotImplementedException();
        }
    }
}
