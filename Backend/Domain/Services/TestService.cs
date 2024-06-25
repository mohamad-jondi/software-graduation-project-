using AutoMapper;
using Data.enums;
using Data.Interfaces;
using Data.Models;
using Domain.DTOs;
using Domain.IServices;
using Microsoft.EntityFrameworkCore;

namespace Domain.Services
{
    public class TestService : ITestService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TestService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<TestDTO>> GetTests(string PatientUsername)
        {
            var tests = await _unitOfWork.GetRepositories<Test>()
                .Get()
                .Include(t => t.Patient)
                .Where(t => t.Patient.Username == PatientUsername)
                .ToListAsync();

            return _mapper.Map<IEnumerable<TestDTO>>(tests);
        }

        public async Task<TestDTO> AddTest(int CaseID, TestDTO testDTO)
        {
            var doctor = await _unitOfWork.GetRepositories<Case>().Get().FirstOrDefaultAsync(d => d.CaseId== CaseID);
            if (doctor == null)
                return null;

            var test = _mapper.Map<Test>(testDTO);
            
            var addedTest = await _unitOfWork.GetRepositories<Test>().Add(test);


            return _mapper.Map<TestDTO>(addedTest);
        }

        public async Task<bool> UpdateTestStatus(int testId, TestStatus status)
        {
            var test = await _unitOfWork.GetRepositories<Test>().Get().FirstOrDefaultAsync(t => t.TestID == testId);
            if (test == null)
                return false;

            test.Status = status;
            await _unitOfWork.GetRepositories<Test>().Update(test);

            return true;
        }

        public async Task<bool> DeleteTest(int testId)
        {
            var test = await _unitOfWork.GetRepositories<Test>().Get().FirstOrDefaultAsync(t => t.TestID == testId);
            if (test == null)
                return false;

            await _unitOfWork.GetRepositories<Test>().Delete(test);

            return true;
        }

    }
}
