namespace DateTimeRangeParser.Extensions
{
    public static class DateTimeRangeParserExtensions
    {
        public static DateTimeRangeParser EnableCaching(this DateTimeRangeParser dateTimeRangeParser)
        {
            dateTimeRangeParser.CachingEnabled = true;
            return dateTimeRangeParser;
        }

        public static DateTimeRangeParser DisableCaching(this DateTimeRangeParser dateTimeRangeParser)
        {
            dateTimeRangeParser.CachingEnabled = false;
            return dateTimeRangeParser;
        }
    }
}
