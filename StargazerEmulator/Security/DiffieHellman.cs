using System.Security.Cryptography;
using System.Text;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;

namespace Stargazer.Security;

public class DiffieHellman
{
    private readonly BigInteger _prime;
    private readonly BigInteger _privateKey;
    
    public BigInteger PublicKey { get; private set; }

    public DiffieHellman(BigInteger prime, BigInteger generator)
    {
        _prime = prime;
        _privateKey = new BigInteger(GenerateRandomHexBytes(30));

        PublicKey = generator.ModPow(_privateKey, _prime);
    }

    public BigInteger GenerateSharedKey(BigInteger clientPublicKey)
    {
        return clientPublicKey.ModPow(_privateKey, _prime);
    }
    
    public static byte[] GenerateRandomHexBytes(int length)
    {
        if (length < 1)
        {
            throw new ArgumentException("Length must be positive.", nameof(length));
        }

        return RandomNumberGenerator.GetBytes(length / 2);
    }
    
    public static string generateRandomNumString(int len) {
        var result = new StringBuilder();

        var numbers = new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };

        for (var i = 0; i < len; i++) {
            result.Append(numbers[RandomNumberGenerator.GetInt32(numbers.Length)]);
        }
        return result.ToString();
    }
}