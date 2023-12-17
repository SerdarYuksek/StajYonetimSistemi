using AutoMapper;
using UserService.Api.Dtos;
using UserService.Api.Model;

namespace UserService.Api
{
    public class DtoMapper : Profile
    {
        public DtoMapper()
        {
            //AutoMapper İle ad soyad bilgilerini birleştirerek Dtoya aktarıyoruz
            CreateMap<Student, StudentDto>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FirstName + " " + src.Surname));

            CreateMap<Personal, PersonalDto>()
               .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FirstName + " " + src.Surname));

        }
    }
}
