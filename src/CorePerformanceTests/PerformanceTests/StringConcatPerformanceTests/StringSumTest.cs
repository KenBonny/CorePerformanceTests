﻿using System;

namespace KenBonny.CorePerformanceTests.PerformanceTests.StringConcatPerformanceTests
{
    public class StringSumTest : IPerformanceTest
    {
        public void Run()
        {
            var begin = Guid.NewGuid().ToString();
            var end = Guid.NewGuid().ToString();
            var result = begin + end;
        }
    }
}