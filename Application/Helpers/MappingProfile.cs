using Application.Dtos;
using AutoMapper;
using Domain;

namespace Application.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AppUser, UserDto>();
            CreateMap<Animal, AnimalDto>().ForMember(o => o.AnimalType,
                i =>
                    i.MapFrom(x => x.AnimalType.Description));
        }
    }
}