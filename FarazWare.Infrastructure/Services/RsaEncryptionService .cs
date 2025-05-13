using System.Security.Cryptography;
using System.Text;

namespace FarazWare.Infrastructure.Services
{
    public class RsaEncryptionService : IEncryptionService
    {
        private readonly RSA _rsa;

        public RsaEncryptionService()
        {
            var publicKeyPath = Environment.GetEnvironmentVariable("PUBLIC_KEY_PATH");

            if (string.IsNullOrWhiteSpace(publicKeyPath))
                throw new InvalidOperationException("Environment variable 'PUBLIC_KEY_PATH' is not set.");

            if (!File.Exists(publicKeyPath))
                throw new FileNotFoundException($"Public key file not found at path: {publicKeyPath}");

            var pem = File.ReadAllText(publicKeyPath);

            _rsa = RSA.Create();
            _rsa.ImportFromPem(pem.ToCharArray()); 
        }

        public string Encrypt(string plainText)
        {
            var bytes = Encoding.UTF8.GetBytes(plainText);
            var encrypted = _rsa.Encrypt(bytes, RSAEncryptionPadding.Pkcs1);
            return Convert.ToBase64String(encrypted);
        }
    }
}
