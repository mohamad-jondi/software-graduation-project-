// File: C:\Users\moham\Desktop\software\Backend\Domain\Services\DoctorService.cs
using AutoMapper;
using Data.enums;
using Data.Interfaces;
using Data.Models;
using Domain.DTOs;
using Domain.DTOs.Cases;
using Domain.DTOs.Patient;
using Domain.IServices;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

public class DoctorService : IDoctorService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public DoctorService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AppointmentDTO>> GetAcceptedAppointments(string doctorUsername)
    {
        var appointments = await _unitOfWork.GetRepositories<Appointment>()
            .Get()
            .Include(a => a.Doctor)
            .Include(a => a.Patient)
            .Where(a => a.Doctor.Username == doctorUsername && a.Status == AppointmentStatus.Accepted)
            .ToListAsync();

        return _mapper.Map<IEnumerable<AppointmentDTO>>(appointments);
    }

    public async Task<IEnumerable<AppointmentDTO>> GetPendingAppointments(string doctorUsername)
    {
        var appointments = await _unitOfWork.GetRepositories<Appointment>()
            .Get()
            .Include(a => a.Doctor)
            .Include(a => a.Patient)
            .Where(a => a.Doctor.Username == doctorUsername && a.Status == AppointmentStatus.Pending)
            .ToListAsync();

        return _mapper.Map<IEnumerable<AppointmentDTO>>(appointments);
    }

    public async Task<bool> ManageAppointment(int appointmentId, AppointmentDTO appointmentDTO)
    {
        var appointment = await _unitOfWork.GetRepositories<Appointment>().Get().FirstOrDefaultAsync(a => a.AppointmentId == appointmentId);
        if (appointment == null)
            return false;

        appointment.Status = Enum.Parse<AppointmentStatus>(appointmentDTO.Status);
        appointment.DoctorNotes = appointmentDTO.Notes;
        await _unitOfWork.GetRepositories<Appointment>().Update(appointment);

        return true;
    }

    public async Task<IEnumerable<AvaliabilityDTO>> GetDoctorAvailability(string doctorUsername)
    {
        var availabilities = await _unitOfWork.GetRepositories<Avaliability>()
            .Get()
            .Include(a => a.Doctor)
            .Where(a => a.Doctor.Username == doctorUsername)
            .ToListAsync();

        return _mapper.Map<IEnumerable<AvaliabilityDTO>>(availabilities);
    }

    public async Task<bool> DeleteDoctorAvailability(int availabilityId)
    {
        var availability = await _unitOfWork.GetRepositories<Avaliability>().Get().FirstOrDefaultAsync(a => a.AvalibailityID == availabilityId);
        if (availability == null)
            return false;

        await _unitOfWork.GetRepositories<Avaliability>().Delete(availability);
        return true;
    }

    public async Task<AvaliabilityDTO> AddDoctorAvailability(string doctorUsername, AvaliabilityDTO availabilityDTO)
    {
        var doctor = await _unitOfWork.GetRepositories<Doctor>().Get().FirstOrDefaultAsync(d => d.Username == doctorUsername);
        if (doctor == null)
            return null;

        var availability = _mapper.Map<Avaliability>(availabilityDTO);
        availability.Doctor = doctor;
        var addedAvailability = await _unitOfWork.GetRepositories<Avaliability>().Add(availability);

        return _mapper.Map<AvaliabilityDTO>(addedAvailability);
    }

    public async Task<AvaliabilityDTO> UpdateDoctorAvailability(int availabilityId, AvaliabilityDTO availabilityDTO)
    {
        var availability = await _unitOfWork.GetRepositories<Avaliability>().Get().FirstOrDefaultAsync(a => a.AvalibailityID == availabilityId);
        if (availability == null)
            return null;

        availability.DayOfWeek = availabilityDTO.DayOfWeek;
        availability.StartHour = availabilityDTO.StartHour;
        availability.EndHour = availabilityDTO.EndHour;
        await _unitOfWork.GetRepositories<Avaliability>().Update(availability);

        return _mapper.Map<AvaliabilityDTO>(availability);
    }

    //public async task<ienumerable<avaliabilitydto>> updaterangedoctoravailability(int availabilityid, ienumerable<avaliabilitydto> availabilitydtos)
    //{
    //    var availabilities = await _unitofwork.getrepositories<avaliability>().get().where(a => a.avalibailityid == availabilityid).tolistasync();
    //    if (availabilities == null)
    //        return null;

    //    foreach (var availabilitydto in availabilitydtos)
    //    {
    //        var availability = availabilities.firstordefault(a => a.avalibailityid == availabilityid);
    //        if (availability != null)
    //        {
    //            availability.dayofweek = availabilitydto.dayofweek;
    //            availability.starthour = availabilitydto.starthour;
    //            availability.endhour = availabilitydto.endhour;
    //        }
    //    }
    //    await _unitofwork.getrepositories<avaliability>().updaterange(availabilities);

    //    return _mapper.map<ienumerable<avaliabilitydto>>(availabilities);
    //}

    public async Task<AppointmentReminderDTO> GetUpcomingAppointmentsReminders(string doctorUsername)
    {
        // Implement logic to get upcoming appointment reminders
        return new AppointmentReminderDTO();
    }

    public async Task<bool> RequestAccessToPatientData(string doctorUsername, string patientUsername)
    {
        // Implement logic to request access to patient data
        return true;
    }

    public async Task<PatientFullDTO> ViewPatientHistory(string patientUsername)
    {
        var patient = await _unitOfWork.GetRepositories<Patient>()
            .Get()
            .Include(p => p.Allergies)
            .Include(p => p.Vaccinations)
            .Include(p => p.cases)
            .Include(p => p.EmergancyContact)
            .FirstOrDefaultAsync(p => p.Username == patientUsername);

        if (patient == null)
            return null;

        return _mapper.Map<PatientFullDTO>(patient);
    }

    public async Task<IEnumerable<DocumentDTO>> ViewPatientDocuments(string patientUsername)
    {
        var patient = await _unitOfWork.GetRepositories<Patient>()
            .Get()
            .Include(p => p.RelatedDocumtents)
            .FirstOrDefaultAsync(p => p.Username == patientUsername);

        if (patient == null)
            return null;

        return _mapper.Map<IEnumerable<DocumentDTO>>(patient.RelatedDocumtents);
    }

    public async Task<CredentialDTO> UpdateDoctorCredential(string doctorUsername, CredentialDTO doctorCredential)
    {
        var doctor = await _unitOfWork.GetRepositories<Doctor>().Get().Include(d => d.credential).FirstOrDefaultAsync(d => d.Username == doctorUsername);
        if (doctor == null)
            return null;

        var credential = doctor.credential.FirstOrDefault(c => c.CredentialID == doctorCredential.CredentialId);
        if (credential == null)
            return null;

        credential.CredentialValue = doctorCredential.CredentialValue;
        credential.CredentialType= doctorCredential.CredentialType;

        await _unitOfWork.GetRepositories<Credential>().Update(credential);

        return _mapper.Map<CredentialDTO>(credential);
    }

    public async Task<bool> DeleteDoctorCredential(string doctorUsername, CredentialDTO doctorCredential)
    {
        var doctor = await _unitOfWork.GetRepositories<Doctor>().Get().Include(d => d.credential).FirstOrDefaultAsync(d => d.Username == doctorUsername);
        if (doctor == null)
            return false;

        var credential = doctor.credential.FirstOrDefault(c => c.CredentialID == doctorCredential.CredentialId);
        if (credential == null)
            return false;

        await _unitOfWork.GetRepositories<Credential>().Delete(credential);

        return true;
    }

    public async Task<CredentialDTO> AddDoctorCredential(string doctorUsername, CredentialDTO doctorCredential)
    {
        var doctor = await _unitOfWork.GetRepositories<Doctor>().Get().FirstOrDefaultAsync(d => d.Username == doctorUsername);
        if (doctor == null)
            return null;

        var credential = _mapper.Map<Credential>(doctorCredential);
        doctor.credential.Add(credential);
        await _unitOfWork.GetRepositories<Credential>().Add(credential);

        return _mapper.Map<CredentialDTO>(credential);
    }

    public async Task<bool> UpdateDoctorTypeOfWork(string doctorUsername, string specializationDTO)
    {
        var doctor = await _unitOfWork.GetRepositories<Doctor>().Get().FirstOrDefaultAsync(d => d.Username == doctorUsername);
        if (doctor == null)
            return false;

        doctor.Specialization = specializationDTO;
        await _unitOfWork.GetRepositories<Doctor>().Update(doctor);

        return true;
    }

    // Cases Management
    public async Task<IEnumerable<CaseDTO>> ViewCases(string doctorUsername)
    {
        var cases = await _unitOfWork.GetRepositories<Case>()
            .Get()
            .Include(c => c.Doctor)
            .Where(c => c.Doctor.Username == doctorUsername)
            .ToListAsync();

        return _mapper.Map<IEnumerable<CaseDTO>>(cases);
    }

    public async Task<bool> UpdateCaseInformation(int caseId, CaseDTO caseDTO)
    {
        var caseInfo = await _unitOfWork.GetRepositories<Case>().Get().FirstOrDefaultAsync(c => c.CaseId == caseId);
        if (caseInfo == null)
            return false;

        caseInfo.Diagnosis = caseDTO.Diagnosis;
        caseInfo.TreatmentPlan = _mapper.Map<TreatmentPlan>(caseDTO.TreatmentPlan);
        await _unitOfWork.GetRepositories<Case>().Update(caseInfo);

        return true;
    }

    public Task<bool> UpdateDoctorTypeOfWork(string doctorUsername, DoctorWorkType specializationDTO)
    {
        throw new NotImplementedException();
    }

    public Task<bool> SnoozeDoctorAppointments(string doctorUsername, int minutes)
    {
        throw new NotImplementedException();
    }

    public Task<bool> MoveAppointment(int appointmentId)
    {
        throw new NotImplementedException();
    }
}
