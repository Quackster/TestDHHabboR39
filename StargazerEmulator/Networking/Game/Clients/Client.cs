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
        string tSeedHex = Token.Substring(Token.Length - 4);
        Dictionary<string, int> tVals = new Dictionary<string, int>();
        tVals.Add("0", 0);
        tVals.Add("1", 1);
        tVals.Add("2", 2);
        tVals.Add("3", 3);
        tVals.Add("4", 4);
        tVals.Add("5", 5);
        tVals.Add("6", 6);
        tVals.Add("7", 7);
        tVals.Add("8", 8);
        tVals.Add("9", 9);
        tVals.Add("a", 10);
        tVals.Add("b", 11);
        tVals.Add("c", 12);
        tVals.Add("d", 13);
        tVals.Add("e", 14);
        tVals.Add("f", 15);
        tVals.Add("A", 10);
        tVals.Add("B", 11);
        tVals.Add("C", 12);
        tVals.Add("D", 13);
        tVals.Add("E", 14);
        tVals.Add("F", 15);

        this.Ptx = 0;

        for (int i = 0; i <= 3; i++)
        {
            this.Ptx += (int)(Math.Pow(16, i) * tVals[Char.ToString(tSeedHex[3 - i])]);
        }

        this.Prx = 0;

        tSeedHex = Token.Substring(0, 4);

        for (int i = 0; i <= 3; i++)
        {
            this.Prx += (int)(Math.Pow(16, i) * tVals[Char.ToString(tSeedHex[3 - i])]);
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