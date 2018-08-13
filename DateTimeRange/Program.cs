using System;
using System.Collections.Generic;
using System.Reflection;
using DateTimeRange.DateTimeRangeCalculators;

namespace DateTimeRange
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            DefaultDateTimeProvider defaultDateTimeProvider = new DefaultDateTimeProvider();

            List<DateTimeRangeCalculatorBase> calculators = new List<DateTimeRangeCalculatorBase>
            {
                new TodayCalculator(),
                new CalendarWeekCalculator(),
                new YesterdayCalculator(),
                new LastWeekCalculator(),
                new CurrentWeekCalculator(),
                new ThisMonthCalculator(),
                new ThisYearCalculator()
            };

            // range=CW3.2018 => er muss es herausfinden, welche Implementation es ist (Dynamic)
            // range=03.08.2018->05.08.2018 => er muss es herausfinden, welche Implementation es ist (Dynamic)
            // Idee: Eventuell anhand eines Regex?

            // range=Yesterday => er weiß, welche Implementation ist (Static) -> OK

            RangeExtractor rangeExctractor = new RangeExtractor(
                dateTimeProvider: defaultDateTimeProvider,
                calculators: calculators);

            var range = rangeExctractor.GenerateDateTimeRangeFromInput(input: "currentweek");

            Console.WriteLine(range);
        }
    }
}
