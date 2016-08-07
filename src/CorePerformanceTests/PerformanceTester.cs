using System;
using System.Collections.Generic;
using System.Diagnostics;
using KenBonny.CorePerformanceTests.PerformanceTests;

namespace KenBonny.CorePerformanceTests
{
    public class PerformanceTester
    {
        private readonly IPerformanceTest test;

        public Type Type => typeof(IPerformanceTest);

        public PerformanceTester(IPerformanceTest test)
        {
            this.test = test;
        }

        public IEnumerable<TimeSpan> ExecuteTests(int iterations)
        {
            var executionTimes = new List<TimeSpan>();
            var stopwatch = new Stopwatch();

            for (var i = 0; i < iterations; i++)
            {
                stopwatch.Restart();
                test.Run();
                stopwatch.Stop();
                executionTimes.Add(stopwatch.Elapsed);
            }

            return executionTimes;
        }
    }
}