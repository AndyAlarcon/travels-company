using System.Security.Cryptography;
using System.Text;
using Clientes.Domain.Interfaces;

namespace Clientes.Infrastructure.Services;

public class CryptoService : ICryptoService
{
    private readonly byte[] _key;
    private readonly byte[] _iv;

    public CryptoService()
    {
        // ⚠️ En real, esto debe ir en configuración segura, no hardcoded
        _key = Encoding.UTF8.GetBytes("SuperClaveAES1234"); // 16 bytes
        _iv = Encoding.UTF8.GetBytes("VectorInicial1234");  // 16 bytes
    }

    public string Encrypt(string plainText)
    {
        using var aes = Aes.Create();
        aes.Key = _key;
        aes.IV = _iv;

        var encryptor = aes.CreateEncryptor();
        var plainBytes = Encoding.UTF8.GetBytes(plainText);
        var cipherBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);

        return Convert.ToBase64String(cipherBytes);
    }

    public string Decrypt(string cipherText)
    {
        using var aes = Aes.Create();
        aes.Key = _key;
        aes.IV = _iv;

        var decryptor = aes.CreateDecryptor();
        var cipherBytes = Convert.FromBase64String(cipherText);
        var plainBytes = decryptor.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);

        return Encoding.UTF8.GetString(plainBytes);
    }
}
