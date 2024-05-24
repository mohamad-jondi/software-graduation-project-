using AutoMapper;
using Data.Models;
using Domain.DTOs;
using Domain.DTOs.Allergy;
using Domain.DTOs.Doctor;
using Domain.DTOs.Login;
using Domain.DTOs.Person;
using Domain.DTOs.Vaccination;

namespace Domain.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, RegisterModelDTO>().ReverseMap();
            CreateMap<JWTTokens, JWTTokensDTO>().ReverseMap();
            CreateMap<Person, PersonDTO>().ReverseMap();
            CreateMap<Doctor, DoctorForOutputDTO>().ReverseMap();
            CreateMap<Doctor, DoctorForInputDTO>().ReverseMap();
            CreateMap<Appointment, AppointmentDTO>().ReverseMap();
            CreateMap<Allergy, AllergyForOutputDTO>().ReverseMap();
            CreateMap<Allergy, AllergyDTO>().ReverseMap();
            CreateMap<Vaccination, VaccinationForOutputDTO>().ReverseMap();
            CreateMap<Allergy, VaccinationDTO>().ReverseMap();


        }
    }

}
