﻿using System;
using System.CommandLine;
using System.Runtime.InteropServices.Marshalling;
using System.Security.Cryptography;
using System.Text;

namespace SapSmallTools;

public class Program
{
    public static void Main(string[] args)
    {
        var rootCmd = new RootCommand("sha512 calculator of SapSmallTools.");
        var fileOptions = new Option<FileInfo?>("-f", "The file to be caculated sha512.");
        var textOptions = new Option<string>("-t", "The text to be caculated sha512.");

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

    public static void CalcFile(FileInfo fi)
    {
        if (fi.Exists is false)
        {
            Console.Error.WriteLine("File not found.");
            return;
        }
        using var fs = fi.Open(FileMode.Open, FileAccess.Read);
        byte[] hashbytes = SHA512.HashData(fs);
        Console.WriteLine($"File sha512 result: {GetHashString(hashbytes)}");
    }

    public static void CalcText(string str)
    {
        Console.WriteLine("sha512 of the text:");
        Console.WriteLine($"    UTF8:    {StringHash(str, Encoding.UTF8)}");
        Console.WriteLine($"    UTF16:   {StringHash(str, Encoding.Unicode)}");
        Console.WriteLine($"    UTF32:   {StringHash(str, Encoding.UTF32)}");
        Console.WriteLine($"    ASCII:   {StringHash(str, Encoding.ASCII)}");
        Console.WriteLine($"    Latin1:  {StringHash(str, Encoding.Latin1)}");
        Console.WriteLine($"    BEUTF16: {StringHash(str, Encoding.BigEndianUnicode)}");
    }

    public static string StringHash(string str, Encoding encoding)
    {
        byte[] bytes = encoding.GetBytes(str);
        byte[] hashbytes = SHA512.HashData(bytes);
        return GetHashString(hashbytes);
    }

    public static string GetHashString(byte[] hash)
        => string.Join(null, hash.Select(b => b.ToString("x").PadLeft(2, '0')));
}
