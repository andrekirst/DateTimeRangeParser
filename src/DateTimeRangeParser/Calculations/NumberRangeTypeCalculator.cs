using System;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;
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

        public virtual char DayCharacter => 'd';
        public virtual char WeekCharacter => 'w';
        public virtual char MonthCharacter => 'm';
        public virtual char YearCharacter => 'y';

        private Dictionary<char, Func<int, int, DateTime>> DateUnitMappings => new Dictionary<char, Func<int, int, DateTime>>
            {
                { DayCharacter, (faktorCalculation, numberOfUnits) => Today.AddDays(faktorCalculation * numberOfUnits) },
                { WeekCharacter, (faktorCalculation, numberOfUnits) => Today.AddDays(faktorCalculation * (numberOfUnits * 7)) },
                { MonthCharacter, (faktorCalculation, numberOfUnits) => Today.AddMonths(faktorCalculation * numberOfUnits) },
                { YearCharacter, (faktorCalculation, numberOfUnits) => Today.AddYears(faktorCalculation * numberOfUnits) }
            };

        public sealed override DateTimeRange CalculateFromInput(string input = "")
        {
            List<string> matches = ExtractMatchingValues(input);

            int faktorCalculation = string.IsNullOrEmpty(matches[0]) || matches[0] == "+" ? 1 : -1;
            int numberOfUnits = int.Parse(matches[1]);
            char dateUnit = matches[2][0];

            DateTime date = DateUnitMappings[dateUnit](faktorCalculation, numberOfUnits);

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

        private static List<string> ExtractMatchingValues(string input)
        {
            return Regex.Matches(
                input: input,
                pattern: Pattern,
                options: RegexOptions.Compiled)
                .Cast<Match>()
                .Select(match => match.Groups)
                .First()
                .Select(group => group.Captures)
                .Skip(1)
                .Select(captureCollection => captureCollection.First().Value)
                .ToList();
        }
    }
}
