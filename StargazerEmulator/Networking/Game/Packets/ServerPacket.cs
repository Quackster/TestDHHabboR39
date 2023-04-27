using System.Text;
using DotNetty.Buffers;
using HabboEncoding;

namespace Stargazer.Networking.Game.Packets;

public class ServerPacket
{
    private readonly IByteBuffer _buffer;

    public ServerPacket(int header)
    {
        _buffer = Unpooled.Buffer();
        _buffer.WriteBytes(Base64Encoding.EncodeInt32(header));
    }

    public void WriteInteger(int value)
    {
        _buffer.WriteBytes(VL64Encoding.EncodeInt32(value));
    }

    public void WriteString(string value)
    {
        _buffer.WriteBytes(Encoding.GetEncoding(0).GetBytes(value));
        _buffer.WriteByte(2);
    }

    public void WriteBoolean(bool value)
    {
        _buffer.WriteByte(value ? VL64Encoding.Positive : VL64Encoding.Negative);
    }

    public IByteBuffer GetBuffer()
    {
        return Unpooled.CopiedBuffer(_buffer).WriteByte(1);
    }
}