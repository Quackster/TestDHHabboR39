using YamlDotNet.Serialization;

namespace Stargazer.Configuration.Objects;

public class ServerConfig
{
    [YamlMember(Alias = "Database", ApplyNamingConventions = false)]
    public Database? Database { get; set; }
    
    [YamlMember(Alias = "Network", ApplyNamingConventions = false)]
    public Network? Network { get; set; }
}