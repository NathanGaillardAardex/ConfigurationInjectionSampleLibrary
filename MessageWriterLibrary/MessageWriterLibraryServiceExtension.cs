using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MessageWriterLibrary;

public static class MessageWriterLibraryServiceExtension
{
    public static IServiceCollection AddMessageWriterLibrary(this IServiceCollection serviceCollection,
        IMessageWriterOptions options)
    {
        AddLibraryConfiguration(serviceCollection, options);
        AddDependencies(serviceCollection);
        return serviceCollection;
    }

    private static void AddDependencies(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IFirstMessageWriter, FirstMessageWriter>();
        serviceCollection.AddSingleton<ISecondMessageWriter, SecondMessageWriter>();
    }

    private static void AddLibraryConfiguration(this IServiceCollection serviceCollection, IMessageWriterOptions options)
    {
        serviceCollection.AddSingleton(options);
    }
    
    public static IMessageWriterOptions GetMessageWriterOptions(this IConfiguration configuration) =>
        configuration.GetRequiredSection(MessageWriterOptions.MessageWriterConfigurationSectionName).Get<MessageWriterOptions>()
        ?? throw new ValidationException();
}