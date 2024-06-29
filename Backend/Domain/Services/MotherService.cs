using AutoMapper;
using Data.enums;
using Data.Interfaces;
using Data.Models;
using Domain.DTOs.Appointment;
using Domain.DTOs.Child;
using Domain.DTOs.Vaccination;
using Microsoft.EntityFrameworkCore;
using System.Linq;

public class MotherService : IMotherService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public MotherService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<ChildDTO> AddChildAsync(string motherUsername, ChildForCreationDTO childDTO)
    {
        var child = _mapper.Map<Child>(childDTO);
        var mother = await _unitOfWork.GetRepositories<Mother>().Get().Include(c=> c.childrens).FirstOrDefaultAsync(m => m.Username == motherUsername);
        if (mother == null) return null;

        child.Parent = mother;
        var x=  await _unitOfWork.GetRepositories<Child>().Add(child);
        mother.childrens.Add(x);
        await _unitOfWork.GetRepositories<Mother>().Update(mother);
        return _mapper.Map<ChildDTO>(x)  ;
    }

    public async Task<bool> CancelChildAppointmentAsync(int childId, AppointmentCanceltionDTO appointmentDTO)
    {
        var appointment = await _unitOfWork.GetRepositories<Appointment>().Get().Where(v => v.AppointmentId == appointmentDTO.appointmentId).FirstOrDefaultAsync();
        appointment.CanceledReson = appointmentDTO.CanceledReson;
        appointment.CanceledBy = appointmentDTO.CanceledBy;
        await _unitOfWork.GetRepositories<Appointment>().Update(appointment);
        return true;
    }

    public async Task<IEnumerable<ChildDTO>> GetChildrenAsync(string motherUsername)
    {
        var mother = await _unitOfWork.GetRepositories<Mother>().Get().Include(c=> c.childrens).ThenInclude(c=> c.Vaccination).Include(c=> c.childrens).ThenInclude(c=> c.Appointments).FirstOrDefaultAsync(m => m.Username == motherUsername);
        if (mother == null) return null;
        return _mapper.Map<IEnumerable<ChildDTO>>(mother.childrens);
    }

    public async Task<ChildDTO> UpdateChildInfo(int ChildID, ChildForCreationDTO childDTO)
    {
        var child = await _unitOfWork.GetRepositories<Child>().Get().FirstOrDefaultAsync(c => c.Id == ChildID);
        if (child == null) return null;

        child.DateOfBirth = childDTO.DateOfBirth== null ? child.DateOfBirth : (DateTime) childDTO.DateOfBirth;
        child.LatestRecordedWeight = childDTO.LatestRecordedWeight ?? child.LatestRecordedWeight;
        child.LatestRecordedHeight = childDTO.LatestRecordedHeight ?? child.LatestRecordedHeight;
        child.Gender = childDTO.Gender == null?child.Gender : (Gender)childDTO.Gender;
        child.Name = string.IsNullOrEmpty(childDTO.Name) ? child.Name : childDTO.Name;

        await _unitOfWork.GetRepositories<Child>().Update(child);
        return _mapper.Map<ChildDTO>(child);
    }

    public async Task<ChildDTO> GetChildByID(int ChildId)
    {
        var child = await _unitOfWork.GetRepositories<Child>().Get().FirstOrDefaultAsync(c => c.Id == ChildId);
        return _mapper.Map<ChildDTO>(child);
    }

    public async Task<VaccinationDTO> AddChildVacination(int ChildID, VaccinationDTO vacine)
    {
        var child = await _unitOfWork.GetRepositories<Child>().Get().Include(c=> c.Vaccination).FirstOrDefaultAsync(c => c.Id == ChildID);
        if (child == null)  return null;

        child.Vaccination.Add(_mapper.Map<Vaccination>(vacine));
        await _unitOfWork.GetRepositories<Child>().Update(child);
        return _mapper.Map<VaccinationDTO>(vacine);
    }

    public async Task<bool> DeleteChildVacination(int vaccinationID)
    {
        var x= await _unitOfWork.GetRepositories<Vaccination>().Get().Where(v => v.VaccinationID == vaccinationID).FirstOrDefaultAsync();
        if (x== null )return false;
        await _unitOfWork.GetRepositories<Vaccination>().Delete(x);
        return true;
        
    }

    public async Task<bool> UpdateChildVaccinationAsync(VaccinationForUpdatingDTO vaccinationDTO)
    {
        var vaccination = await _unitOfWork.GetRepositories<Vaccination>()
            .Get()
            .FirstOrDefaultAsync(v => v.VaccinationID == vaccinationDTO.VaccinationID);

        if (vaccination == null)
            return false;

        if (!string.IsNullOrEmpty(vaccinationDTO.Name))
            vaccination.Name = vaccinationDTO.Name;

        if (vaccinationDTO.AdministeredDate.HasValue)
            vaccination.AdministeredDate = vaccinationDTO.AdministeredDate.Value;

        if (!string.IsNullOrEmpty(vaccinationDTO.Description))
            vaccination.Description = vaccinationDTO.Description;

        if (vaccinationDTO.VaccineStatus.HasValue)
            vaccination.VaccineStatus = vaccinationDTO.VaccineStatus.Value;

        if (vaccinationDTO.ShotsLeft.HasValue)
            vaccination.ShotsLeft = vaccinationDTO.ShotsLeft.Value;

        await _unitOfWork.GetRepositories<Vaccination>().Update(vaccination);
        return true;
    }

    public async Task<AppointmentDTO> BookAppointment(int ChildID, string DoctorUserName, AppointmentDTO appointment)
    {
        var app = _mapper.Map<Appointment>(appointment);
        app.ChildID = ChildID;
        var doctor =await _unitOfWork.GetRepositories<Doctor>().Get().Where(c => c.Username == DoctorUserName).FirstOrDefaultAsync();
        app.Doctor = doctor;
        return _mapper.Map<AppointmentDTO> (await _unitOfWork.GetRepositories<Appointment>().Add(app));

    }


}
