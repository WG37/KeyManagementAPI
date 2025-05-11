using AutoMapper;
using KeyManagementAPI.DTOs;
using KeyManagementAPI.Entities;

public class KeyMapProfile : Profile
{
    public KeyMapProfile()
    {
        CreateMap<CreateKeyDto, Key>()
          .ForMember(dest => dest.Id, opt => opt.Ignore())
          .ForMember(dest => dest.CreatedOn, opt => opt.Ignore())
          .ForMember(dest => dest.Status, opt => opt.MapFrom(_ => KeyStatus.Active));

        CreateMap<Key, KeyDto>();
    }
}