using System;
using System.Linq;
using Xunit;
using Shouldly;

namespace DateTimeRangeParser.Tests.DateTimeRangeTests
{
    public class DateTimeRangeSpreadTests
    {
        [Fact]
        public void Spread_1_Day()
        {
            DateTimeRange range = new DateTimeRange(
                new DateTime(2019, 1, 5),
                new DateTime(2019, 1, 6));

            DateTimeRange actual = range.SpreadByDays(days: 1);
            actual.Start.ShouldBe(new DateTime(2019, 1, 4));
            actual.End.ShouldBe(new DateTime(2019, 1, 7));
        }

        [Fact]
        public void Spread_2_Days()
        {
            DateTimeRange range = new DateTimeRange(
                new DateTime(2019, 1, 5),
                new DateTime(2019, 1, 6));

            DateTimeRange actual = range.SpreadByDays(days: 2);
            actual.Start.ShouldBe(new DateTime(2019, 1, 3));
            actual.End.ShouldBe(new DateTime(2019, 1, 8));
        }

        [Fact]
        public void Spread_1_Day_for_Start_and_2_Days_for_End()
        {
            DateTimeRange range = new DateTimeRange(
                new DateTime(2019, 1, 5),
                new DateTime(2019, 1, 6));

            DateTimeRange actual = range.SpreadByDays(daysStart: 1, daysEnd: 2);
            actual.Start.ShouldBe(new DateTime(2019, 1, 4));
            actual.End.ShouldBe(new DateTime(2019, 1, 8));
        }

        [Fact]
        public void Spread_1_Week()
        {
            DateTimeRange range = new DateTimeRange(
                new DateTime(2019, 1, 5),
                new DateTime(2019, 1, 6));

            DateTimeRange actual = range.SpreadByWeeks(weeks: 1);
            actual.Start.ShouldBe(new DateTime(2018, 12, 29));
            actual.End.ShouldBe(new DateTime(2019, 1, 13));
        }

        [Fact]
        public void Spread_2_Weeks_for_Start_and_3_Weeks_for_End()
        {
            DateTimeRange range = new DateTimeRange(
                new DateTime(2019, 1, 5),
                new DateTime(2019, 1, 6));

            DateTimeRange actual = range.SpreadByWeeks(weeksStart: 2, weeksEnd: 3);
            actual.Start.ShouldBe(new DateTime(2018, 12, 22));
            actual.End.ShouldBe(new DateTime(2019, 1, 27));
        }


        [Fact]
        public void Spread_1_Month()
        {
            DateTimeRange range = new DateTimeRange(
                new DateTime(2019, 1, 5),
                new DateTime(2019, 1, 6));

            DateTimeRange actual = range.SpreadByMonths(months: 1);
            actual.Start.ShouldBe(new DateTime(2018, 12, 5));
            actual.End.ShouldBe(new DateTime(2019, 2, 6));
        }

        [Fact]
        public void Spread_2_Months_for_Start_and_3_Month_for_End()
        {
            DateTimeRange range = new DateTimeRange(
                new DateTime(2019, 1, 5),
                new DateTime(2019, 1, 6));
            DateTimeRange actual = range.SpreadByMonths(monthsStart: 2, monthsEnd: 3);
            actual.Start.ShouldBe(new DateTime(2018, 11, 5));
            actual.End.ShouldBe(new DateTime(2019, 4, 6));
        }

        [Fact]
        public void Spread_1_Year()
        {
            DateTimeRange range = new DateTimeRange(
                new DateTime(2019, 1, 5),
                new DateTime(2019, 1, 6));

            DateTimeRange actual = range.SpreadByYears(years: 1);
            actual.Start.ShouldBe(new DateTime(2018, 1, 5));
            actual.End.ShouldBe(new DateTime(2020, 1, 6));
        }

        [Fact]
        public void Spread_2_Years_for_Start_and_3_Years_for_End()
        {
            DateTimeRange range = new DateTimeRange(
                new DateTime(2019, 1, 5),
                new DateTime(2019, 1, 6));

            DateTimeRange actual = range.SpreadByYears(yearsStart: 2, yearsEnd: 3);
            actual.Start.ShouldBe(new DateTime(2017, 1, 5));
            actual.End.ShouldBe(new DateTime(2022, 1, 6));
        }
    }
}
