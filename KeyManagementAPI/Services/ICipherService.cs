using KeyManagementAPI.DTOs;

namespace KeyManagementAPI.Services
{
    public interface ICipherService
    {
        Task<EncryptResponse> EncryptAsync(Guid keyId, string plainText);
        Task<DecryptResponse> DecryptAsync(Guid keyId, string cipherText, string iv);
    }
}
