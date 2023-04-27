using System.Collections.Concurrent;
using DotNetty.Buffers;
using DotNetty.Transport.Channels;
using Stargazer.Communication;
using Stargazer.Communication.Interfaces;
using Stargazer.Networking.Game.Packets;
using Stargazer.Security;

namespace Stargazer.Networking.Game.Clients;

public class Client
{
    private string _token;
    
    public int Id { get; }
    public IChannel Channel { get; }
    public DiffieHellman DiffieHellman { get; set; }
    public Cryptography? Decoder { get; set; }
    public Cryptography? HeaderDecoder { get; set; }

    public string Token { get; set; }

    public void GenerateTx()
    {
        var tSeedHex = Token[^4..];
        var tVals = new Dictionary<string,int>
        {
            { "0", 0 },
            { "1", 1 },
            { "2", 2 },
            { "3", 3 },
            { "4", 4 },
            { "5", 5 },
            { "6", 6 },
            { "7", 7 },
            { "8", 8 },
            { "9", 9 },
            { "a", 10 },
            { "b", 11 },
            { "c", 12 },
            { "d", 13 },
            { "e", 14 },
            { "f", 15 },
            { "A", 10 },
            { "B", 11 },
            { "C", 12 },
            { "D", 13 },
            { "E", 14 },
            { "F", 15 }
        };

        Ptx = 0;

        for (var i = 0; i <= 3; i++)
        {
            Ptx += (int)(Math.Pow(16, i) * tVals[char.ToString(tSeedHex[3 - i])]);
        }

        Prx = 0;

        tSeedHex = Token[..4];

        for (var i = 0; i <= 3; i++) {
            Prx += (int)(Math.Pow(16, i) * tVals[char.ToString(tSeedHex[3 - i])]);
        }
    }

    public int Ptx { get; set; }
    public int Prx { get; set; }

    public Client(int id, IChannel channel)
    {
        Id = id;
        Channel = channel;
    }

    public async Task QueueComposer(IMessageComposer composer)
    {
        await Channel.WriteAsync(composer);
    }

    public async Task SendComposer(IMessageComposer composer)
    {
        await Channel.WriteAndFlushAsync(composer);
    }
}