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
                .SelectMany(selector: assembly => assembly.GetTypes())
                .Where(predicate: type => type.IsPublic)
                .Where(predicate: type => !type.IsAbstract)
                .Where(predicate: type => calculatorBaseType.IsAssignableFrom(c: type))
                .Select(selector: Activator.CreateInstance)
                .Cast<DateTimeRangeCalculatorBase>()
                .FilterBySupportedCultures(toLoadingCultures: loadCulturesOf)
                .ToList();
        }
    }
}
