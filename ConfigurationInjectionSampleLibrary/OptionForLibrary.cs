using System.Text.Json.Serialization;
using Microsoft.Extensions.Configuration;

namespace ConfigurationInjectionSampleLibrary;

public record OptionForLibrary : IOptionForLibrary
{
    [ConfigurationKeyName("dependency_setting")]
    public required string DependencySetting { get; init; }

    [ConfigurationKeyName("another_dependency_setting")]
    public required string AnotherDependencySetting { get; init; }
}