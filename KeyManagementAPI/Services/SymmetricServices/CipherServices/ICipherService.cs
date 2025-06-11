using KeyManagementAPI.DTOs.Symmetric;

namespace KeyManagementAPI.Services.SymmetricServices.CipherServices
{
    public interface ICipherService
    {
        Task<EncryptResponse> EncryptAsync(Guid keyId, string plainText);
        Task<DecryptResponse> DecryptAsync(Guid keyId, string cipherText, string iv);
    }
}
