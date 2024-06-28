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
        Task<CaseDTO> UpdateCaseAsync(int caseId, CaseForUpdating caseDTO);
        Task<bool> DeleteCaseAsync(int caseId);
        Task<IEnumerable<DocumentDTO>> ViewDocuments(int caseId);
        Task<DocumentDTO> UploadDocument(int caseId, DocumentDTO document);
        Task<bool> DeleteDocument(int documentId);
        // Task<bool> AssignNurseAsync(int caseId, NurseDTO nurse);
        Task<bool> AddDrugToCaseAsync(int caseId, DrugDTO drug);
        Task<RelatedDocumentDTO> AddDocumetAsync(int caseId, ImageUploadRequestDTO image);
        Task<bool> AddSymptomToCaseAsync(int caseId, SymptomsDTO symptom);
        Task<bool> AddDiagnosisToCaseAsync(int caseId, string diagnosis);
    }
}