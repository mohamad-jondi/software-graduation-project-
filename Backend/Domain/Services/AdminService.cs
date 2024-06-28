using AutoMapper;
using Data.Interfaces;
using Data.Models;
using Domain.DTOs.Doctor;
using Microsoft.EntityFrameworkCore;

public class AdminService : IAdminService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AdminService(IUnitOfWork unitOfWork,IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<DoctorWithCredinttialsDTO>> GetUnverifiedDoctorsAsync()
    {
        return _mapper.Map<IEnumerable<DoctorWithCredinttialsDTO>>( await _unitOfWork.GetRepositories<Doctor>()
            .Get()
            .Include(d=> d.credential)
            .Where(d => !d.isVerifedDoctor)
            .ToListAsync());
    }

    public async Task VerifyDoctorAsync(string username)
    {
        var doctor = await _unitOfWork.GetRepositories<Doctor>()
            .Get()
            .Where(d => d.Username == username)
            .FirstOrDefaultAsync();

        if (doctor == null)
        {
            throw new Exception("Doctor not found");
        }

        doctor.isVerifedDoctor = true;

        await _unitOfWork.GetRepositories<Doctor>().Update(doctor);
    }
}
