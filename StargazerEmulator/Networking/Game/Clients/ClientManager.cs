using System.Collections.Concurrent;
using DotNetty.Transport.Channels;

namespace Stargazer.Networking.Game.Clients;

public class ClientManager
{
    private static ClientManager? _instance;
    public static ClientManager Instance => _instance ??= new ClientManager();
    
    private readonly ConcurrentDictionary<IChannelId, Client> _clients;
    private int _clientId;

    public ClientManager()
    {
        _clients = new ConcurrentDictionary<IChannelId, Client>();
        _clientId = 0;
    }

    public bool TryAddClient(IChannel channel, out Client client)
    {
        client = new Client(Interlocked.Increment(ref _clientId), channel);
        return _clients.TryAdd(channel.Id, client);
    }

    public bool TryRemoveClient(IChannel channel)
    {
        return _clients.TryRemove(channel.Id, out _);
    }

    public bool TryGetClient(IChannel channel, out Client? client)
    {
        return _clients.TryGetValue(channel.Id, out client);
    }
}