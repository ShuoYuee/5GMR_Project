using System;
using System.Security.Cryptography;
using System.Text;

/// <summary>
/// 小工具函數的集合
/// </summary>
public static class Util
{
    private const int HASH_SIZE = 24;

    public static string ByteArrayToHex(byte[] ba)
    {
        StringBuilder hex = new StringBuilder(ba.Length * 2);
        foreach (byte b in ba)
        {
            hex.AppendFormat("{0:x2}", b);
        }
        return hex.ToString();
    }

    public static byte[] HexToByteArray(string hex)
    {
        int NumberChars = hex.Length;
        byte[] bytes = new byte[NumberChars / 2];
        for (int i = 0; i < NumberChars; i += 2)
        {
            bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
        }
        return bytes;
    }

    public static string GenerateRandomString(int length)
    {
        string token = null;
        int byteLen = (int)Math.Ceiling((float)length * 3 / 4);
        using (RandomNumberGenerator rng = new RNGCryptoServiceProvider())
        {
            byte[] tokenData = new byte[byteLen];
            rng.GetBytes(tokenData);

            token = Convert.ToBase64String(tokenData);
        }
        return token;
    }

    public static string PBKDF2Hash(string salt, string password, int iter, int hashSize = HASH_SIZE)
    {
        byte[] byteSalt = Encoding.UTF8.GetBytes(salt);
        byte[] bytePwd = Encoding.UTF8.GetBytes(password);

        Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(bytePwd, byteSalt, iter);
        return Convert.ToBase64String(pbkdf2.GetBytes(hashSize));
    }

    public static string KeyedHashSHA512(string key, string msg)
    {
        string base64 = null;
        using (var sha512 = new HMACSHA512(Convert.FromBase64String(key)))
        {
            byte[] hashed = sha512.ComputeHash(Encoding.UTF8.GetBytes(msg));
            base64 = Convert.ToBase64String(hashed);
        }
        return base64;
    }

    public static string HashSHA512(string msg)
    {
        string base64 = null;
        using (var sha512 = SHA512.Create())
        {
            byte[] hashed = sha512.ComputeHash(Encoding.UTF8.GetBytes(msg));
            base64 = Convert.ToBase64String(hashed);
        }
        return base64;
    }

    public static string GenerateServerKey(string saltedPwd)
    {
        string text = "Server Key";
        return KeyedHashSHA512(saltedPwd, text);
    }

    public static string GenerateClientKey(string saltedPwd)
    {
        string text = "Client Key";
        return KeyedHashSHA512(saltedPwd, text);
    }

    public static string XORCipherBase64(string text, string key)
    {
        byte[] bytes = XORCipher(Convert.FromBase64String(text), Convert.FromBase64String(key));
        return Convert.ToBase64String(bytes);
    }

    public static byte[] XORCipher(byte[] text, byte[] key)
    {
        byte[] xor = new byte[text.Length];
        for (int i = 0; i < text.Length; i++)
        {
            xor[i] = (byte)(text[i] ^ key[i % key.Length]);
        }
        return xor;
    }
}
