using KeyManagementAPI.DTOs;
using Microsoft.EntityFrameworkCore;
using KeyManagementAPI.Data;
using KeyManagementAPI.Entities;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Http.HttpResults;

namespace KeyManagementAPI.Services.AsymmetricServices.KeyServices
{
    public class AsymKeyService : IAsymKeyService
    {
        private readonly AppDbContext _db;
        public AsymKeyService(AppDbContext db) => _db = db;

        public async Task<KeyDto> AsymCreateAsync(CreateKeyDto dto)
        {
            using var rsa = RSA.Create(dto.KeySize);
            var pub = rsa.ExportSubjectPublicKeyInfo();
            var priv = rsa.ExportPkcs8PrivateKey();


            var key = new Key
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                KeySize = dto.KeySize,
                Algorithm = "RSA",
                PubKey = pub,
                PrivKey = priv,
                Status = KeyStatus.Active,
                CreatedOn = DateTime.UtcNow
            };

            _db.Keys.Add(key);
            await _db.SaveChangesAsync();

            return new KeyDto
            {
                Id = key.Id,
                Name = key.Name,
                KeySize = key.KeySize,
                Algorithm = key.Algorithm,
                PubKey = key.PubKey,
                PrivKey = null!,     // NEVER EXPOSE
                Status = key.Status,
                CreatedOn = key.CreatedOn
            };
        }

        public async Task<List<KeyDto>> AsymGetAllAsync()
        {
            return await _db.Keys
                .Select(k => new KeyDto
                {
                    Id = k.Id,
                    Name = k.Name,
                    KeySize = k.KeySize,
                    Algorithm = k.Algorithm,
                    PubKey = k.PubKey,
                    PrivKey = k.PrivKey,
                    Status = k.Status,
                    CreatedOn = k.CreatedOn
                }).ToListAsync();
        }

        public async Task<KeyDto> AsymGetAsync(Guid id)
        {
            var key = await _db.Keys.FindAsync(id);
            if (key == null || key.Status != KeyStatus.Active)
                throw new KeyNotFoundException();

            return new KeyDto
            {
                Id = key.Id,
                Name = key.Name,
                KeySize = key.KeySize,
                Algorithm = key.Algorithm,
                PubKey = key.PubKey,
                PrivKey = key.PrivKey,
                Status = key.Status,
                CreatedOn = key.CreatedOn
            };
        }

        public async Task AsymDeleteAsync(Guid id)
        {
            var key = await _db.Keys.FindAsync(id);
            if (key == null) 
                throw new KeyNotFoundException($"No key exists with the id: {id} ");

            _db.Keys.Remove(key);
            await _db.SaveChangesAsync();
        }
    }
}
