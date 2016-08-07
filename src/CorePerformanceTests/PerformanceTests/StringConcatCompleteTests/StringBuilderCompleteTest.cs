using System.Text;

namespace KenBonny.CorePerformanceTests.PerformanceTests.StringConcatCompleteTests
{
    public class StringBuilderCompleteTest : ICompleteTest
    {
        public void Run(int iterations)
        {
            var builder = new StringBuilder();
            for (var i = 0; i < iterations; i++)
            {
                builder.Append(i);
            }

            var text = builder.ToString();
        }
    }
}