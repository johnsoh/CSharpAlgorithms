using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Algorithms.GCJ.APAC2017.Round2
{
    public class C
    {
        public static void Parse()
        {
            var inAddress = @"C:\git\CSharpAlgorithms\Algorithms\Algorithms\GCJ\APAC2017\Round2\Files\CInputSmall.txt";
            var outAddress = @"C:\git\CSharpAlgorithms\Algorithms\Algorithms\GCJ\APAC2017\Round2\Files\COutputSmall.txt";
            var arr = File.ReadAllLines(inAddress);
            var ptr = 0;
            var cases = int.Parse(arr[ptr++]);

            using (var outFile = new StreamWriter(path: outAddress, append: false, encoding: Encoding.ASCII))
            {
                for (var caseNum = 1; caseNum <= cases; caseNum++)
                {
                    var input = arr[ptr++].Split(' ').Select(int.Parse).ToArray();
                    outFile.WriteLine("Case #{0}: {1}", caseNum, Solve(input[0], input[1], input[2], input[3], input[4], input[5], input[6], input[7]));
                }
            }
        }

        private static int Solve(int N, int L, int R, int A, int B, int C1, int C2, int M)
        {
            if (N <= 1) return 0;

            var intervals = new List<Interval>();
            intervals.Add(new Interval { x = L, y = R });
            for(var i = 1; i < N; i++)
            {
                var newX = (A * intervals[i - 1].x + B * intervals[i - 1].y + C1) % M;
                var newY = (A * intervals[i - 1].y + B * intervals[i - 1].x + C2) % M;
                intervals.Add(new Interval { x = newX, y = newY });
            }

            foreach(var interval in intervals)
            {
                interval.low = Math.Min(interval.x, interval.y);
                interval.high = Math.Max(interval.x, interval.y);
            }

            // generate protected (overlapping) areas 
            var overlaps = new List<Interval>();
            for(var i = 0; i < N; i++)
            {
                var interval1 = intervals[i];
                for(var j = i + 1; j < N; j++)
                {
                    var interval2 = intervals[j];
                    var overlap = GetOverlap(interval1, interval2);
                    if (overlap != null) Merge(overlap, overlaps);
                }
            }

            // find each interval's independent areas
            var max = 0;
            var maxInterval = intervals[0];
            foreach(var interval in intervals)
            {
                max = Math.Max(max, FindIndependent(interval, overlaps));
                maxInterval = interval;
            }

            // find full. get lens . minus max
            var full = new List<Interval>();
            foreach(var interval in intervals)
            {
                Merge(interval, full);
            }

            var ans  = full.Sum(Length) - max; // inclusive

            return ans;
        }

        public static int Length(Interval interval)
        {
            return interval.high - interval.low + 1;
        }

        private static int FindIndependent(Interval interval, List<Interval> overlaps)
        {
            var sum = 0;
            foreach(var interval2 in overlaps)
            {
                if(HasOverlap(interval, interval2))
                {
                    var overlap = GetOverlap(interval, interval2);
                    sum += Length(interval) - Length(overlap); // inclusive!
                }
            }
            return sum;
        }

        private static void Merge(Interval overlap, List<Interval> overlaps)
        {
            if(overlaps.Count == 0)
            {
                overlaps.Add(overlap);
                return;
            }

            var i = overlaps.Count - 1;
            while(i >= 0)
            {
                var candidate = overlaps[i];
                if(HasOverlap(overlap, candidate) || overlap.high + 1 == candidate.low || candidate.high + 1 == overlap.low)
                {
                    overlap = Merge(overlap, candidate);
                    overlaps.Remove(candidate);
                    overlaps.Add(overlap);
                }
                i--;
            }
        }

        private static Interval Merge(Interval interval1, Interval interval2)
        {
            return new Interval
            {
                low = Math.Min(interval1.low, interval2.low),
                high = Math.Max(interval1.high, interval2.high)
            };
        }

        private static bool HasOverlap(Interval interval1, Interval interval2)
        {
            return interval1.high >= interval2.low && interval1.high <= interval2.high
                || interval2.high >= interval1.low && interval2.high <= interval1.high;
        }

        private static Interval GetOverlap(Interval interval1, Interval interval2)
        {
            if(HasOverlap(interval1, interval2))
            {
                return new Interval
                {
                    low = Math.Max(interval1.low, interval2.low),
                    high = Math.Min(interval1.high, interval2.high)
                };
            }
            return null;
        }

        public class Interval
        {
            public int x;
            public int y;
            public int low;
            public int high;
        }
    }
}
