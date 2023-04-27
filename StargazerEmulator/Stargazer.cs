using Microsoft.Extensions.Logging;
using Org.BouncyCastle.Math;
using Stargazer.Communication;
using Stargazer.Configuration;
using Stargazer.Configuration.Objects;
using Stargazer.Logging;
using Stargazer.Networking.Game;

namespace Stargazer;

public class Stargazer
{
    private static readonly ILogger<Stargazer> Logger = LoggerManager.GetLogger<Stargazer>();
    public static ServerConfig? Config { get; set; }

    public const string Prime =
        "e49afaa7505ba1a24c0234aff2f4d6490551fc9d784276acd67b2425a96c81b352c0febb472b2d1249b8d75115bde0e35b9ea3492341eba8291b178930b21022dc4b5295f38f2dae7dc5fd99b708842776995dfcad8295a608a9ac5cca2713df77dcd3474d901727bbc428c4cfd83d3c2158f8d01b55d226e1af96af83ecfa0334c4357d88b6ddfb66b4a41abea68ac9d21b123a0c3ea076653be384de215cb9f5273fb13340f5551298e824a23a30f2682f4392b794972bae769a77011c5e2a9006377fbb017d0b1fad07227206e6f76cdaa84c1c0d9c14d73110e33227de526b368d927b1bd356bae78e95a5ab7496f9c1f4fa96839eb5c8a161ca406f30a1";

    public const string Generator = "6";
    
    public static async Task Boot()
    {
        Logger.LogInformation("Initializing Stargazer for {User}", Environment.UserName);
        
        Config = ConfigLoader.GetConfig<ServerConfig>("server_config.yaml");
        
        MessageHandlerManager.Instance.RegisterHandlers();

        await GameNetworkServer.Instance.Start();
    }
}