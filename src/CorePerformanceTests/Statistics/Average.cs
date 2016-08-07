using System;
using System.Collections.Generic;
using System.Linq;

namespace KenBonny.CorePerformanceTests.Statistics
{
    public class Average : IStatistic
    {
        public decimal Result { get; private set; }

        public decimal Calculate(IEnumerable<TimeSpan> executionTimes)
        {
            var timeSpans = executionTimes as TimeSpan[] ?? executionTimes.ToArray();
            var totalTicks = (decimal)timeSpans.Sum(x => x.Ticks);
            Result = totalTicks/timeSpans.Length;
            return Result;
        }
    }
}
