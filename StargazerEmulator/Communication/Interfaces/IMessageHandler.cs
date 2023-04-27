using Stargazer.Networking.Game.Clients;
using Stargazer.Networking.Game.Packets;

namespace Stargazer.Communication.Interfaces;

public interface IMessageHandler
{
    MessageHandlerType Type { get; }
    Task Handle(Client client, ClientPacket packet);
}