using System;
using System.Collections.Generic;
using System.Linq;
using KenBonny.CorePerformanceTests.PerformanceTests;
using KenBonny.CorePerformanceTests.PerformanceTests.StringConcatTests;
using KenBonny.CorePerformanceTests.Statistics;

namespace KenBonny.CorePerformanceTests
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int iterations;
            Console.Write("Iterations: ");
            while (int.TryParse(Console.ReadLine(), out iterations))
            {
                var results = ExecutePerformanceTests(iterations);
                PrintResults(results);
                Console.WriteLine();
                Console.Write("Iterations: ");
            }
        }

        private static void PrintResults(IEnumerable<Result> results)
        {
            foreach (var result in results.OrderBy(x => x.StatisticResult).GroupBy(x => x.StatisticType))
            {
                Console.WriteLine($"For {result.Key.Name}");
                var enumerator = result.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    Console.WriteLine(string.Format("{0,30}: {1,8} ticks", enumerator.Current.TestType.Name,
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

        private static IEnumerable<IStatistic> GetStatistics()
        {
            return new IStatistic[]
            {
                new Average(),
                new Median(), 
                new Mode()
            };
        }
    }
}
