using KeyManagementAPI.Data;
using KeyManagementAPI.DTOs.Asymmetric;
using KeyManagementAPI.Entities;
using System.Security.Cryptography;
using System.Text;

namespace KeyManagementAPI.Services.AsymmetricServices.CipherServices
{
    public class AsymCipherService : IAsymCipherService
    {
        private readonly AppDbContext _db;
        public AsymCipherService(AppDbContext db) => _db = db;

        public async Task<RsaEncryptResponse> EncryptAsync(Guid keyId, string plainText)
        {
            var key = await _db.Keys.FindAsync(keyId);
            if (key == null || key.Status != KeyStatus.Active) 
                throw new KeyNotFoundException($"Key: {keyId} not found or deactivated."); // controllers will catch and return 404 -- prevent DAL leak

            using var rsa = RSA.Create();
            rsa.ImportSubjectPublicKeyInfo(key.PubKey, out _);

            var pt = Encoding.UTF8.GetBytes(plainText);
            var ct = rsa.Encrypt(pt, RSAEncryptionPadding.OaepSHA384);

            return new RsaEncryptResponse
            {
                CipherText = Convert.ToBase64String(ct),
            };
        }

        public async Task<RsaDecryptResponse> DecryptAsync(Guid KeyId, string cipherText)
        {
            var key = await _db.Keys.FindAsync(KeyId);
            if (key == null || key.Status != KeyStatus.Active)
                throw new KeyNotFoundException($"Key: {KeyId} not found or deactivated."); // controllers will catch and return 404 -- prevent DAL leak

            using var rsa = RSA.Create();
            rsa.ImportPkcs8PrivateKey(key.PrivKey, out _);

            var ct = Convert.FromBase64String(cipherText);
            var pt = rsa.Decrypt(ct, RSAEncryptionPadding.OaepSHA384);

            return new RsaDecryptResponse
            {
                PlainText = Encoding.UTF8.GetString(pt)
            };
        }
    }
}
