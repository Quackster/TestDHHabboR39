using YamlDotNet.Serialization;

namespace Stargazer.Configuration.Objects;

public class Network
{
    [YamlMember(Alias = "Game", ApplyNamingConventions = false)]
    public GameNetwork? Game { get; set; }
}