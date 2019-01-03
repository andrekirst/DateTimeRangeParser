using DateTimeRangeParser.Extensions;
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
                System.Console.WriteLine(item);
            }
        }
    }
}
