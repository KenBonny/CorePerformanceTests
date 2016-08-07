namespace KenBonny.CorePerformanceTests.PerformanceTests.StringConcatCompleteTests
{
    public class StringSumCompleteTest : ICompleteTest
    {
        public void Run(int iterations)
        {
            var text = string.Empty;
            for (var i = 0; i < iterations; i++)
            {
                text += " " + i;
            }
        }
    }
}