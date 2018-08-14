using System;

namespace DateTimeRange
{
    public class RaisedCalculationEventArgs : EventArgs
    {
        public RaisedCalculationEventArgs(DateTimeRangeCalculatorBase dateTimeRangeCalculatorBase)
        {
            RaisedCalculator = dateTimeRangeCalculatorBase;
        }

        public DateTimeRangeCalculatorBase RaisedCalculator { get; private set; }
    }
}