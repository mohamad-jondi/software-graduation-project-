using Data.enums;
using Data.Models;
using Domain.DTOs;

namespace Domain.IServices
{
    public interface ITestService
    {
        Task<IEnumerable<TestDTO>> GetTests(int CaseID);
        Task<TestDTO> AddTest(int CaseID, TestDTO testDTO);
        Task<bool> UpdateTestStatus(int testId, TestStatus status);
        Task<bool> DeleteTest(int testId);

    }
}
