using AutoMapper;
using NET.Mysql.Sample.Application.UseCases.CreateContact;
using NET.Mysql.Sample.Domain.Entities;

namespace NET.Mysql.Sample.Application.Mappers
{
    public class CreateContactInputProfile : Profile
    {
        public CreateContactInputProfile()
        {
            CreateMap<CreateContactInput, Contact>()
                .ForMember(dest => dest.ContactName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.ContactEmail, opt => opt.MapFrom(src => src.Email))
            ;
        }
    }
}
