using System.Collections.Generic;
using System.Globalization;

namespace DateTimeRangeParser.Calculations.Cultures.German
{
    public class HeuteCalculator : TodayCalculator
    {
        public override string Name => "Heute";

        public override bool DoesMatchInput(string input)
        {
            return EqualsLowerMatch(
                input: input,
                match: "heute");
        }

        public override List<CultureInfo> SupportedCultures =>
            new List<CultureInfo>()
            {
                CultureInfo.GetCultureInfoByIetfLanguageTag(name: "de")
            };
    }
}