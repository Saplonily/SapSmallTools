using System.Security.Cryptography;

namespace SapSmallTools;

public class Sha512Program : Hasher
{
    public override string HashName => "sha512";
    public override byte[] HashData(byte[] array) => SHA512.HashData(array);
    public override byte[] HashData(Stream stream) => SHA512.HashData(stream);
    public static void Main(string[] args) => new Sha512Program().Run(args);
}