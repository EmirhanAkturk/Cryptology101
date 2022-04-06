using System;
using System.Security.Cryptography;
using System.Text;

public class Hash
{
    public static string HashSHA256(string plaintext)
    {
        using (var sha256provider = new SHA256CryptoServiceProvider())
        {
            var hash = sha256provider.ComputeHash(Encoding.UTF8.GetBytes(plaintext));
            return BitConverter.ToString(hash).Replace("-", "");
        }
    }
    
    public static string HashMD5(string plaintext)
    {
        using (var mdp5provider = new MD5CryptoServiceProvider())
        {
            var hash = mdp5provider.ComputeHash(Encoding.UTF8.GetBytes(plaintext));
            return BitConverter.ToString(hash).Replace("-", "");
        }
    }
}
