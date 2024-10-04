using System.ComponentModel.DataAnnotations;
using ConfigurationInjectionSampleLibrary;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ConfigurationInjectionSampleLibrary;

public static class LibraryServiceExtension
{
    public const string LibraryConfigurationSectionName = "LibrarySettings";

    public static void UseConfigurationInjectionSample(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        AddLibraryConfiguration(serviceCollection, configuration);
        AddLibraryDependencies(serviceCollection);
    }
    
    public static void AddLibraryDependencies(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IDependency, Dependency>();
        serviceCollection.AddSingleton<IAnotherDependency, AnotherDependency>();
    }
    
    public static void AddLibraryConfiguration(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddSingleton(ReadConfiguration(configuration));
    }

    private static IOptionForLibrary ReadConfiguration(IConfiguration configuration) =>
        configuration.GetRequiredSection(LibraryConfigurationSectionName).Get<OptionForLibrary>()
        ?? throw new ValidationException();
}