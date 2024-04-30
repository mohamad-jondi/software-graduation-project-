using AutoMapper;
using Data.Models;
using Domain.DTOs;
using Domain.DTOs.Login;

namespace Domain.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, RegisterModelDTO>().ReverseMap();
            CreateMap<JWTTokens, JWTTokensDTO>().ReverseMap();
        }
    }

}
