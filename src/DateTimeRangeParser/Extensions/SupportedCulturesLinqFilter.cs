using System;
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
                return calculations.Where(predicate: b =>
                    b.SupportedCultures == null ||
                    b.SupportedCultures.Intersect(second: toLoadingCultures).Any() ||
                    b.SupportedCultures.Intersect(second: toLoadingCultures.Select(selector: s => s.Parent)).Any());
            }
            return calculations;
        }
    }
}