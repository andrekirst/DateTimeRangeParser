using System;
using System.Collections.Generic;

namespace DateTimeRangeParser
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            DefaultDateTimeProvider defaultDateTimeProvider = new DefaultDateTimeProvider();

            List<DateTimeRangeCalculatorBase> calculators = new CalculationsLoader().LoadCalculations();

            DateTimeRangeParser rangeExctractor = new DateTimeRangeParser(
                dateTimeProvider: defaultDateTimeProvider,
                calculators: calculators);
            rangeExctractor.RaisedCalculation += RangeExctractor_RaisedCalculation;

            DateTimeRange range = rangeExctractor.Parse(input: "thismonth..yesterday");

            Console.WriteLine(value: range);
        }

        private static void RangeExctractor_RaisedCalculation(object sender, RaisedCalculationEventArgs e)
        {
            Console.WriteLine(value: $"Raised calculation: {e.RaisedCalculator.Name}");
        }
    }
}
