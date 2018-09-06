using System;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;
using DateTimeRangeParser.Extensions;
using DateTimeRangeParser.Abstractions.SystemAbstractions;

namespace DateTimeRangeParser
{
    public class CalculationsLoader
    {
        private readonly IAppDomain _appDomain;

        public CalculationsLoader(IAppDomain appDomain)
        {
            _appDomain = appDomain;
        }

        public List<DateTimeRangeCalculatorBase> LoadCalculations(List<CultureInfo> loadCulturesOf = null)
        {
            Type calculatorBaseType = typeof(DateTimeRangeCalculatorBase);
            return _appDomain.GetAssemblies()
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
