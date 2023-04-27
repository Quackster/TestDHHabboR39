using Stargazer.Communication.Interfaces;
using Stargazer.Communication.Outgoing.Handshake;
using Stargazer.Networking.Game.Clients;
using Stargazer.Networking.Game.Packets;

namespace Stargazer.Communication.Incoming.Handshake;

public class UniqueIDMessageEvent : IMessageHandler
{
    public MessageHandlerType Type => MessageHandlerType.Handshake;

    public async Task Handle(Client client, ClientPacket packet)
    {
        await client.SendComposer(new UniqueMachineIDComposer("beepgoesthejeep"));
        await client.SendComposer(new AuthenticationOKMessageComposer());
    }
}