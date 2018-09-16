using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace DateTimeRangeParser.Calculations
{
    public class NumberRangeTypeCalculator : DateTimeRangeCalculatorBase
    {
        public override string Name => "NumberRangeTypeCalculator";

        public override List<CultureInfo> SupportedCultures => new List<CultureInfo>
        {
            CultureInfo.GetCultureInfoByIetfLanguageTag(name: "en")
        };

        public override DateTimeRange CalculateFromInput(string input = "")
        {
            throw new NotImplementedException();
        }

        public override bool DoesMatchInput(string input)
        {
            return Regex.IsMatch(
                input: input,
                pattern: @"^([\+\-]{0,1})([1-9]\d*)(w|d|y)$",
                options: RegexOptions.Compiled);
        }
    }
}
