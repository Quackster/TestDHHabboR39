using YamlDotNet.Serialization;

namespace Stargazer.Configuration.Objects;

public class Database
{
    [YamlMember(Alias = "Hostname", ApplyNamingConventions = false)]
    public string? Hostname { get; set; }
}