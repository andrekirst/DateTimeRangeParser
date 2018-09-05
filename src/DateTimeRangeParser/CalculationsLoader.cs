using System;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;
using DateTimeRangeParser.Calculations;
using DateTimeRangeParser.Calculations.Cultures.German;

namespace DateTimeRangeParser
{
    public class CalculationsLoader
    {
        public List<DateTimeRangeCalculatorBase> LoadCalculations(List<CultureInfo> loadCulturesOf = null)
        {
            Type calculatorBaseType = typeof(DateTimeRangeCalculatorBase);
            var result = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(selector: s => s.GetTypes())
                .Where(predicate: p => !p.IsAbstract)
                .Where(predicate: p => calculatorBaseType.IsAssignableFrom(c: p))
                .Select(selector: Activator.CreateInstance)
                .Cast<DateTimeRangeCalculatorBase>();

            if (loadCulturesOf != null && loadCulturesOf.Any())
            {
                return result.Where(predicate: b =>
                    b.SupportedCultures == null ||
                    b.SupportedCultures.Intersect(second: loadCulturesOf).Any()).ToList();
            }

            return result.ToList();
        }
    }
}
