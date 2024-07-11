using AutoMapper.Bugfix.Service.Dto;
using AutoMapper.Bugfix.Service.Entities;
using AutoMapper.Bugfix.Web.Mapped;

namespace AutoMapper.Bugfix.Web.MappingProfiles;

public class AutoMappingWeb : Profile
{
    public AutoMappingWeb()
    {
        CreateMap<AutoMappingClassALarge, AutoMappingClassALargeMapped>()
           .ForMember(
              dest => dest.ClassAMappedList,
              opt => opt.MapFrom(src => src.ClassAList));

        CreateMap<AutoMappingClassA, AutoMappingClassAMapped>()
          .ForMember(
             dest => dest.Data,
              opt => opt.MapFrom(src => src.Data));

        CreateMap<AutoMappingSharedClass, AutoMappingSharedClassMapped>()
             .ForMember(
                dest => dest.Name,
                opt => opt.MapFrom(src => src.Name));

        CreateMap<AutoMappingClassBLarge, AutoMappingClassBLargeMapped>();

        CreateMap<AutoMappingClassB, AutoMappingClassBMapped>();

    }
}
