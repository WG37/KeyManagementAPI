using KeyManagementAPI.DTOs.Asymmetric;

namespace KeyManagementAPI.Services.AsymmetricServices.CipherServices
{
    public interface IAsymCipherService
    {
        Task<RsaEncryptResponse> EncryptAsync(Guid keyId, string plainText);
        Task<RsaDecryptResponse> DecryptAsync(Guid keyId, string cipherText);
    }
}
