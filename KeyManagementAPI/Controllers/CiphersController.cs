using KeyManagementAPI.Data;
using KeyManagementAPI.DTOs;
using KeyManagementAPI.Entities;
using KeyManagementAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace KeyManagementAPI.Controllers
{
    [ApiController]
    [Route("api/keys/{keyId}")]
    public class CiphersController : ControllerBase
    {
        private readonly ICipherService _cipher;
        public CiphersController(ICipherService cipher) => _cipher = cipher;


        [HttpPost("encrypt")]
        public async Task<IActionResult> Encrypt(Guid keyId, [FromBody] EncryptRequest request)
        {
            var response = await _cipher.EncryptAsync(keyId, request.PlainText);
            return Ok(response);
        }

        [HttpPost("decrypt")]
        public async Task<IActionResult> Decrypt(Guid keyId, [FromBody] DecryptRequest request)
        {
            var response = await _cipher.DecryptAsync(keyId, request.CipherText, request.Iv);
            return Ok(response);
        }
    }
}
