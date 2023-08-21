using System.CommandLine;
using System.Text;

namespace SapSmallTools;

public abstract class Hasher
{
    public abstract string HashName { get; }
    public abstract byte[] HashData(byte[] array);
    public abstract byte[] HashData(Stream stream);

    public void Run(string[] args)
    {
        var rootCmd = new RootCommand($"{HashName} calculator of SapSmallTools.");
        var fileOptions = new Option<FileInfo?>("-f", $"The file to be caculated {HashName}.");
        var textOptions = new Option<string>("-t", $"The text to be caculated {HashName}.");

        rootCmd.Add(fileOptions);
        rootCmd.Add(textOptions);
        rootCmd.SetHandler((f, t) =>
        {
            if (f is not null)
                CalcFile(f);
            else if (t is not null)
                CalcText(t);
            else
                rootCmd.Invoke("-h");
        }, fileOptions, textOptions);

        rootCmd.Invoke(args);
    }

    private void CalcFile(FileInfo fi)
    {
        if (fi.Exists is false)
        {
            Console.Error.WriteLine("File not found.");
            return;
        }
        using var fs = fi.Open(FileMode.Open, FileAccess.Read);
        byte[] hashbytes = HashData(fs);
        Console.WriteLine($"File {HashName} result: {GetHashString(hashbytes)}");
    }

    private void CalcText(string str)
    {
        Console.WriteLine($"{HashName} of the text:");
        Console.WriteLine($"    UTF8:    {StringHash(str, Encoding.UTF8)}");
        Console.WriteLine($"    UTF16:   {StringHash(str, Encoding.Unicode)}");
        Console.WriteLine($"    UTF32:   {StringHash(str, Encoding.UTF32)}");
        Console.WriteLine($"    ASCII:   {StringHash(str, Encoding.ASCII)}");
        Console.WriteLine($"    Latin1:  {StringHash(str, Encoding.Latin1)}");
        Console.WriteLine($"    BEUTF16: {StringHash(str, Encoding.BigEndianUnicode)}");
    }

    private string StringHash(string str, Encoding encoding)
    {
        byte[] bytes = encoding.GetBytes(str);
        byte[] hashbytes = HashData(bytes);
        return GetHashString(hashbytes);
    }

    private static string GetHashString(byte[] hash)
        => string.Join(null, hash.Select(b => b.ToString("x").PadLeft(2, '0')));
}