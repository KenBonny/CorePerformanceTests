namespace KenBonny.CorePerformanceTests.PerformanceTests.StringConcatCompleteTests
{
    public class StringInterpolationCompleteTest : ICompleteTest
    {
        public void Run(int iterations)
        {
            var text = string.Empty;
            for (var i = 0; i < iterations; i++)
            {
                text = $"{text} {i}";
            }
        }
    }
}