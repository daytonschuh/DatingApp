using System.Linq;
using AutoMapper;
using DTOs;
using ModelLayer;

namespace BusinessLogicLayer
{
    public class AutoMappeProfiles : Profile
    {
        public AutoMappeProfiles()
        {
            CreateMap<AppUser, MemberDto>()
                .ForMember(dest => dest.PhotoUrl, opt => opt
                .MapFrom(src => src.Photos.FirstOrDefault(x=>x.IsMain).Url));
            CreateMap<Photo, PhotoDto>();
            CreateMap<AppUser, UserDto>();
        }
    }
}