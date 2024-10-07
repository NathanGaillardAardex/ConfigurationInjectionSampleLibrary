using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Configuration;

namespace MessageWriterLibrary;

public sealed record MessageWriterOptions : IMessageWriterOptions
{
    public const string MessageWriterConfigurationSectionName = "MessageWriterSettings";

    // If you decided to use another casing for the configuration keys,
    // you can use the ConfigurationKeyName attribute
    [ConfigurationKeyName("first_message")]
    public required string FirstMessage { get; init; }
    public required string SecondMessage { get; init; }
}