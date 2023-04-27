using YamlDotNet.Serialization;

namespace Stargazer.Configuration.Objects;

public class GameNetwork
{
    [YamlMember(Alias = "Port", ApplyNamingConventions = false)]
    public int Port { get; set; }
}