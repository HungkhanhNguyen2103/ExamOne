using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Text;

namespace ExamOne
{
    public class AesHelper
    {
        private readonly string _key;
        private readonly string _iv;
        public readonly string ExamDataKey = "iN2aGc54tZvoYw9psL2UEf4hs";
        public readonly string AnswerClientDataKey = "N2ZGYah4oUctswLsi9v2p54Ef";

        public AesHelper(IOptions<Encryption> options)
        {
            _key = options.Value.AESKey ?? throw new ArgumentNullException(nameof(options.Value.AESKey));
            _iv = options.Value.AESIV ?? throw new ArgumentNullException(nameof(options.Value.AESIV));
        }

        public string Encrypt(string plainText)
        {
            using var aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(_key);
            aes.IV = Encoding.UTF8.GetBytes(_iv);

            using var encryptor = aes.CreateEncryptor();
            var bytes = Encoding.UTF8.GetBytes(plainText);
            var encrypted = encryptor.TransformFinalBlock(bytes, 0, bytes.Length);
            return Convert.ToBase64String(encrypted);
        }

        public string Decrypt(string cipherText)
        {
            using var aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(_key);
            aes.IV = Encoding.UTF8.GetBytes(_iv);

            using var decryptor = aes.CreateDecryptor();
            var bytes = Convert.FromBase64String(cipherText);
            var decrypted = decryptor.TransformFinalBlock(bytes, 0, bytes.Length);
            return Encoding.UTF8.GetString(decrypted);
        }
    }

    public class Encryption
    {
        public string? AESKey { get; set; }
        public string? AESIV { get; set; }
    }
}
