using System;
using System.Collections.Generic;
using System.Linq;

namespace KenBonny.CorePerformanceTests.Statistics
{
    public class Total : IStatistic
    {
        public decimal Result { get; private set; }
        public decimal Calculate(IEnumerable<TimeSpan> executionTimes)
        {
            Result = executionTimes.Sum(x => x.Ticks);
            return Result;
        }
    }
}