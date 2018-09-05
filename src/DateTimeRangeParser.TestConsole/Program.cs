using System;
using System.Collections.Generic;
using System.Globalization;

namespace DateTimeRangeParser.TestConsole
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var b1 = CultureInfo.GetCultureInfoByIetfLanguageTag("en");
            var b2 = CultureInfo.GetCultureInfoByIetfLanguageTag("de");
            var b3 = CultureInfo.GetCultureInfo(1031);

            DateTimeRangeParser parser = DateTimeRangeParser.CreateDefault(
                supportedCulsturesToLoad: new List<CultureInfo>(){ CultureInfo.GetCultureInfoByIetfLanguageTag("de") });

            var range = parser.Parse(input: "aktuellewoche->heute");
            Console.WriteLine(value: range);
        }
    }
}
