using Stargazer.Communication.Interfaces;
using Stargazer.Networking.Game.Packets;

namespace Stargazer.Communication.Outgoing.Handshake;

public class AuthenticationOKMessageComposer : IMessageComposer
{
    public int Header => 3;
    public void Compose(ServerPacket packet)
    {
    }
}