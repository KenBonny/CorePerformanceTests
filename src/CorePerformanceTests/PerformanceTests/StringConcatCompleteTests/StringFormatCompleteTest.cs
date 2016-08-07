namespace KenBonny.CorePerformanceTests.PerformanceTests.StringConcatCompleteTests
{
    public class StringFormatCompleteTest : ICompleteTest
    {
        public void Run(int iterations)
        {
            var text = string.Empty;
            for (var i = 0; i < iterations; i++)
            {
                text = string.Format("{0} {1}", text, i);
            }
        }
    }
}