using AutoMapper;
using Data.Models;
using Data.Models.Data.Models;
using Domain.DTOs;
using Domain.DTOs.Allergy;
using Domain.DTOs.Appointment;
using Domain.DTOs.Cases;
using Domain.DTOs.Chats;
using Domain.DTOs.Child;
using Domain.DTOs.Doctor;
using Domain.DTOs.LifestyleFactors;
using Domain.DTOs.Login;
using Domain.DTOs.Patient;
using Domain.DTOs.Person;
using Domain.DTOs.Symptoms;
using Domain.DTOs.Vaccination;
using System.Reflection.Metadata;

namespace Domain.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, RegisterModelDTO>().ReverseMap();
            CreateMap<JWTTokens, JWTTokensDTO>().ReverseMap();
            CreateMap<User, Person>().ReverseMap();
            CreateMap<User, Doctor>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<Appointment, AppointmentDTO>().ReverseMap();
            CreateMap<Appointment, AppointmentForShowDTO>().ReverseMap();
            CreateMap<Vaccination, VaccinationForUpdatingDTO>().ReverseMap();
            CreateMap<Doctor, DoctorWithCredinttialsDTO>().ReverseMap();
            CreateMap<Credential, CredentialForShowDTO>().ReverseMap();
            CreateMap<Documents,RelatedDocumentDTO>().ReverseMap();

            CreateMap<Avaliability, AvaliabilityDTO>().ReverseMap();
            CreateMap<Doctor, DoctorForBrowsingDTO>().ReverseMap();
            CreateMap<Person, Patient>().ReverseMap();
            CreateMap<Person, Doctor>().ReverseMap();
            CreateMap<Person, Mother>().ReverseMap();
            CreateMap<Patient, PersonDTO>().ReverseMap();
            CreateMap<Person, PersonDTO>().ReverseMap();
            CreateMap<Doctor, PersonDTO>().ReverseMap();
            CreateMap<Patient, PatientFullDTO>().ReverseMap();
            CreateMap<Case, CaseDTO>().ReverseMap();
            CreateMap<Case, CaseForCreationDTO>().ReverseMap();
            
            CreateMap<Nurse, PersonDTO>().ReverseMap();
            CreateMap<Doctor, DoctorForOutputDTO>().ReverseMap();
            CreateMap<Doctor, DoctorForInputDTO>().ReverseMap();
            CreateMap<Appointment, AppointmentDTO>().ReverseMap();
            CreateMap<Allergy, AllergyForOutputDTO>().ReverseMap();
            CreateMap<Allergy, AllergyDTO>().ReverseMap();
            CreateMap<Vaccination, VaccinationForOutputDTO>().ReverseMap();
            CreateMap<Allergy, VaccinationDTO>().ReverseMap();
            CreateMap<ChatMessage, ChatMessageDTO>().ReverseMap();
            CreateMap<ChatMessage, CreateChatMessageDTO>().ReverseMap();
            // Added mappings for cases, tests, surgeries, documents, etc.
            CreateMap<Test, TestDTO>().ReverseMap();
            CreateMap<Document, DocumentDTO>().ReverseMap();
            CreateMap<MedicalSecondOpinion, MedicalSecondOpinionDTO>().ReverseMap();
            CreateMap<TreatmentPlan, TreatmentPlanDTO>().ReverseMap();
            CreateMap<Drug, DrugDTO>().ReverseMap();
            CreateMap<Callender, CallenderDTO>().ReverseMap();
            CreateMap<Credential, CredentialDTO>().ReverseMap();
            CreateMap<EmergencyContactInfo, EmergencyContactInfoDTO>().ReverseMap();
            CreateMap<LifestyleFactors, LifestyleFactorsDTO>().ReverseMap();
            CreateMap<LifestyleFactors, LifestyleFactorsForUpdatingDTO>().ReverseMap();
            CreateMap<Child, ChildDTO>().ReverseMap();
            CreateMap<Child, ChildForCreationDTO>().ReverseMap();
            CreateMap<Child, CompleteChildDTO>().ReverseMap();
            CreateMap<Symptoms, SymptomsDTO>().ReverseMap();




        }
    }

}
