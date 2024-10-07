# How to make your library easy to use:

## Add your dependencies yourself!
Your library should not ask the client to add to its dependent services itself. Your library should be configurable as a whole, meaning your library should provide an extension method to load all the dependent services. This way, the client can just call the extension method on his ``services collection`` and all the dependent services will be loaded.
```csharp
public static class MyLibraryServiceExtension
{
    public static IServiceCollection AddMyLibrary(this IServiceCollection services)
    {
        services.AddSingleton<IMyLibraryService, MyLibraryService>();
        return services;
    }
}
```
```csharp
builder.Services.AddMyLibrary();
```

## Your library options should be typed.
Your library should provide an options class to the client to configure the library. This way, the client can configure the library using the options class, or create it from a configuration via a provided static method.
```csharp
// Interfacing your options is good for testing, but no other child classes should be written.
public interface IMyLibraryOptions
{
    string MyLibraryOption1 { get; }
    string MyLibraryOption2 { get; }
}

public sealed record MyLibraryOptions : IMyLibraryOptions
{
    public string MyLibraryOption1 { get; init; }
    public string MyLibraryOption2 { get; init; }
}
```
## Injecting your options is a piece of cake :)
To create the option object from the configuration is easily made with a typed `Get` on the configuration object.
This way, it is also easy to crash (fail fast) the application if the configuration is not valid. Instead of waiting for the first call to the library to fail.
```csharp
public static IMyLibraryOptions GetMyLibraryOptions(this IConfiguration configuration)
{
    return configuration.GetSection("MyLibraryOptions").Get<MyLibraryOptions>()
        ?? throw new ValidationException("MyLibraryOptions is not configured");
}
```
If you need to use a different name in your configuration source, you can use ``ConfigurationKeyName`` attribute on the option class.

You can use the required keyword to make sure the configuration is valid.
Or you can use a default value in the Options class to provide a default value if the configuration value is not provided.
I also recommend using the `init` keyword to make sure the options are immutable, and exposing a const with the configuration section name to make sure the client uses the correct section name.
```csharp
public record MyLibraryOptions : IMyLibraryOptions
{
    public const string SectionName = "MyLibraryOptions";
    [ConfigurationKeyName("my_library_option1")]
    public required string MyLibraryOption1 { get; init; }
    [ConfigurationKeyName("my_library_option2")]
    public string MyLibraryOption2 { get; init; } = "default value";
}
/* Created from:
{
    "MyLibraryOptions": {
        "my_library_option1": "value1",
        "my_library_option2": "override default value"
    }
}
*/
```
You can then use the options in your library like this:
```csharp
public static class MyLibraryServiceExtension
{
    public static IServiceCollection AddMyLibrary(this IServiceCollection services, IMyLibraryOptions options)
    {
        services.AddSingleton(options);
        services.AddSingleton<IMyLibraryService, MyLibraryService>();
        return services;
    }
}
```
```csharp
builder.Services.AddMyLibrary(builder.Configuration.GetMyLibraryOptions());
```
