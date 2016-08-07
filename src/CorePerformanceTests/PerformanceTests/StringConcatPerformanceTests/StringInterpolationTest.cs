using System;

namespace KenBonny.CorePerformanceTests.PerformanceTests.StringConcatPerformanceTests
{
    public class StringInterpolationTest : IPerformanceTest
    {
        public void Run()
        {
            var begin = Guid.NewGuid().ToString();
            var end = Guid.NewGuid().ToString();
            var result = $"{begin} {end}";
        }
    }
}