using KeyManagementAPI.DTOs;

namespace KeyManagementAPI.Services.AsymmetricServices.KeyServices
{
    public interface IAsymKeyService
    {
        Task<KeyDto> AsymCreateAsync(CreateKeyDto dto);
        Task<List<KeyDto>> AsymGetAllAsync();
        Task<KeyDto> AsymGetAsync(Guid id);
        Task AsymDeleteAsync(Guid id);
    }
}
