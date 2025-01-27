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
}