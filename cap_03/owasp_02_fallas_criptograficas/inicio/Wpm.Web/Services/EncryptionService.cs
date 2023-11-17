using System.Security.Cryptography;
using System.Text;

namespace Wpm.Web.Services;

public class EncryptionService
{
    private readonly byte[] key = Encoding.ASCII.GetBytes("WisdomPM");

    public byte[] EncryptData(string plainText)
    {
        using var des = new DESCryptoServiceProvider();
        des.Key = key;
        des.IV = key;
        var encryptor = des.CreateEncryptor();
        var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
        
        return encryptor.TransformFinalBlock(plainTextBytes, 0, plainTextBytes.Length);
    }

    public string DecryptData(byte[] encryptedData)
    {
        using var des = new DESCryptoServiceProvider();
        des.Key = key;
        des.IV = key;
        var decryptor = des.CreateDecryptor();
        var decryptedBytes = decryptor.TransformFinalBlock(encryptedData, 0, encryptedData.Length);
        
        return Encoding.UTF8.GetString(decryptedBytes);
    }
}