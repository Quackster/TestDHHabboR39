using Stargazer.Communication.Interfaces;
using Stargazer.Networking.Game.Packets;

namespace Stargazer.Communication.Outgoing.Handshake;

public class InitCryptoMessageComposer : IMessageComposer
{
    private readonly string _token;

    public InitCryptoMessageComposer(string token)
    {
        _token = token;
    }
    
    public int Header => 277;
    public void Compose(ServerPacket packet)
    {
        packet.WriteString(_token);
        packet.WriteBoolean(false); // isServerEncrypted
    }
}