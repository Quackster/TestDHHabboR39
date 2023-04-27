using Stargazer.Communication.Interfaces;
using Stargazer.Networking.Game.Packets;

namespace Stargazer.Communication.Outgoing.Handshake;

public class SecretKeyComposer : IMessageComposer
{
    private readonly string _publicKey;

    public SecretKeyComposer(string publicKey)
    {
        _publicKey = publicKey;
    }

    public int Header => 1;
    public void Compose(ServerPacket packet)
    {
        packet.WriteString(_publicKey);
    }
}