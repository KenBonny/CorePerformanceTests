using System;

namespace KenBonny.CorePerformanceTests.PerformanceTests.StringConcatPerformanceTests
{
    public class StringFormatTest : IPerformanceTest
    {
        public void Run()
        {
            var begin = Guid.NewGuid().ToString();
            var end = Guid.NewGuid().ToString();
            var result = string.Format("{0} {1}", begin, end);
        }
    }
}