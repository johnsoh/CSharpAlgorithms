using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Algorithms.GCJ.APAC2017.Round1
{
    public class A
    {
        public static void Parse()
        {
            var inAddress = @"C:\git\CSharpAlgorithms\Algorithms\Algorithms\GCJ\APAC2017\Round1\AInputSmall.txt";
            var outAddress = @"C:\git\CSharpAlgorithms\Algorithms\Algorithms\GCJ\APAC2017\Round1\AOutputSmall.txt";
            var arr = File.ReadAllLines(inAddress);
            var ptr = 0;
            var cases = int.Parse(arr[ptr++]);

            using (var outFile = new StreamWriter(path: outAddress, append: false, encoding: System.Text.ASCIIEncoding.ASCII))
            {
                for (var caseNum = 1; caseNum <= cases; caseNum++)
                {
                    var words = int.Parse(arr[ptr++]);
                    var list = new List<string>();
                    for (var word = 0; word < words; word++) list.Add(arr[ptr++]);
                    //while (words-- >= 0) list.Add(arr[ptr++]);

                    outFile.WriteLine("Case #{0}: {1}", caseNum, Solve(list));
                }
            }
        }

        public static string Solve(List<string> names)
        {
            var info = names.Select(name => new
            {
                Name = name,
                UniqueCount = (new HashSet<char>(name.ToCharArray())).Count
            }).ToList();
            var solution = info.OrderByDescending(x => x.UniqueCount).ThenBy(x => x.Name).First().Name;
            return solution;
        }
    }
}
