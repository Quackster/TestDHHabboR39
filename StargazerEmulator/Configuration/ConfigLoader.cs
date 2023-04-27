using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Stargazer.Configuration;

public class ConfigLoader
{
    public static T? GetConfig<T>(string path)
    {
        if (!File.Exists(path))
        {
            // TODO: Give error
            return default;
        }

        var serializer = new DeserializerBuilder().WithNamingConvention(CamelCaseNamingConvention.Instance).Build();

        try
        {
            return (T)serializer.Deserialize<T>(File.ReadAllText(path));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            // TODO: Give error
            return default;
        }
    }
}