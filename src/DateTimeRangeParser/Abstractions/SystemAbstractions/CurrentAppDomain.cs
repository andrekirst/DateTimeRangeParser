using System;
using System.Reflection;

namespace DateTimeRangeParser.Abstractions.SystemAbstractions
{
    public class CurrentAppDomain : IAppDomain
    {
        public Assembly[] GetAssemblies()
        {
            return AppDomain.CurrentDomain.GetAssemblies();
        }
    }
}