using System.Security.Cryptography;

namespace SapSmallTools;

public class SHA256Program : Hasher
{
    public override string HashName => "sha256";
    public override byte[] HashData(byte[] array) => SHA256.HashData(array);
    public override byte[] HashData(Stream stream) => SHA256.HashData(stream);
    public static void Main(string[] args) => new SHA256Program().Run(args);
}