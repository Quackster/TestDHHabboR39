using DotNetty.Transport.Channels;
using Microsoft.Extensions.Logging;
using Stargazer.Networking.Game.Codecs;

namespace Stargazer.Networking.Game;

public class GameNetworkServer : AbstractNetworkServer
{
    private static GameNetworkServer? _instance;
    public static GameNetworkServer Instance => _instance ??= new GameNetworkServer();
    
    public override async Task Start()
    {
        await base.Start();
        
        Logger.LogInformation("GameNetworkServer initialized, bound on port {Port}", Port);
    }

    protected override Action<IChannel> ChannelHandlerInitializer => channel =>
    {
        channel.Pipeline.AddLast("decoder", new Decoder());
        channel.Pipeline.AddLast("encoder", new Encoder());
        channel.Pipeline.AddLast("handler", new GameNetworkHandler());
    };

    protected override int Port => Stargazer.Config?.Network?.Game?.Port ?? 0;
}