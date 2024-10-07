namespace MessageWriterLibrary;

public class SecondMessageWriter : ISecondMessageWriter
{
    private readonly IMessageWriterOptions _messageWriterOptions;

    public SecondMessageWriter(IMessageWriterOptions messageWriterOptions)
    {
        _messageWriterOptions = messageWriterOptions;
    }

    public string SecondMessage() => _messageWriterOptions.SecondMessage;
}