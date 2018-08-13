using System;
using System.Collections.Generic;

namespace DateTimeRange
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            DefaultDateTimeProvider defaultDateTimeProvider = new DefaultDateTimeProvider();

            List<DateTimeRangeCalculatorBase> calculators = new CalculationsLoader().LoadCalculations();

            RangeExtractor rangeExctractor = new RangeExtractor(
                dateTimeProvider: defaultDateTimeProvider,
                calculators: calculators);

            var range = rangeExctractor.GenerateDateTimeRangeFromInput(input: "currentweek");

            Console.WriteLine(range);
        }
    }
}
