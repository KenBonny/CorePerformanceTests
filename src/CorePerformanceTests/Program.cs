using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using KenBonny.CorePerformanceTests.PerformanceTests;
using KenBonny.CorePerformanceTests.PerformanceTests.StringConcatTests;
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
                    var results = ExecutePerformanceTests(iterations);
                    PrintResults(results);
                    Console.WriteLine();

                    ExecuteSimpleTests(iterations);
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

        private static void ExecuteSimpleTests(int iterations)
        {
            Action<string, long> print = (name, sec) => Console.WriteLine("String {0, 15}: {1, 4} ms", name, sec);

            var result = "";
            var stopwatch = Stopwatch.StartNew();
            for (var i = 0; i < iterations; i++)
            {
                result += " " + i;
            }
            stopwatch.Stop();
            print("sum", stopwatch.ElapsedMilliseconds);

            result = "";
            stopwatch.Restart();
            for (var i = 0; i < iterations; i++)
            {
                result = string.Format("{0} {1}", result, i);
            }
            stopwatch.Stop();
            print("format", stopwatch.ElapsedMilliseconds);

            var builder = new StringBuilder();
            stopwatch.Restart();
            for (var i = 0; i < iterations; i++)
            {
                builder.Append(i);
            }
            stopwatch.Stop();
            print("builder", stopwatch.ElapsedMilliseconds);

            result = "";
            stopwatch.Restart();
            for (var i = 0; i < iterations; i++)
            {
                result = $"{result} {i}";
            }
            stopwatch.Stop();
            print("interpolation", stopwatch.ElapsedMilliseconds);
        }

        private static void PrintResults(IEnumerable<Result> results)
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
