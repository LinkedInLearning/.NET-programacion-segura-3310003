using Microsoft.AspNetCore.DataProtection;
using System.Text;

namespace Wpm.Web.Services;

public class EncryptionService
{
    private readonly IDataProtector protector;
    public EncryptionService(IDataProtectionProvider dataProtectionProvider)
    {
        protector = dataProtectionProvider.CreateProtector("MicrochipNumberEncryption");
    }

    public byte[] EncryptData(string plainText)
    {
        var bytes = Encoding.UTF8.GetBytes(plainText);
        return protector.Protect(bytes);
    }

    public string DecryptData(byte[] encryptedData)
    {
        var bytes = protector.Unprotect(encryptedData);
        return Encoding.UTF8.GetString(bytes);
    }
}