namespace ConfigurationInjectionSampleLibrary;

public class Dependency : IDependency
{
    private readonly IOptionForLibrary _optionForLibrary;

    public Dependency(IOptionForLibrary optionForLibrary)
    {
        _optionForLibrary = optionForLibrary;
    }
    public string WriteOneMessage() => _optionForLibrary.DependencySetting;
}