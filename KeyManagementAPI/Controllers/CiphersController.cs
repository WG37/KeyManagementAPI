using KeyManagementAPI.Data;
using KeyManagementAPI.Entities;
using KeyManagementAPI.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace KeyManagementAPI.Controllers
{
    [Route("api/keys/{keyId}")]
    [ApiController]
    public class CiphersController : ControllerBase
    {
        private readonly AppDbContext _db;
        public CiphersController(AppDbContext db) => _db = db;

        [HttpPost("encrypt")]
        public async Task<IActionResult> Encrypt(Guid keyId, [FromBody] EncryptRequest request)
        {
            var key = await _db.Keys.FindAsync(keyId);
            if (key == null) return NotFound();
            if (key.Status != KeyStatus.Active) return BadRequest(new { message = "Key is not active" });

            using var aes = Aes.Create();
            aes.Key = key.Keybytes;
            aes.GenerateIV();

            var ptBytes = Encoding.UTF8.GetBytes(request.PlainText);
            var ctBytes = aes.CreateEncryptor().TransformFinalBlock(ptBytes, 0, ptBytes.Length);

            return Ok(new EncryptResponse
            {
                CipherText = Convert.ToBase64String(ctBytes),
                Iv = Convert.ToBase64String(aes.IV)
            });
        }

        [HttpPost("decrypt")]
        public async Task<IActionResult> Decrypt(Guid keyId, [FromBody] DecryptRequest request)
        {
            var key = await _db.Keys.FindAsync(keyId);
            if (key == null) return NotFound();
            if (key.Status != KeyStatus.Active) return BadRequest(new { message = "Key is not active" });

            using var aes = Aes.Create();
            aes.Key = key.Keybytes;
            aes.IV = Convert.FromBase64String(request.Iv);

            var ctBytes = Convert.FromBase64String(request.CipherText);
            var ptBytes = aes.CreateDecryptor().TransformFinalBlock(ctBytes, 0, ctBytes.Length);

            return Ok(new DecryptResponse { PlainText = Encoding.UTF8.GetString(ptBytes) 
            });
        }
    }
}
