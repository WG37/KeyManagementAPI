using KeyManagementAPI.DTOs;

namespace KeyManagementAPI.Services.SymmetricServices.KeyServices
{
    public interface IKeyService
    {
        Task<KeyDto> CreateAsync(CreateKeyDto keyDto);
        Task<List<KeyDto>> GetAllAsync();
        Task<KeyDto> GetAsync(Guid id);
        Task DeleteAsync(Guid id);
    }
}
