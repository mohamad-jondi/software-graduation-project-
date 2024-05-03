using AutoMapper;
using Data.Models;
using Domain.DTOs;
using Domain.DTOs.Login;
using Domain.DTOs.Person;

namespace Domain.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, RegisterModelDTO>().ReverseMap();
            CreateMap<JWTTokens, JWTTokensDTO>().ReverseMap();
            CreateMap<Person, PersonDTO>().ReverseMap();
            CreateMap<User, Person>().ReverseMap();

        }
    }

}
