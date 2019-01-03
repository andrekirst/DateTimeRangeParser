using DateTimeRangeParser.Extensions;
using System;
using System.Linq;

namespace DateTimeRangeParser.TestConsole
{
    public class Program
    {
        public static void Main(string[] args)
        {
            DateTimeRangeParser parser = new DateTimeRangeParser();
            foreach (var item in parser.ImplementedCalculations)
            {
                Console.WriteLine(item);
                if (item.Examples != null)
                {
                    foreach (CalculationExample example in item.Examples)
                    {
                        Console.WriteLine($" - Input: \"{example.InputString}\" results value: {parser.Parse(input: example.InputString)}");
                    } 
                }
            }
        }
    }
}
