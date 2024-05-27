using AutoMapper;
using Data.enums;
using Data.Interfaces;
using Data.Models;
using Domain.DTOs.Person;
using Domain.IServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Domain.Services
{
    public class PersonService : IPersonService
    {
        private readonly IUnitOfWork _context;
        private readonly IMapper _mapper;

        public PersonService(IUnitOfWork unitOfWork , IMapper mapper) { 
            _context = unitOfWork;
            _mapper = mapper;
        }
        public async Task<PersonDTO> EditHightAndWeight(HeightAndWeightDTO infoUpdate)
        {
            var existingUser = await _context.GetRepositories<User>()
                                      .Get()
                                      .Where(_ => _.UserType == UserType.Person.ToString())
                                      .OfType<Person>()
                                      .FirstOrDefaultAsync();
            if (existingUser == null) return null;
            if (infoUpdate.LatestRecordedWeight.HasValue)existingUser.LatestRecordedWeight = infoUpdate.LatestRecordedWeight.Value;
            if (infoUpdate.LatestRecordedHeight.HasValue)existingUser.LatestRecordedHeight = infoUpdate.LatestRecordedHeight.Value;
            await _context.GetRepositories<Person>().Update(existingUser);
            var temp =  _mapper.Map<PersonDTO>(existingUser);
            temp.Age = (DateTime.Now - temp.DateOfBirth).TotalDays / 365.25;
            return temp;

        }

        public async Task<PersonDTO> EditOccupation(newOccupationDTO newOccupation)
        {
            var existingUser = await _context.GetRepositories<Person>().Get().Where(_ => _.Username == newOccupation.Username).FirstOrDefaultAsync();
            if (existingUser == null) return null;
            existingUser.Occupation = newOccupation.Occupation;
            await _context.GetRepositories<Person>().Update(existingUser);
            var temp = _mapper.Map<PersonDTO>(existingUser);
            temp.Age = (DateTime.Now - temp.DateOfBirth).TotalDays / 365.25;
            return temp;
        }

        public async Task<PersonDTO> editPersonType(string userName,personTypeDTO personTypeDTO)
        {
            var existingUser = await _context.GetRepositories<Person>().Get().Where(p=> p.Username == userName).FirstOrDefaultAsync();

            try
            {
              
                existingUser.PersonType = personTypeDTO.PersonType;
                await _context.GetRepositories<Person>().Delete(existingUser);
                existingUser.Id = 0;
                if (personTypeDTO.PersonType == PersonType.Patient) await _context.GetRepositories<Patient>().Add(_mapper.Map<Patient>(existingUser));
                if (personTypeDTO.PersonType == PersonType.Doctor) await _context.GetRepositories<Doctor>().Add(_mapper.Map<Doctor>(existingUser));
                //if (personTypeDTO.PersonType == PersonType.Nurse) await _context.GetRepositories<Nurse>().Add(_mapper.Map<Nurse>(existingUser));

            }catch (Exception ex)
            {
                return null;
            }
            return _mapper.Map<PersonDTO>(existingUser);


        }

        public async Task<PersonDTO> UpdateInfo(InfoUpdateDTO infoUpdate)
        {
            var existingUser = await _context.GetRepositories<Person>().Get().Where(_ => _.Username == infoUpdate.username).FirstOrDefaultAsync();
            if (existingUser == null) return null;
            if (infoUpdate.LatestRecordedWeight.HasValue) existingUser.LatestRecordedWeight = infoUpdate.LatestRecordedWeight.Value;
            if (infoUpdate.LatestRecordedHeight.HasValue) existingUser.LatestRecordedHeight = infoUpdate.LatestRecordedHeight.Value;
            if (!infoUpdate.MaritalStatus.IsNullOrEmpty()) existingUser.MaritalStatus= infoUpdate.MaritalStatus;
            if (!infoUpdate.Occupation.IsNullOrEmpty()) existingUser.Occupation= infoUpdate.Occupation;
            existingUser.Gender = infoUpdate.Gender;
            await _context.GetRepositories<Person>().Update(existingUser);
            var temp = _mapper.Map<PersonDTO>(existingUser);
            temp.Age = (DateTime.Now - temp.DateOfBirth).TotalDays / 365.25;
            return temp;
        }
    }
}
