using System;

namespace KenBonny.CorePerformanceTests.PerformanceTests
{
    public class ActionTest : IPerformanceTest
    {
        private readonly Action action;

        public ActionTest(Action action)
        {
            this.action = action;
        }

        public void Run()
        {
            action();
        }
    }
}