using System.Security.Cryptography;

namespace SapSmallTools;

public class MD5Program : Hasher
{
    public override string HashName => "md5";
    public override byte[] HashData(byte[] array) => MD5.HashData(array);
    public override byte[] HashData(Stream stream) => MD5.HashData(stream);
    public static void Main(string[] args) => new MD5Program().Run(args);
}