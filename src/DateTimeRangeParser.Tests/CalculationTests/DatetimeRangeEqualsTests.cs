﻿using System;
using Xunit;

namespace DateTimeRangeParser.Tests.CalculationTests
{
    public class DatetimeRangeEqualsTests
    {
        [Fact]
        public void DatetimeRangeEqualsTests_IsEqual()
        {
            DateTimeRange range1 = new DateTimeRange
            {
                Start = new DateTime(year: 1986, month: 4, day: 11),
                End = new DateTime(year: 2018, month: 8, day: 10)
            };

            DateTimeRange range2 = new DateTimeRange
            {
                Start = new DateTime(year: 1986, month: 4, day: 11),
                End = new DateTime(year: 2018, month: 8, day: 10)
            };

            Assert.Equal(expected: range1, actual: range2);
        }

        [Fact]
        public void DatetimeRangeEqualsTests_NotEqual()
        {
            DateTimeRange range1 = new DateTimeRange
            {
                Start = new DateTime(year: 1986, month: 4, day: 11),
                End = new DateTime(year: 2018, month: 8, day: 10)
            };

            DateTimeRange range2 = new DateTimeRange
            {
                Start = new DateTime(year: 1986, month: 4, day: 11),
                End = new DateTime(year: 2020, month: 10, day: 12)
            };

            Assert.NotEqual(expected: range1, actual: range2);
        }
    }
}
