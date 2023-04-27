using DotNetty.Buffers;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using Microsoft.Extensions.Logging;
using Stargazer.Logging;

namespace Stargazer.Networking;

public abstract class AbstractNetworkServer
{
    protected readonly ILogger<AbstractNetworkServer> Logger = LoggerManager.GetLogger<AbstractNetworkServer>();

    private ServerBootstrap _bootstrap;
    private IEventLoopGroup _bossGroup;
    private IEventLoopGroup _workerGroup;

    public virtual async Task Start()
    {
        _bossGroup = new MultithreadEventLoopGroup(1);
        _workerGroup = new MultithreadEventLoopGroup(10);

        _bootstrap = new ServerBootstrap();
        _bootstrap.Group(_bossGroup, _workerGroup)
            .Channel<TcpServerSocketChannel>().ChildHandler(new ActionChannelInitializer<IChannel>(ChannelHandlerInitializer))
            .ChildOption(ChannelOption.TcpNodelay, true).ChildOption(ChannelOption.SoKeepalive, true)
            .ChildOption(ChannelOption.SoReuseaddr, true).ChildOption(ChannelOption.SoRcvbuf, 1024)
            .ChildOption(ChannelOption.RcvbufAllocator, new FixedRecvByteBufAllocator(1024))
            .ChildOption(ChannelOption.Allocator, UnpooledByteBufferAllocator.Default);
        await _bootstrap.BindAsync(Port);
    }

    public virtual async Task Stop()
    {
        try
        {
            await _workerGroup.ShutdownGracefullyAsync();
            await _bossGroup.ShutdownGracefullyAsync();
        }
        catch (ObjectDisposedException e)
        {
        }
        catch (NullReferenceException e)
        {
        }
    }

    protected virtual int Port => 0;

    protected virtual Action<IChannel> ChannelHandlerInitializer => channel => { };
}