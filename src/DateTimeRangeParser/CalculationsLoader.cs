using System;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;
using DateTimeRangeParser.Extensions;

namespace DateTimeRangeParser
{
    public class CalculationsLoader
    {
        public List<DateTimeRangeCalculatorBase> LoadCalculations(List<CultureInfo> loadCulturesOf = null)
        {
            Type calculatorBaseType = typeof(DateTimeRangeCalculatorBase);
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(selector: s => s.GetTypes())
                .Where(predicate: p => p.IsPublic)
                .Where(predicate: p => !p.IsAbstract)
                .Where(predicate: p => calculatorBaseType.IsAssignableFrom(c: p))
                .Select(selector: Activator.CreateInstance)
                .Cast<DateTimeRangeCalculatorBase>()
                .FilterBySupportedCultures(toLoadingCultures: loadCulturesOf)
                .ToList();
        }
    }
}
