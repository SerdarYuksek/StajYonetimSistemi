using AutoMapper;
using Microsoft.AspNetCore.Identity;
using UserService.Api.Model;

namespace UserService.Api
{
    public class NameMapper : Profile
    {
        public NameMapper()
        {
            //AutoMapper İle ad soyad bilgilerini birleştirerek Modellere aktarıyoruz
            CreateMap<AppUser, IdentityUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.FirstName + " " + src.Surname));

            CreateMap<AppUser, PersonalListResponseModel>()
               .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.FirstName + " " + src.Surname));

            CreateMap<AppUser, StudentListResponseModel>()
              .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.FirstName + " " + src.Surname));

            CreateMap<AppUser, PersonalUpdateResponseModel>()
              .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.FirstName + " " + src.Surname));

            CreateMap<AppUser, StudentUpdateResponseModel>()
              .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.FirstName + " " + src.Surname));
        }
    }
}
