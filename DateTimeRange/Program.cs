using System;
using System.Collections.Generic;
using System.Globalization;
using DateTimeRange.DateTimeRangeCalculators;

namespace DateTimeRange
{
    public static class Program
    {
        public static void Main(string[] args)
        {

            DateTime y2018 = DateTime.Parse("01.01.2018");
            DateTime y2017 = DateTime.Parse("01.01.2017");
            DateTime y2016 = DateTime.Parse("01.01.2016");
            DateTime y2015 = DateTime.Parse("01.01.2015");

            Calendar calendar = new GregorianCalendar();
            int yyy = calendar.GetWeekOfYear(y2018, CalendarWeekRule.FirstDay, DayOfWeek.Monday);


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

            var range = rangeExctractor.GenerateDateTimeRangeFromInput(input: "CW3.2018");
        }
    }
}
