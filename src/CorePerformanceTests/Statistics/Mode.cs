﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace KenBonny.CorePerformanceTests.Statistics
{
    public class Mode : IStatistic
    {
        public decimal Result { get; private set; }

        public decimal Calculate(IEnumerable<TimeSpan> executionTimes)
        {
            Result = executionTimes.GroupBy(x => x.Ticks).Max(x => x.Count());
            return Result;
        }
    }
}