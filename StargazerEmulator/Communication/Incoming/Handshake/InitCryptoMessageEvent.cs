using Org.BouncyCastle.Math;
using Stargazer.Communication.Interfaces;
using Stargazer.Communication.Outgoing.Handshake;
using Stargazer.Networking.Game.Clients;
using Stargazer.Networking.Game.Packets;
using Stargazer.Security;

namespace Stargazer.Communication.Incoming.Handshake;

public class InitCryptoMessageEvent : IMessageHandler
{
    public MessageHandlerType Type => MessageHandlerType.Handshake;

    public async Task Handle(Client client, ClientPacket packet)
    {
        client.DiffieHellman =
            new DiffieHellman(new BigInteger(Stargazer.Prime, 16), new BigInteger(Stargazer.Generator, 16));

        var token = DiffieHellman.generateRandomNumString(24);

        client.Token = token;
        client.GenerateTx();
        
        await client.SendComposer(new InitCryptoMessageComposer(token));
    }
}