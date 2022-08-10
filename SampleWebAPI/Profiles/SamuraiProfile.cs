using AutoMapper;
using SampleWebAPI.Domain;
using SampleWebAPI.DTO;

namespace SampleWebAPI.Profiles
{
    public class SamuraiProfile : Profile
    {
        public SamuraiProfile()
        {
            //CreateMap<SamuraiWithQuotesDTO, Samurai>();
            CreateMap<Samurai, SamuraiWithQuotesDTO>();

            CreateMap<Samurai, SamuraiReadDTO>();
            CreateMap<SamuraiReadDTO, Samurai>();
            CreateMap<SamuraiCreateDTO, Samurai>();

            CreateMap<QuoteDTO, Quote>();
            CreateMap<Quote,QuoteDTO>();

            CreateMap<Sword, SwordReadDTO>();
            CreateMap<SwordReadDTO, Sword>();
            CreateMap<SwordCreateDTO, Sword>();

            CreateMap<Element, ElementReadDTO>();
            CreateMap<ElementReadDTO, Element>();
            CreateMap<ElementCreateDTO, Element>();

            CreateMap<SampleWebAPI.Domain.Type, TypeReadDTO>();
            CreateMap<TypeReadDTO, SampleWebAPI.Domain.Type>();
            CreateMap<TypeCreateDTO, SampleWebAPI.Domain.Type>();

            CreateMap<UserCreateDTO, User>();

            CreateMap<ElementSwordDTO, ElementSword>();

            CreateMap<Samurai, SamuraiWithSwordElementTypeDTO>();
            CreateMap<SamuraiWithSwordElementTypeDTO, Samurai>();

            CreateMap<Sword, SwordTypeDTO>();



        }
    }
}
