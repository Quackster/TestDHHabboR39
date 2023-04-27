using System.Text;
using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;
using HabboEncoding;
using Stargazer.Networking.Game.Clients;
using Stargazer.Networking.Game.Packets;

namespace Stargazer.Networking.Game.Codecs;

public class Decoder : ByteToMessageDecoder
{
    protected override void Decode(IChannelHandlerContext context, IByteBuffer input, List<object> output)
    {
        if (!ClientManager.Instance.TryGetClient(context.Channel, out var client) || client == null)
            return;
        
        if (input.GetByte(0) == 60 && client.Decoder == null && client.HeaderDecoder == null)
        {
            context.WriteAndFlushAsync(Unpooled.CopiedBuffer(Encoding.UTF8.GetBytes("<?xml version=\"1.0\"?>\r\n" +
                "<!DOCTYPE cross-domain-policy SYSTEM \"http://www.macromedia.com/xml/dtds/cross-domain-policy.dtd\">\r\n" +
                "<cross-domain-policy>\r\n" +
                "<allow-access-from domain=\"*\" to-ports=\"*\" />\r\n" +
                "</cross-domain-policy>\0")));
            context.CloseAsync();
        }
        else
        {
            input.MarkReaderIndex();

            if (client.Decoder != null && client.HeaderDecoder != null)
            {
                String tHeader;
                String tBody;

                int pMsgSize;

                while (input.ReadableBytes > 6)
                {
                    byte[] tHeaderMsg = new byte[6];
                    input.ReadBytes(tHeaderMsg);

                    tHeader = Encoding.Default.GetString(tHeaderMsg);
                    tHeader = client.HeaderDecoder.kg4R6Jo5xjlqtFGs1klMrK4ZTzb3R(tHeader);

                    int tByte1 = ((int)tHeader[3]) & 63;
                    int tByte2 = ((int)tHeader[2]) & 63;
                    int tByte3 = ((int)tHeader[1]) & 63;
                    pMsgSize = (tByte2 * 64) | tByte1;
                    pMsgSize = (tByte3 * 64 * 64) | pMsgSize;

                    if (input.ReadableBytes < pMsgSize)
                    {
                        input.ResetReaderIndex();
                        return;
                    }

                    client.Ptx = IterateRandom(client.Ptx);

                    byte[] tBodyMsg = new byte[pMsgSize];
                    input.ReadBytes(tBodyMsg);

                    tBody = Encoding.Default.GetString(tBodyMsg);
                    tBody = client.Decoder.kg4R6Jo5xjlqtFGs1klMrK4ZTzb3R(tBody);
                    tBody = removePadding(tBody, client.Ptx % 5);
                    output.Add(new ClientPacket(Unpooled.CopiedBuffer(Encoding.GetEncoding(0).GetBytes(tBody))));
                }
            }
            else
            {
                while (input.ReadableBytes >= 5)
                {
                    var length = Base64Encoding.DecodeInt32(input.ReadBytes(3).Array);

                    if (length <= input.ReadableBytes)
                    {
                        output.Add(new ClientPacket(input.ReadBytes(length)));
                    }
                }
            }
        }
    }
    
    
    public static int IterateRandom(int tSeed)
    {
        return ((19979 * tSeed) + 5) % 65536;
    }
    
    public static String removePadding(String tBody, int i) {
        if (i >= tBody.Length)
            return tBody;

        return tBody.Substring(i);
    }

}