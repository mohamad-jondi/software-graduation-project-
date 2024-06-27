using Domain.DTOs;
using Domain.DTOs.Cases;
using Domain.DTOs.Symptoms;


namespace Domain.IServices
{
    public interface ICaseService
    {
        Task<IEnumerable<CaseDTO>> GetCasesAsync(string doctorUsername);
        Task<CaseDTO> GetCaseByIdAsync(int caseId);
        Task<CaseDTO> AddCaseAsync(CaseForCreationDTO caseDTO);
        Task<bool> UpdateCaseAsync(int caseId, CaseDTO caseDTO);
        Task<bool> DeleteCaseAsync(int caseId);
        Task<IEnumerable<DocumentDTO>> ViewDocuments(int caseId);
        Task<DocumentDTO> UploadDocument(int caseId, DocumentDTO document);
        Task<bool> DeleteDocument(int documentId);
        // Task<bool> AssignNurseAsync(int caseId, NurseDTO nurse);
        Task<bool> AddDrugToCaseAsync(int caseId, DrugDTO drug);
        Task<bool> AddNoteToCaseAsync(int caseId, string note);
        Task<bool> AddSymptomToCaseAsync(int caseId, SymptomsDTO symptom);
        Task<bool> AddDiagnosisToCaseAsync(int caseId, string diagnosis);
    }
}