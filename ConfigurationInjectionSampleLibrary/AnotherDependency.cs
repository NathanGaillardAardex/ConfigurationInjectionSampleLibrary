namespace ConfigurationInjectionSampleLibrary;

public class AnotherDependency : IAnotherDependency
{
    private readonly IOptionForLibrary _optionForLibrary;

    public AnotherDependency(IOptionForLibrary optionForLibrary)
    {
        _optionForLibrary = optionForLibrary;
    }

    public string WriteAnotherMessage() => _optionForLibrary.AnotherDependencySetting;
}