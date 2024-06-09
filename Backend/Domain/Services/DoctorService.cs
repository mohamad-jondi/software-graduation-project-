using AutoMapper;
using Data.enums;
using Data.Interfaces;
using Data.Models;
using Domain.DTOs.Cases;
using Domain.DTOs.Patient;
using Domain.DTOs;
using Domain.IServices;
using Microsoft.EntityFrameworkCore;

public class DoctorService : IDoctorService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public DoctorService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AppointmentDTO>> GetAccpetedAppointments(string doctorUsername)
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
        var appointment = await _unitOfWork.GetRepositories<Appointment>()
            .Get()
            .FirstOrDefaultAsync(a => a.AppointmentId == appointmentId);

        if (appointment == null) return false;

        appointment.Status = Enum.Parse<AppointmentStatus>(appointmentDTO.Status);
        appointment.Description = appointmentDTO.Description;
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

    public async Task<bool> DeleteDoctorAvailability(int availabilityId, AvaliabilityDTO availabilityDTO)
    {
        var availability = await _unitOfWork.GetRepositories<Avaliability>()
            .Get()
            .FirstOrDefaultAsync(a => a.AvalibailityID == availabilityId);

        if (availability == null) return false;

        await _unitOfWork.GetRepositories<Avaliability>().Delete(availability);
        return true;
    }

    public async Task<AvaliabilityDTO> AddDoctorAvailability(string doctorUsername, AvaliabilityDTO availabilityDTO)
    {
        var doctor = await _unitOfWork.GetRepositories<Doctor>()
            .Get()
            .FirstOrDefaultAsync(d => d.Username == doctorUsername);

        if (doctor == null) return null;

        var availability = _mapper.Map<Avaliability>(availabilityDTO);
        availability.DoctorID = doctor.Id;

        await _unitOfWork.GetRepositories<Avaliability>().Add(availability);
        return _mapper.Map<AvaliabilityDTO>(availability);
    }

    public async Task<AvaliabilityDTO> UpdateDoctorAvailability(int availabilityId, AvaliabilityDTO availabilityDTO)
    {
        var availability = await _unitOfWork.GetRepositories<Avaliability>()
            .Get()
            .FirstOrDefaultAsync(a => a.AvalibailityID == availabilityId);

        if (availability == null) return null;

        availability.DayOfWeek = availabilityDTO.DayOfWeek;
        availability.StartHour = availabilityDTO.StartHour;
        availability.EndHour = availabilityDTO.EndHour;

        await _unitOfWork.GetRepositories<Avaliability>().Update(availability);
        return _mapper.Map<AvaliabilityDTO>(availability);
    }

    public async Task<IEnumerable<AvaliabilityDTO>> UpdateRangeDoctorAvailability(int availabilityId, IEnumerable<AvaliabilityDTO> availabilityDTOs)
    {
        var availabilities = await _unitOfWork.GetRepositories<Avaliability>()
            .Get()
            .Where(a => a.AvalibailityID == availabilityId)
            .ToListAsync();

        await _unitOfWork.GetRepositories<Avaliability>().UpdateRange(availabilities);
        return _mapper.Map<IEnumerable<AvaliabilityDTO>>(availabilities);
    }

    public async Task<AppointmentReminderDTO> GetUpcomingAppointmentsReminders(string doctorUsername)
    {
        // Implementation for getting upcoming appointment reminders
        throw new NotImplementedException();
    }

    public async Task<bool> RequestAccessToPatientData(string doctorUsername, string patientUsername)
    {
        // Implementation for requesting access to patient data
        throw new NotImplementedException();
    }

    public async Task<PatientFullDTO> ViewPatientHistory(string patientUsername)
    {
        var patient = await _unitOfWork.GetRepositories<Patient>()
            .Get()
            .Include(p => p.Allergies)
            .Include(p => p.Vaccinations)
            .Include(p => p.cases)
            .FirstOrDefaultAsync(p => p.Username == patientUsername);

        return _mapper.Map<PatientFullDTO>(patient);
    }

    public async Task<IEnumerable<DocumentDTO>> ViewPatientDocuments(string patientUsername)
    {
        throw new NotImplementedException();
    }

    public async Task<CredentialDTO> UpdateDoctorCredential(string doctorUsername, CredentialDTO doctorCredential)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteDoctorCredential(string doctorUsername, CredentialDTO doctorCredential)
    {
        throw new NotImplementedException();
    }

    public async Task<CredentialDTO> AddDoctorCredential(string doctorUsername, CredentialDTO doctorCredential)
    {
        var doctor = await _unitOfWork.GetRepositories<Doctor>()
            .Get()
            .Include(d => d.credential)
            .FirstOrDefaultAsync(d => d.Username == doctorUsername);

        if (doctor == null) return null;

        var credential = _mapper.Map<Credential>(doctorCredential);
        doctor.credential.Add(credential);

        await _unitOfWork.GetRepositories<Doctor>().Update(doctor);
        return _mapper.Map<CredentialDTO>(credential);
    }

    public async Task<bool> UpdateDoctorTypeOfWork(string doctorUsername, DoctorWorkType specializationDTO)
    {
        var doctor = await _unitOfWork.GetRepositories<Doctor>()
            .Get()
            .FirstOrDefaultAsync(d => d.Username == doctorUsername);

        if (doctor == null) return false;

        doctor.DoctorWorkType = specializationDTO;
        await _unitOfWork.GetRepositories<Doctor>().Update(doctor);
        return true;
    }

    public async Task<IEnumerable<CaseDTO>> ViewCases(string doctorUsername)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> ManageCase(int caseId, CaseDTO caseDTO)
    {
       throw new NotImplementedException();
    }

    public async Task<IEnumerable<TestDTO>> GetTests(string doctorUsername)
    {
        var tests = await _unitOfWork.GetRepositories<Test>()
            .Get()
            .Include(t => t.Patient)
            .Where(t => t.Patient.Username == doctorUsername)
            .ToListAsync();

        return _mapper.Map<IEnumerable<TestDTO>>(tests);
    }

    public async Task<TestDTO> AddTest(string doctorUsername, TestDTO testDTO)
    {
        var doctor = await _unitOfWork.GetRepositories<Doctor>()
            .Get()
            .FirstOrDefaultAsync(d => d.Username == doctorUsername);

        if (doctor == null) return null;

        var test = _mapper.Map<Test>(testDTO);
        test.PatientID = doctor.Id;

        await _unitOfWork.GetRepositories<Test>().Add(test);
        return _mapper.Map<TestDTO>(test);
    }

    public async Task<bool> UpdateTestStatus(int testId, TestStatus status)
    {
        var test = await _unitOfWork.GetRepositories<Test>()
            .Get()
            .FirstOrDefaultAsync(t => t.TestID == testId);

        if (test == null) return false;

        test.Status = status;
        await _unitOfWork.GetRepositories<Test>().Update(test);
        return true;
    }

    public async Task<bool> DeleteTest(int testId)
    {
        var test = await _unitOfWork.GetRepositories<Test>()
            .Get()
            .FirstOrDefaultAsync(t => t.TestID == testId);

        if (test == null) return false;

        await _unitOfWork.GetRepositories<Test>().Delete(test);
        return true;
    }

    public async Task<IEnumerable<SurgeryDTO>> ViewSurgeries(string doctorUsername)
    {
        var surgeries = await _unitOfWork.GetRepositories<Surgery>()
            .Get()
            .Include(s => s.Patient)
            .Where(s => s.Patient.Username == doctorUsername)
            .ToListAsync();

        return _mapper.Map<IEnumerable<SurgeryDTO>>(surgeries);
    }

    public async Task<bool> ScheduleSurgery(string doctorUsername, SurgeryDTO surgeryDTO)
    {
        var doctor = await _unitOfWork.GetRepositories<Doctor>()
            .Get()
            .FirstOrDefaultAsync(d => d.Username == doctorUsername);

        if (doctor == null) return false;

        var surgery = _mapper.Map<Surgery>(surgeryDTO);
        surgery.SurgeonID = doctor.Id;

        await _unitOfWork.GetRepositories<Surgery>().Add(surgery);
        return true;
    }

    public async Task<bool> UpdateSurgery(int surgeryId, SurgeryDTO surgeryDTO)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> CancelSurgery(int surgeryId)
    {
        var surgery = await _unitOfWork.GetRepositories<Surgery>()
            .Get()
            .FirstOrDefaultAsync(s => s.OperationID == surgeryId);

        if (surgery == null) return false;

        await _unitOfWork.GetRepositories<Surgery>().Delete(surgery);
        return true;
    }

    public async Task<IEnumerable<DocumentDTO>> ViewDocuments(string doctorUsername)
    {
        throw new NotImplementedException();
    }

    public async Task<DocumentDTO> UploadDocument(string doctorUsername, DocumentDTO documentDTO)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteDocument(int documentId)
    {
        throw new NotImplementedException();
    }

    public Task<AvaliabilityDTO> AddDoctorAvailability(int availabilityId, AvaliabilityDTO availabilityDTO)
    {
        throw new NotImplementedException();
    }
}
