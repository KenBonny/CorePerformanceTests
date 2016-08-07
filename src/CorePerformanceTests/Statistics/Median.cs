using System;
using System.Collections.Generic;
using System.Linq;

namespace KenBonny.CorePerformanceTests.Statistics
{
    public class Median : IStatistic
    {
        public decimal Result { get; private set; }

        public decimal Calculate(IEnumerable<TimeSpan> executionTimes)
        {
            var orderedTimes = executionTimes.OrderBy(x => x.Ticks).ToArray();
            Result = orderedTimes.Length%2 == 0 ? EvenMedian(orderedTimes) : UnevenMedian(orderedTimes);
            return Result;
        }

        private static decimal EvenMedian(IReadOnlyList<TimeSpan> executionTimes)
        {
            var middle = executionTimes.Count/2;
            return executionTimes[middle].Ticks;
        }

        private static decimal UnevenMedian(IReadOnlyList<TimeSpan> executionTimes)
        {
            var middle = (executionTimes.Count - 1)/2;
            var prevMiddleTicks = executionTimes[middle].Ticks;
            var postMiddleTicks = executionTimes[middle + 1].Ticks;
            return (decimal)(prevMiddleTicks + postMiddleTicks)/2;
        }
    }
}
