using System.Reflection;

namespace DateTimeRangeParser.Abstractions.SystemAbstractions
{
    public interface IAppDomain
    {
        Assembly[] GetAssemblies();
    }
}