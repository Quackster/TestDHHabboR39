using Stargazer.Networking.Game.Packets;

namespace Stargazer.Communication.Interfaces;

public interface IMessageComposer
{
    int Header { get; }
    void Compose(ServerPacket packet);
}