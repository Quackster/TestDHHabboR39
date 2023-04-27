using DotNetty.Transport.Channels;
using Microsoft.Extensions.Logging;
using Stargazer.Communication;
using Stargazer.Logging;
using Stargazer.Networking.Game.Clients;
using Stargazer.Networking.Game.Packets;

namespace Stargazer.Networking.Game;

public class GameNetworkHandler : SimpleChannelInboundHandler<ClientPacket>
{
    private readonly ILogger<GameNetworkHandler> _logger = LoggerManager.GetLogger<GameNetworkHandler>();

    public override void ChannelActive(IChannelHandlerContext context)
    {
        if (!ClientManager.Instance.TryAddClient(context.Channel, out var client))
        {
            context.Channel.DisconnectAsync().Wait();
        }
        
        _logger.LogInformation("New connection from {RemoteAddr}", context.Channel.RemoteAddress);
    }

    public override void ChannelInactive(IChannelHandlerContext context)
    {
        if (!ClientManager.Instance.TryRemoveClient(context.Channel))
        {
            return;
        }

        _logger.LogInformation("Lost connection {RemoteAddr}", context.Channel.RemoteAddress);
    }

    protected override async void ChannelRead0(IChannelHandlerContext ctx, ClientPacket msg)
    {
        if (!ClientManager.Instance.TryGetClient(ctx.Channel, out var client) || client == null)
        {
            ctx.DisconnectAsync().Wait();
            return;
        }

        var header = msg.ReadBase64();

        if (MessageHandlerManager.Instance.TryGetHandler(header, out var handler) && handler != null)
        {
            Console.WriteLine("Invoked " + handler.GetType().Name);
            await handler.Handle(client, msg);
        }
        else
        {
            // _logger.LogWarning("Unregistered Message Handler for ID {Header} - {Body}", header, msg.ToString());
        }
    }

    public override void ExceptionCaught(IChannelHandlerContext context, Exception exception)
    {
        _logger.LogError("{Error}", exception.ToString());
        throw exception;
    }
}