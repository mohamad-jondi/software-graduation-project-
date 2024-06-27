using AutoMapper;
using Data.enums;
using Data.Interfaces;
using Data.Models;
using Domain.DTOs;
using Domain.DTOs.Appointment;
using Domain.DTOs.Cases;
using Domain.DTOs.Patient;
using Domain.IServices;
using Microsoft.EntityFrameworkCore;
using Domain.DTOs;

public class DoctorService : IDoctorService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public DoctorService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AppointmentForShowDTO>> GetAcceptedAppointments(string doctorUsername)
    {
        var appointments = await _unitOfWork.GetRepositories<Appointment>()
            .Get()
            .Include(a => a.Doctor)
            .Include(a => a.Patient)
            .Where(a => a.Doctor.Username == doctorUsername && a.Status == AppointmentStatus.Accepted)
            .ToListAsync();

        return _mapper.Map<IEnumerable<AppointmentForShowDTO>>(appointments);
    }

    public async Task<IEnumerable<AppointmentForShowDTO>> GetPendingAppointments(string doctorUsername)
    {
        var appointments = await _unitOfWork.GetRepositories<Appointment>()
            .Get()
            .Include(a => a.Doctor)
            .Include(a => a.Patient)
            .Where(a => a.Doctor.Username == doctorUsername && a.Status == AppointmentStatus.Pending)
            .ToListAsync();

        return _mapper.Map<IEnumerable<AppointmentForShowDTO>>(appointments);
    }

    public async Task<bool> ManageAppointment(AppointmentMangmentDTO appointmentDTO)
    {
        var appointment = await _unitOfWork.GetRepositories<Appointment>().Get().FirstOrDefaultAsync(a => a.AppointmentId == appointmentDTO.appointmentId);
        if (appointment == null)
            return false;

        appointment.Status = appointmentDTO.AppointmentStatus;
        var x = await _unitOfWork.GetRepositories<Appointment>().Update(appointment);

        return true;
    }

    public async Task<DoctorAvaliabilityWithAppointmentsDTO> GetDoctorAvailability(string doctorUsername)
    {
        var doctor = await _unitOfWork.GetRepositories<Doctor>()
            .Get()
            .Include(d => d.Avalible)
            .Include(d => d.Appointment)
            .ThenInclude(a => a.Patient) 
            .FirstOrDefaultAsync(d => d.Username == doctorUsername);

        if (doctor == null)
        {
            return null; // or handle the case where the doctor is not found
        }

        var avaliabilityDTOs = doctor.Avalible.Select(avaliability => new AvaliabilityDTO
        {
            DayOfWeek = avaliability.DayOfWeek,
            StartHour = avaliability.StartHour,
            EndHour = avaliability.EndHour
        }).ToList();

        var appointmentDTOs = doctor.Appointment.Select(appointment => new AppointmentDTO
        {
            Date = appointment.Date,
            Status = appointment.Status.ToString(),
            Description = appointment.Description,
            Notes = appointment.DoctorNotes,
            DoctorName = doctor.Name, 
            PatientName = appointment.Patient != null ? appointment.Patient.Name : null
        }).ToList();

        var result = new DoctorAvaliabilityWithAppointmentsDTO
        {
            Avaliabilities = avaliabilityDTOs,
            Appointments = appointmentDTOs
        };

        return result;
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
        throw new Exception();
        //var doctor = await _unitOfWork.GetRepositories<Doctor>().Get().Include(d => d.credential).FirstOrDefaultAsync(d => d.Username == doctorUsername);
        //if (doctor == null)
        //    return null;

        //var credential = doctor.credential.FirstOrDefault(c => c.CredentialID == doctorCredential.CredentialId);
        //if (credential == null)
        //    return null;

        //credential.CredentialValue = doctorCredential.CredentialValue;
        //credential.CredentialType= doctorCredential.CredentialType;

        //await _unitOfWork.GetRepositories<Credential>().Update(credential);

        //return _mapper.Map<CredentialDTO>(credential);
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
