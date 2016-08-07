using System;
using KenBonny.CorePerformanceTests.PerformanceTests;
using KenBonny.CorePerformanceTests.Statistics;

namespace KenBonny.CorePerformanceTests
{
    public class Result
    {
        public Type TestType { get; protected set; }

        public Type StatisticType { get; protected set; }

        public decimal StatisticResult { get; protected set; }

        public Result(IPerformanceTest test, IStatistic statistic)
        {
            TestType = test.GetType();
            StatisticType = statistic.GetType();
            StatisticResult = statistic.Result;
        }
    }
}
