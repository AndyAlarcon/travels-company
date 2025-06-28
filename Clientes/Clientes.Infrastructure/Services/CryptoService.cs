using System.Security.Cryptography;
using System.Text;
using Clientes.Domain.Interfaces;
using Clientes.Infrastructure.Utils;
using Microsoft.Extensions.Options;

namespace Clientes.Infrastructure.Services;

public class CryptoService : ICryptoService
{
    private readonly byte[] _key;
    private readonly byte[] _iv;

    public CryptoService(IOptions<EncryptionOptions> options)
    {
        var settings = options.Value;

        _key = Convert.FromBase64String(settings.Key);
        _iv = Convert.FromBase64String(settings.IV);
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
