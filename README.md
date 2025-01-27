# How to make your library easy to use:

## Add your dependencies yourself!
Your library should not ask the client to add to its dependent services itself. Your library should be configurable as a whole, meaning your library should provide an extension method to load all the dependent services. This way, the client can just call the extension method on his ``services collection`` and all the dependent services will be loaded.
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

If your config object is straightforward, you can use the `Bind` method on the configuration object to bind the configuration to your options object. This prevents you to write custom validation logic.

In .NET, You should use a camelCase name in your configuration source.

If needed, you can use a default value in the Options class to provide a default value if the configuration value is not provided.
I also recommend using the `init` keyword to make sure the options are immutable, and exposing an optional const with the configuration section name to make sure the client uses the correct section name.

```csharp
// Doing it "manually"
public sealed record MyLibraryOptions : IMyLibraryOptions
{
    public MyLibraryOptions(IConfiguration configuration){
        var libConfig = configuration.GetSection(SectionName);
        MyLibraryOption1 = libConfig.GetValue<string>(nameof(MyLibraryOption1))
            ?? throw new ConfigurationErrorsException("MyLibraryOption1 is required");
        
        MyLibraryOption2 = libConfig.GetValue<string>(nameof(MyLibraryOption2))
            ?? MyLibraryOption2;
    }
    
    public const string SectionName = "MyLibrary";
    
    public string MyLibraryOption1 { get; init; }
    
    public string MyLibraryOption2 { get; init; } = "default value";
}
```
```csharp
// Using binding
public sealed record MyLibraryOptions : IMyLibraryOptions
{
    public MyLibraryOptions(IConfiguration configuration){
        var libConfig = configuration.GetSection(SectionName);
        libConfig.Bind(this);
    }
    
    public const string SectionName = "MyLibrary";
    
    public string MyLibraryOption1 { get; init; }
    
    public string MyLibraryOption2 { get; init; } = "default value";
}
/* Created from:
{
    "MyLibraryOptions": {
        "MyLibraryOption1": "value1",
        "MyLibraryOption2": "override default value"
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
builder.Services.AddMyLibrary(new MyLibraryOptions(builder.Configuration));
```

# App Configuration
In Azure App Configuration, nested json are not allowed.
You should, for example, use a colon to separate the sections.
```json
{
    "MyLibraryOptions:MyLibraryOption1": "value1",
    "MyLibraryOptions:MyLibraryOption2": "override default value"
}
```

For usage in Azure App Configuration, your appsettings.json should only contain the connection string to the Azure App Configuration, as well as the current Environment (usually prod).