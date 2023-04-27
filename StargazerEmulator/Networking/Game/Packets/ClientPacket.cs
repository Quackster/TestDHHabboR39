using System.Text;
using DotNetty.Buffers;
using HabboEncoding;

namespace Stargazer.Networking.Game.Packets;

public class ClientPacket
{
    private readonly IByteBuffer _buffer;

    public ClientPacket(IByteBuffer buffer)
    {
        _buffer = buffer;
    }

    private byte[] ReadBytes(int amount)
    {
        var dest = new byte[amount];
        _buffer.ReadBytes(dest);
        return dest;
    }

    private byte[] GetBytes(int? amount)
    {
        var dest = new byte[amount ?? _buffer.ReadableBytes];
        _buffer.GetBytes(0, dest);
        return dest;
    }

    public int ReadVL64()
    {
        var value = VL64Encoding.DecodeInt32(GetBytes(VL64Encoding.MaxIntegerByteAmount), out var totalBytes);
        ReadBytes(totalBytes);
        return value;
    }

    public int ReadBase64()
    {
        return Base64Encoding.DecodeInt32(ReadBytes(2));
    }

    public string ReadString()
    {
        return Encoding.Default.GetString(ReadBytes(ReadBase64()));
    }

    public override string ToString()
    {
        return Encoding.GetEncoding(0).GetString(_buffer.Array);
    }
}