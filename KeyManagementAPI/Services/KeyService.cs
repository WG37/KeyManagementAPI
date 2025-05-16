using KeyManagementAPI.Data;
using KeyManagementAPI.DTOs;
using KeyManagementAPI.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace KeyManagementAPI.Services
{
    public class KeyService : IKeyService
    {
        private readonly AppDbContext _db;
        public KeyService(AppDbContext db) => _db = db;

        public async Task<KeyDto> CreateAsync(CreateKeyDto keyDto)
        {
            var key = new Key
            {
                Id = Guid.NewGuid(),
                Name = keyDto.Name,
                KeySize = keyDto.KeySize,
                Status = KeyStatus.Active,
                CreatedOn = DateTime.UtcNow
            };

            var bytes = new byte[key.KeySize / 8];
            RandomNumberGenerator.Create().GetBytes(bytes);
            key.KeyBytes = bytes;

            _db.Keys.Add(key);
            await _db.SaveChangesAsync();

            return new KeyDto
            {
                Id = key.Id,
                Name = key.Name,
                KeySize = key.KeySize,
                KeyBytes = key.KeyBytes,
                Status = key.Status,
                CreatedOn = key.CreatedOn
            };
        }

        public async Task<List<KeyDto>> GetAllAsync()
        {
            return await _db.Keys
                .Select(k => new KeyDto
                {
                    Id = k.Id,
                    Name = k.Name,
                    KeySize = k.KeySize,
                    KeyBytes = k.KeyBytes,
                    Status = k.Status,
                    CreatedOn = k.CreatedOn
                })
                .ToListAsync();
        }

        public async Task<KeyDto> GetAsync(Guid id)
        {
            var key = await _db.Keys.FindAsync(id);
            if (key == null || key.Status != KeyStatus.Active)
                throw new KeyNotFoundException();

            return new KeyDto
            {
                Id = key.Id,
                Name = key.Name,
                KeySize = key.KeySize,
                KeyBytes = key.KeyBytes,
                Status = key.Status,
                CreatedOn = key.CreatedOn
            };
        }

        public async Task DeleteAsync(Guid id)
        {
            var key = await _db.Keys.FindAsync(id);
            if (key == null) throw new KeyNotFoundException();
            
            _db.Keys.Remove(key);
            await _db.SaveChangesAsync();
        }
    }
}
