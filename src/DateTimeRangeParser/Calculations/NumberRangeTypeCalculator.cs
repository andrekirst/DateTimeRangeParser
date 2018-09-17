using System;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace DateTimeRangeParser.Calculations
{
    public class NumberRangeTypeCalculator : DateTimeRangeCalculatorBase
    {
        private const string Pattern = @"^([\+\-]{0,1})([1-9]\d*)(d|w|m|y)$";

        public override string Name => "NumberRangeTypeCalculator";

        public override List<CultureInfo> SupportedCultures => new List<CultureInfo>
        {
            CultureInfo.GetCultureInfoByIetfLanguageTag(name: "en")
        };

        public override DateTimeRange CalculateFromInput(string input = "")
        {
            List<string> matches = Regex.Matches(
                input: input,
                pattern: Pattern,
                options: RegexOptions.Compiled)
                .Cast<Match>()
                .Select(s => s.Groups)
                .First()
                .Select(s => s.Captures)
                .Skip(1)
                .Select(s => s.First().Value)
                .ToList();

            int faktorCalculation = string.IsNullOrEmpty(matches[0]) || matches[0] == "+" ? 1 : -1;
            int numberOfUnits = int.Parse(matches[1]);
            char dateUnit = matches[2][0];

            DateTime date = DateTime.MinValue;

            switch (dateUnit)
            {
                case 'd':
                    {
                        date = Today.AddDays(faktorCalculation * numberOfUnits);
                        break;
                    }
                case 'w':
                    {
                        date = Today.AddDays(faktorCalculation * (numberOfUnits * 7));
                        break;
                    }
                case 'm':
                    {
                        date = Today.AddMonths(faktorCalculation * numberOfUnits);
                        break;
                    }
                case 'y':
                    {
                        date = Today.AddYears(faktorCalculation * numberOfUnits);
                        break;
                    }
                default:
                    break;
            }

            return new DateTimeRange
            {
                Start = date,
                End = date
            };
        }

        public override bool DoesMatchInput(string input)
        {
            return Regex.IsMatch(
                input: input,
                pattern: Pattern,
                options: RegexOptions.Compiled);
        }
    }
}
