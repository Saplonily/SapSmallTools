using System.Security.Cryptography;

namespace SapSmallTools;

public class Sha1Program : Hasher
{
    public override string HashName => "sha1";
    public override byte[] HashData(byte[] array) => SHA1.HashData(array);
    public override byte[] HashData(Stream stream) => SHA1.HashData(stream);
    public static void Main(string[] args) => new Sha1Program().Run(args);
}