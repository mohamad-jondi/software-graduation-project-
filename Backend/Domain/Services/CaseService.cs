using AutoMapper;
using Data.Interfaces;
using Data.Models;
using Domain.DTOs;
using Domain.DTOs.Cases;
using Domain.DTOs.Symptoms;
using Domain.IServices;
using Microsoft.EntityFrameworkCore;


public class CaseService : ICaseService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CaseService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CaseDTO>> GetCasesAsync(string doctorUsername)
    {
        var cases = await _unitOfWork.GetRepositories<Case>()
            .Get()
            .Include(c => c.Doctor)
            .Include(c=> c.Patient)
            .Where(c => c.Doctor.Username == doctorUsername || c.Patient.Username== doctorUsername)
            .ToListAsync();

        return _mapper.Map<IEnumerable<CaseDTO>>(cases);
    }

    public async Task<CaseDTO> GetCaseByIdAsync(int caseId)
    {
        var caseEntity = await _unitOfWork.GetRepositories<Case>()
            .Get()
            .Include(c => c.Doctor)
            .FirstOrDefaultAsync(c => c.CaseId == caseId);

        if (caseEntity == null)
            return null;

        return _mapper.Map<CaseDTO>(caseEntity);
    }

    public async Task<CaseDTO> AddCaseAsync(CaseForCreationDTO caseDTO)
    {
        var caseEntity = _mapper.Map<Case>(caseDTO);

        var patient = await _unitOfWork.GetRepositories<Patient>().Get().Where(r => r.Username == caseDTO.PatientUsername).FirstOrDefaultAsync();
        var Doctor = await _unitOfWork.GetRepositories<Doctor>().Get().Where(r => r.Username == caseDTO.DoctorUserName).FirstOrDefaultAsync();
        var Nurse = await _unitOfWork.GetRepositories<Nurse>().Get().Where(r => r.Username == caseDTO.NurseUserName).FirstOrDefaultAsync();

        caseEntity.Doctor = Doctor;
        caseEntity.Patient = patient;
        caseEntity.Nurse = Nurse;
        var addedCase = await _unitOfWork.GetRepositories<Case>().Add(caseEntity);

        return _mapper.Map<CaseDTO>(addedCase);
    }

    public async Task<bool> UpdateCaseAsync(int caseId, CaseDTO caseDTO)
    {
        var caseEntity = await _unitOfWork.GetRepositories<Case>().Get().FirstOrDefaultAsync(c => c.CaseId == caseId);
        if (caseEntity == null)
            return false;

        caseEntity.Diagnosis = caseDTO.Diagnosis;
        caseEntity.TreatmentPlan = _mapper.Map<TreatmentPlan>(caseDTO.TreatmentPlan);
        await _unitOfWork.GetRepositories<Case>().Update(caseEntity);

        return true;
    }

    public async Task<bool> DeleteCaseAsync(int caseId)
    {
        var caseEntity = await _unitOfWork.GetRepositories<Case>().Get().FirstOrDefaultAsync(c => c.CaseId == caseId);
        if (caseEntity == null)
            return false;

        await _unitOfWork.GetRepositories<Case>().Delete(caseEntity);

        return true;
    }
    public async Task<IEnumerable<DocumentDTO>> ViewDocuments(int caseId)
    {
        var documents = await _unitOfWork.GetRepositories<Documents>()
            .Get()
            .Where(d => d.CaseID == caseId)
            .ToListAsync();

        return documents.Select(d => new DocumentDTO
        {
            DocumentId = d.Id,
            FileName = d.FileName,
            Url = d.Url,
            Type = "application/octet-stream",
            FileDataBase64 = Convert.ToBase64String(d.Data)
        });
    }

    public async Task<DocumentDTO> UploadDocument(int caseId, DocumentDTO documentDto)
    {
        var document = new Documents
        {
            FileName = documentDto.FileName,
            Data = Convert.FromBase64String(documentDto.FileDataBase64),
            CaseID = caseId,
            Url = GenerateDocumentUrl(documentDto.FileName)
        };

        await _unitOfWork.GetRepositories<Documents>().Add(document);

        documentDto.DocumentId = document.Id;
        documentDto.Url = document.Url;
        return documentDto;
    }

    private string GenerateDocumentUrl(string fileName)
    {
        return $"https://yourdomain.com/documents/{fileName}";
    }

    public async Task<bool> DeleteDocument(int documentId)
    {
        var document = await _unitOfWork.GetRepositories<Documents>().Get().FirstOrDefaultAsync(c=> c.Id == documentId);
        if (document == null)
            return false;

        await _unitOfWork.GetRepositories<Documents>().Delete(document);
        return true;
    }

    public Task<bool> AddDrugToCaseAsync(int caseId, DrugDTO drug)
    {
        throw new NotImplementedException();
    }

    public Task<bool> AddNoteToCaseAsync(int caseId, string note)
    {
        throw new NotImplementedException();
    }

    public Task<bool> AddSymptomToCaseAsync(int caseId, SymptomsDTO symptom)
    {
        throw new NotImplementedException();
    }

    public Task<bool> AddDiagnosisToCaseAsync(int caseId, string diagnosis)
    {
        throw new NotImplementedException();
    }
}
