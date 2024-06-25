using Data.Models;
using Domain.DTOs;
using Domain.DTOs.Cases;
using System.Reflection.Metadata;

namespace Domain.IServices
{
    public interface ICaseService
    {
        Task<IEnumerable<CaseDTO>> GetCasesAsync(string doctorUsername);
        Task<CaseDTO> GetCaseByIdAsync(int caseId);
        Task<CaseDTO> AddCaseAsync(CaseDTO caseDTO);
        Task<bool> UpdateCaseAsync(int caseId, CaseDTO caseDTO);
        Task<bool> DeleteCaseAsync(int caseId);
        Task<IEnumerable<DocumentDTO>> ViewDocuments(int CaseID);

        Task<DocumentDTO> UploadDocument(int CaseID, DocumentDTO documentDTO);

        Task<bool> DeleteDocument(int documentId);
    }
}
