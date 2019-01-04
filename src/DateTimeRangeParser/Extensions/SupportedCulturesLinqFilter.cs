using System.Linq;
using System.Collections.Generic;
using System.Globalization;

namespace DateTimeRangeParser.Extensions
{
    public static class SupportedCulturesLinqFilter
    {
        public static IEnumerable<DateTimeRangeCalculatorBase> FilterBySupportedCultures(
            this IEnumerable<DateTimeRangeCalculatorBase> calculations,
            List<CultureInfo> toLoadingCultures = null)
        {
            if (toLoadingCultures != null && toLoadingCultures.Any())
            {
                return calculations.Where(predicate: calculator =>
                    calculator.SupportedCultures == null ||
                    calculator.SupportedCultures.Intersect(second: toLoadingCultures).Any() ||
                    calculator.SupportedCultures.Intersect(second: toLoadingCultures.Select(selector: s => s.Parent)).Any());
            }
            return calculations;
        }
    }
}