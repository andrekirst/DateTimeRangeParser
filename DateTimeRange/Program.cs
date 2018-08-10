using System.Collections.Generic;
using DateTimeRange.DateTimeRangeCalculators;

namespace DateTimeRange
{
    public class Program
    {
        public static void Main(string[] args)
        {
            DefaultDateTimeProvider defaultDateTimeProvider = new DefaultDateTimeProvider();

            List<IDateTimeRangeCalculator> calculators = new List<IDateTimeRangeCalculator>
            {
                new TodayCalculator(),
                new CalendarWeekCalculator()
                /*,
                new YesterdayCalculator(dateTimeProvider: defaultDateTimeProvider),
                new LastWeekCalculator(dateTimeProvider: defaultDateTimeProvider),
                new CurrentWeekCalculator(dateTimeProvider: defaultDateTimeProvider),
                new ThisMonthCalculator(dateTimeProvider: defaultDateTimeProvider),
                new ThisYearCalculator(dateTimeProvider: defaultDateTimeProvider)*/
            };

            // range=CW3.2018 => er muss es herausfinden, welche Implementation es ist (Dynamic)
            // range=03.08.2018->05.08.2018 => er muss es herausfinden, welche Implementation es ist (Dynamic)
            // Idee: Eventuell anhand eines Regex?

            // range=Yesterday => er weiß, welche Implementation ist (Static) -> OK

            RangeExtractor rangeExctractor2 = new RangeExtractor(
                dateTimeProvider: defaultDateTimeProvider,
                calculators: calculators);

            var range = rangeExctractor2.GenerateDateTimeRangeFromInput("CW3.2018");
        }
    }
}
