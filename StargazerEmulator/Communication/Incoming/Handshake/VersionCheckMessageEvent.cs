using Stargazer.Communication.Interfaces;
using Stargazer.Networking.Game.Clients;
using Stargazer.Networking.Game.Packets;

namespace Stargazer.Communication.Incoming.Handshake;

public class VersionCheckMessageEvent : IMessageHandler
{
    public MessageHandlerType Type => MessageHandlerType.Handshake;
    public Task Handle(Client client, ClientPacket packet)
    {
        // BLA...
        return Task.CompletedTask;
    }
}