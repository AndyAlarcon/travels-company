namespace Clientes.Infrastructure.Utils
{
    public class EncryptionOptions
    {
        public string Key { get; set; } = string.Empty;
        public string IV { get; set; } = string.Empty;
    }
}