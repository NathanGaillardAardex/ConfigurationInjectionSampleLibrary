using Microsoft.Extensions.Configuration;

namespace MessageWriterLibrary;

public sealed record MessageWriterOptions : IMessageWriterOptions
{
    public static readonly string MessageWriterConfigurationSectionName = "MessageWriter";
    
    public MessageWriterOptions(IConfiguration configuration, string? section = null)
    {
        var options = configuration
            .GetRequiredSection(section ?? MessageWriterConfigurationSectionName);

        FirstMessage = options.GetValue<string>(nameof(FirstMessage))
                       ?? throw new Exception($"{nameof(FirstMessage)} is not present");

        SecondMessage = options.GetValue<string>(nameof(SecondMessage))
                        ?? SecondMessage;
    }
    
    public string FirstMessage { get; init; }
    public string SecondMessage { get; init; } = "default second message";
}