using Domain.DTOs;
using Domain.DTOs.Vaccination;

public class MotherService : IMotherService
{
    private readonly IChildRepository _childRepository;
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IVaccinationRepository _vaccinationRepository;
    private readonly IDoctorRepository _doctorRepository;

    public MotherService(
        IChildRepository childRepository,
        IAppointmentRepository appointmentRepository,
        IVaccinationRepository vaccinationRepository,
        IDoctorRepository doctorRepository)
    {
        _childRepository = childRepository;
        _appointmentRepository = appointmentRepository;
        _vaccinationRepository = vaccinationRepository;
        _doctorRepository = doctorRepository;
    }

    public async Task<ChildDTO> AddChildAsync(string motherUsername, ChildDTO childDTO)
    {
        return await _childRepository.AddChildAsync(motherUsername, childDTO);
    }

    public async Task<bool> ManageChildVaccinationAsync(int childId, VaccinationDTO vaccinationDTO)
    {
        return await _vaccinationRepository.ManageVaccinationAsync(childId, vaccinationDTO);
    }

    public async Task<bool> ManageChildAppointmentAsync(int childId, AppointmentDTO appointmentDTO)
    {
        return await _appointmentRepository.ManageAppointmentAsync(childId, appointmentDTO);
    }

    public async Task<IEnumerable<ChildDTO>> GetChildrenAsync(string motherUsername)
    {
        return await _childRepository.GetChildrenAsync(motherUsername);
    }

    public async Task<bool> SnoozeDoctorAppointmentsAsync(string doctorUsername, int minutes)
    {
        var appointments = await _appointmentRepository.GetAppointmentsByDoctorAsync(doctorUsername);
        if (appointments == null) return false;

        foreach (var appointment in appointments)
        {
            appointment.StartTime = appointment.StartTime.AddMinutes(minutes);
            appointment.EndTime = appointment.EndTime.AddMinutes(minutes);
        }

        return await _appointmentRepository.UpdateAppointmentsAsync(appointments);
    }

    public async Task<bool> MoveAppointmentAsync(int appointmentId, int minutes)
    {
        var appointment = await _appointmentRepository.GetAppointmentByIdAsync(appointmentId);
        if (appointment == null) return false;

        appointment.StartTime = appointment.StartTime.AddMinutes(minutes);
        appointment.EndTime = appointment.EndTime.AddMinutes(minutes);

        return await _appointmentRepository.UpdateAppointmentAsync(appointment);
    }
}
