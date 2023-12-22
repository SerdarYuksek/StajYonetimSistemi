using AutoMapper;
using Microsoft.AspNetCore.Identity;
using UserService.Api.Model;

namespace UserService.Api
{
    public class DtoMapper : Profile
    {
        public DtoMapper()
        {
            //AutoMapper İle ad soyad bilgilerini birleştirerek Dtoya aktarıyoruz
            CreateMap<Student, IdentityUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.FirstName + " " + src.Surname));

            CreateMap<Personal, IdentityUser>()
               .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.FirstName + " " + src.Surname));

        }
    }
}
