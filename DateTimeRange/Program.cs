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
            rangeExctractor.RaisedCalculation += RangeExctractor_RaisedCalculation;

            DateTimeRange range = rangeExctractor.GenerateDateTimeRangeFromInput(input: "today");

            Console.WriteLine(range);
        }

        private static void RangeExctractor_RaisedCalculation(object sender, RaisedCalculationEventArgs e)
        {
            Console.WriteLine($"Raised calculation: {e.RaisedCalculator.Name}");
        }
    }
}
