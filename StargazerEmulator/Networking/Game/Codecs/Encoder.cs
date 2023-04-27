using System.Text;
using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;
using Stargazer.Communication.Interfaces;
using Stargazer.Networking.Game.Packets;

namespace Stargazer.Networking.Game.Codecs;

public class Encoder : MessageToMessageEncoder<object>
{
    protected override void Encode(IChannelHandlerContext context, object message, List<object> output)
    {
        switch (message)
        {
            case string:
                output.Add(Unpooled.CopiedBuffer(Encoding.Default.GetBytes(message.ToString() ?? string.Empty)));
                break;
            case IMessageComposer messageComposer:
                var response = new ServerPacket(messageComposer.Header);
                messageComposer.Compose(response);
                output.Add(response.GetBuffer());
                break;
        }
    }
}