using System;
using System.Collections.Generic;

namespace KenBonny.CorePerformanceTests.Statistics
{
    public interface IStatistic
    {
        decimal Result { get; }

        decimal Calculate(IEnumerable<TimeSpan> executionTimes);
    }
}