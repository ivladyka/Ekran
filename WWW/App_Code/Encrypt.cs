using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

/// <summary>
/// Summary description for Encrypt
/// </summary>
public class Encrypt
{
    static readonly byte[] Key = { 189, 239, 20, 174, 17, 153, 249, 237, 73, 120, 53, 136, 115, 114, 110, 92, 80, 91, 56, 209, 192, 5, 147, 145, 250, 177, 4, 161, 93, 238, 68, 101 };
    static readonly byte[] IV = { 190, 187, 114, 176, 246, 66, 172, 60, 216, 224, 165, 213, 251, 193, 52, 201 };

    public static string EncryptRijndael(string original)
    {

        byte[] encrypted;

        byte[] toEncrypt;

        UTF8Encoding utf8Converter = new UTF8Encoding();

        toEncrypt = utf8Converter.GetBytes(original);

        RijndaelManaged myRijndael = new RijndaelManaged();

        MemoryStream ms = new MemoryStream();

        ICryptoTransform encryptor = myRijndael.CreateEncryptor(Key, IV);

        CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);

        cs.Write(toEncrypt, 0, toEncrypt.Length);

        cs.FlushFinalBlock();

        encrypted = ms.ToArray();

        string encryptedString = Convert.ToBase64String(encrypted);

        return encryptedString;

    }

    public static string DecryptRijndael(string encryptedString)
    {

        byte[] encrypted;

        byte[] fromEncrypted;

        UTF8Encoding utf8Converter = new UTF8Encoding();

        encrypted = Convert.FromBase64String(encryptedString);

        RijndaelManaged myRijndael = new RijndaelManaged();

        ICryptoTransform decryptor = myRijndael.CreateDecryptor(Key, IV);

        MemoryStream ms = new MemoryStream(encrypted);

        CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);

        fromEncrypted = new byte[encrypted.Length];

        cs.Read(fromEncrypted, 0, fromEncrypted.Length);

        string decryptedString = utf8Converter.GetString(fromEncrypted);
        int indexNull = decryptedString.IndexOf("\0");
        if (indexNull > 0)
        {
            decryptedString = decryptedString.Substring(0, indexNull);
        }
        return decryptedString;

    }
}
