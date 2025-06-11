using KeyManagementAPI.Services.Asymmetric.CipherServices;
using KeyManagementAPI.DTOs.Asymmetric;
using Microsoft.AspNetCore.Mvc;

namespace KeyManagementAPI.Controllers.Asymmetric
{
    [ApiController]
    [Route("api/rsa/{keyId}")]
    public class RsaController : ControllerBase
    {
        private readonly IAsymCipherService _rsa;
        public RsaController(IAsymCipherService rsa) => _rsa = rsa;

        [HttpPost("encrypt")]
        public async Task<IActionResult> Encrypt(Guid keyId, [FromBody] RsaEncryptRequest req)
        {
            var response = await _rsa.EncryptAsync(keyId, req.PlainText);
            return Ok(response);
        }

        [HttpPost("decrypt")]
        public async Task<IActionResult> Decrypt(Guid keyId, [FromBody] RsaDecryptRequest req)
        {
            var response = await _rsa.DecryptAsync(keyId, req.CipherText);
            return Ok(response);
        }

    }
}
