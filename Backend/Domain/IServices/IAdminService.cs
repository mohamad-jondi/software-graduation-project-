using Data.Models;
using Domain.DTOs.Doctor;

public interface IAdminService
{
    Task<IEnumerable<DoctorWithCredinttialsDTO>> GetUnverifiedDoctorsAsync();
    Task VerifyDoctorAsync(string username);
}
