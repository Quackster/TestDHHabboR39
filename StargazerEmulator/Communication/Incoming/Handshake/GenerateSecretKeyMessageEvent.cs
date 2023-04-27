using Org.BouncyCastle.Math;
using Stargazer.Communication.Interfaces;
using Stargazer.Communication.Outgoing.Handshake;
using Stargazer.Networking.Game.Clients;
using Stargazer.Networking.Game.Packets;
using Stargazer.Security;

namespace Stargazer.Communication.Incoming.Handshake;

public class GenerateSecretKeyMessageEvent : IMessageHandler
{
    public MessageHandlerType Type => MessageHandlerType.Handshake;
    public async Task Handle(Client client, ClientPacket packet)
    {
        // TODO: Initialize RC4
        var clientPublicKey = new BigInteger(packet.ReadString(), 10);
        var sharedKey = HexEncoding.GetBytes(client.DiffieHellman.GenerateSharedKey(clientPublicKey).ToString(16));
        client.Decoder = new Cryptography(sharedKey);
        client.HeaderDecoder = new Cryptography(sharedKey);

        await client.SendComposer(new SecretKeyComposer(client.DiffieHellman.PublicKey.ToString(10)));
    }
}