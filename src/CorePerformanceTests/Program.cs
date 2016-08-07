using System;
using System.Collections.Generic;
using System.Linq;
using KenBonny.CorePerformanceTests.PerformanceTests;
using KenBonny.CorePerformanceTests.PerformanceTests.StringConcatCompleteTests;
using KenBonny.CorePerformanceTests.PerformanceTests.StringConcatPerformanceTests;
using KenBonny.CorePerformanceTests.Statistics;

namespace KenBonny.CorePerformanceTests
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                Console.Write("Iterations: ");
                int iterations;
                while (int.TryParse(Console.ReadLine(), out iterations))
                {
                    var performanceTests = ExecutePerformanceTests(iterations);
                    PrintPerformanceResults(performanceTests);
                    Console.WriteLine();

                    var completeTests = ExecuteCompleteTests(iterations);
                    PrintCompleteTests(completeTests);
                    Console.WriteLine();
                    Console.Write("Iterations: ");
                }
            }
            catch (Exception exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(exception);
                Console.ForegroundColor = default(ConsoleColor);
            }
        }

        private static void PrintCompleteTests(IDictionary<string, TimeSpan> results)
        {
            Console.WriteLine("For complete tests");
            foreach (var result in results.OrderBy(x => x.Value))
            {
                Console.WriteLine("{0,30}: {1,10} ms", result.Key, result.Value.Milliseconds);
            }
        }

        private static IDictionary<string, TimeSpan> ExecuteCompleteTests(int iterations)
        {
            var results = new Dictionary<string, TimeSpan>();

            foreach (var test in StringCompleteTests())
            {
                var tester = new CompleteTester(test);
                var timeSpan = tester.ExecuteTest(iterations);
                results.Add(test.GetType().Name, timeSpan);
            }

            return results;
        }

        private static void PrintPerformanceResults(IEnumerable<Result> results)
        {
            foreach (var result in results.OrderBy(x => x.StatisticType.Name).ThenBy(x => x.StatisticResult).GroupBy(x => x.StatisticType))
            {
                Console.WriteLine($"For {result.Key.Name}");
                var enumerator = result.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    Console.WriteLine(string.Format("{0,30}: {1,10} ticks", enumerator.Current.TestType.Name,
                        enumerator.Current.StatisticResult.ToString("##.000")));
                }

                Console.WriteLine();
            }
        }

        private static IEnumerable<Result> ExecutePerformanceTests(int iterations)
        {
            var results = new List<Result>();
            foreach (var test in StringPerformanceTests())
            {
                var tester = new PerformanceTester(test);
                var timeSpans = tester.ExecuteTests(iterations).ToArray();

                foreach (var statistic in GetStatistics())
                {
                    statistic.Calculate(timeSpans);
                    results.Add(new Result(test, statistic));
                }
            }

            return results;
        }

        private static IEnumerable<IPerformanceTest> StringPerformanceTests()
        {
            return new IPerformanceTest[]
            {
                new StringBuilderTest(),
                new StringFormatTest(),
                new StringInterpolationTest(),
                new StringSumTest()
            };
        }

        private static IEnumerable<ICompleteTest> StringCompleteTests()
        {
            return new ICompleteTest[]
            {
                new StringBuilderCompleteTest(), 
                new StringFormatCompleteTest(), 
                new StringInterpolationCompleteTest(), 
                new StringSumCompleteTest(), 
            };
        }

        private static IEnumerable<IStatistic> GetStatistics()
        {
            return new IStatistic[]
            {
                new Average(),
                new Median(), 
                new Mode(),
                new Total()
            };
        }
    }
}
