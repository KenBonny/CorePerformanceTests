using System;
using System.Collections.Generic;
using System.Diagnostics;
using KenBonny.CorePerformanceTests.PerformanceTests;

namespace KenBonny.CorePerformanceTests
{
    internal class CompleteTester
    {
        private readonly ICompleteTest test;

        public Type Type => typeof(ICompleteTest);

        public CompleteTester(ICompleteTest test)
        {
            this.test = test;
        }

        public TimeSpan ExecuteTest(int iterations)
        {
            var stopwatch = Stopwatch.StartNew();
            test.Run(iterations);
            stopwatch.Stop();
            return stopwatch.Elapsed;
        }
    }
}