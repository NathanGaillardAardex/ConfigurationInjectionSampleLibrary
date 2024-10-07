namespace MessageWriterLibrary;

public class FirstMessageWriter : IFirstMessageWriter
{
    private readonly IMessageWriterOptions _messageWriterOptions;

    public FirstMessageWriter(IMessageWriterOptions messageWriterOptions)
    {
        _messageWriterOptions = messageWriterOptions;
    }
    public string FirstMessage() => _messageWriterOptions.FirstMessage;
}