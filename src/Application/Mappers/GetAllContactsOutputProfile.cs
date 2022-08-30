using AutoMapper;
using NET.Mysql.Sample.Application.UseCases.GetAllContatcs;
using System.Collections.Generic;
using Entity = NET.Mysql.Sample.Domain.Entities;

namespace NET.Mysql.Sample.Application.Mappers
{
    public class GetAllContactsOutputProfile : Profile
    {
        public GetAllContactsOutputProfile()
        {
            CreateMap<IEnumerable<Entity.Contact>, GetAllContactsOutput>()
                .ForMember(dest => dest.Contacts, opt => opt.MapFrom(src => src))
            ;

            CreateMap<Entity.Contact, ContactOutput>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.ContactEmail))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ContactName))
            ;
        }
    }
}
