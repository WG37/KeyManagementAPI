using KeyManagementAPI.DTOs;
using KeyManagementAPI.Entities;
using KeyManagementAPI.Data;
using System.Text;
using System.Security.Cryptography;

namespace KeyManagementAPI.Services
{
    public class CipherService : ICipherService
    {
        private readonly AppDbContext _db;
        public CipherService(AppDbContext db) => _db = db;


        public async Task<EncryptResponse> EncryptAsync(Guid keyId, string plainText)
        {
            var key = await _db.Keys.FindAsync(keyId);
            if (key == null || key.Status != KeyStatus.Active)
                throw new KeyNotFoundException();

            using var aes = Aes.Create();
            aes.Key = key.KeyBytes;
            aes.GenerateIV();

            var pt = Encoding.UTF8.GetBytes(plainText);
            var ct = aes.CreateEncryptor()
                        .TransformFinalBlock(pt, 0, pt.Length);

            return new EncryptResponse
            {
                CipherText = Convert.ToBase64String(ct),
                Iv = Convert.ToBase64String(aes.IV)
            };
        }

        public async Task<DecryptResponse> DecryptAsync(Guid keyId, string cipherText, string iv)
        {
            var key = await _db.Keys.FindAsync(keyId);
            if (key == null || key.Status != KeyStatus.Active)
                throw new KeyNotFoundException();

            using var aes = Aes.Create();
            aes.Key = key.KeyBytes;
            aes.IV = Convert.FromBase64String(iv);

            var ct = Convert.FromBase64String(cipherText);
            var pt = aes.CreateDecryptor()
                         .TransformFinalBlock(ct, 0, ct.Length);

            return new DecryptResponse
            {
                PlainText = Encoding.UTF8.GetString(pt)
            };
        }
    }
}

