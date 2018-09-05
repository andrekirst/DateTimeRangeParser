using System.Collections.Generic;
using DateTimeRangeParser.Calculations;

namespace DateTimeRangeParser
{
    public class CalculationsLoader
    {
        public List<DateTimeRangeCalculatorBase> LoadCalculations()
        {
            return new List<DateTimeRangeCalculatorBase>
            {
                new TodayCalculator(),
                new CalendarWeekCalculator(),
                new YesterdayCalculator(),
                new LastWeekCalculator(),
                new CurrentWeekCalculator(),
                new ThisMonthCalculator(),
                new ThisYearCalculator(),
                new DynamicRangeCalculator()
            };
        }
    }
}
