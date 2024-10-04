using Microsoft.Extensions.Configuration;

namespace ConfigurationInjectionSampleLibrary;

public interface IOptionForLibrary
{
    string DependencySetting { get; }
    string AnotherDependencySetting { get; }
}