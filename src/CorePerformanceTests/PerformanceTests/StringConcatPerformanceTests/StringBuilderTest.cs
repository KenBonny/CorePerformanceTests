using System;
using System.Text;

namespace KenBonny.CorePerformanceTests.PerformanceTests.StringConcatPerformanceTests
{
    public class StringBuilderTest : IPerformanceTest
    {
        public void Run()
        {
            var begin = Guid.NewGuid().ToString();
            var end = Guid.NewGuid().ToString();
            var builder = new StringBuilder(begin);
            builder.Append(end);
            var result = builder.ToString();
        }
    }
}