using Stargazer.Communication.Interfaces;
using Stargazer.Networking.Game.Packets;

namespace Stargazer.Communication.Outgoing.Handshake;

public class UniqueMachineIDComposer : IMessageComposer
{
    private readonly string _uniqueId;

    public UniqueMachineIDComposer(string uniqueId)
    {
        _uniqueId = uniqueId;
    }

    public int Header => 439;
    public void Compose(ServerPacket packet)
    {
        packet.WriteString(_uniqueId);
    }
}