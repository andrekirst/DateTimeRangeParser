using System.Collections.Generic;
using System.Globalization;

namespace DateTimeRangeParser.Calculations.Cultures.German
{
    public class AktuelleWocheCalculator : CurrentWeekCalculator
    {
        public override string Name => "AktuelleWoche";

        public override bool DoesMatchInput(string input)
        {
            return EqualsLowerMatch(
                input: input,
                match: "aktuellewoche");
        }

        public override List<CultureInfo> SupportedCultures =>
            new List<CultureInfo>()
            {
                CultureInfo.GetCultureInfoByIetfLanguageTag(name: "de")
            };

        public override IEnumerable<CalculationExample> Examples
        {
            get
            {
                yield return new CalculationExample { InputString = "aktuellewoche" };
            }
        }
    }
}