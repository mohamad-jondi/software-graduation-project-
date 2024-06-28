using AutoMapper;
using Data.Interfaces;
using Data.Migrations;
using Data.Models;
using Domain.DTOs;
using Domain.DTOs.Cases;
using Domain.DTOs.Symptoms;
using Domain.IServices;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Ocsp;


public class CaseService : ICaseService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IWebHostEnvironment _env;

    public CaseService(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment env)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _env = env;
    }

    public async Task<IEnumerable<CaseDTO>> GetCasesAsync(string doctorUsername)
    {
        var cases = await _unitOfWork.GetRepositories<Case>()
            .Get()
            .Include(c => c.Doctor)
            .Include(c=> c.Patient)
            .Include(c=> c.Drugs)
            .Include(c => c.RelatedDocuments)
            .Include(c => c.Tests)
            .Include(c => c.symptoms)
            .Where(c => c.Doctor.Username == doctorUsername || c.Patient.Username== doctorUsername)
            .ToListAsync();

        return _mapper.Map<IEnumerable<CaseDTO>>(cases);
    }

    public async Task<CaseDTO> GetCaseByIdAsync(int caseId)
    {
        var caseEntity = await _unitOfWork.GetRepositories<Case>()
            .Get()
            .Include(c => c.Doctor)
            .Include(c=> c.Patient)
            .Include(c => c.Drugs)
            .Include(c => c.RelatedDocuments)
            .Include(c => c.Tests)
            .Include(c => c.symptoms)
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

    public async Task<CaseDTO> UpdateCaseAsync(int caseId, CaseForUpdating caseDTO)
    {
        var caseEntity = await _unitOfWork.GetRepositories<Case>().Get().FirstOrDefaultAsync(c => c.CaseId == caseId);
        if (caseEntity == null)
            return null;

        if (caseDTO.CaseDescription != null)
            caseEntity.CaseDescription = caseDTO.CaseDescription;

        if (caseDTO.Title != null)
            caseEntity.Title = caseDTO.Title;

        if (caseDTO.CreatedDate.HasValue)
            caseEntity.CreatedDate = caseDTO.CreatedDate.Value;

        if (caseDTO.Diagnosis != null)
            caseEntity.Diagnosis = caseDTO.Diagnosis;

        var x = await  _unitOfWork.GetRepositories<Case>().Update(caseEntity);
      
       

        return _mapper.Map<CaseDTO>(x);
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

    public async Task<bool> AddDrugToCaseAsync(int caseId, DrugDTO drug)
    {
        var caseEntity = await _unitOfWork.GetRepositories<Case>().Get().Include(d=> d.Drugs).FirstOrDefaultAsync(c => c.CaseId == caseId);
        if (caseEntity == null)
            return false;
        var x = _mapper.Map<Drug>(drug);
        caseEntity.Drugs.Add(x);
        await _unitOfWork.GetRepositories<Case>().Update(caseEntity);
        return true;

    }

    public async Task<RelatedDocumentDTO> AddDocumetAsync(int caseId, ImageUploadRequestDTO note)
    {
        var user = await _unitOfWork.GetRepositories<Case>().Get().Where(c => c.CaseId == caseId).FirstOrDefaultAsync();

        if (user == null) { throw new Exception(); }

        var filePath = Path.Combine(_env.WebRootPath, "uploads", note.FileName);

        Directory.CreateDirectory(Path.GetDirectoryName(filePath));

        var imageData = Convert.FromBase64String(note.Base64Image);

        await File.WriteAllBytesAsync(filePath, imageData);

        var picture = new Documents
        {
            FileName = note.FileName,
            Data = imageData,
           
            Url = $"/uploads/{note.FileName}",
            Case = user
        };

        
       var x=await _unitOfWork.GetRepositories<Documents>().Add(picture);
       
        return _mapper.Map<RelatedDocumentDTO>(x);
    }

    public async Task<bool> AddSymptomToCaseAsync(int caseId, SymptomsDTO symptom)
    {
        var caseEntity = await _unitOfWork.GetRepositories<Case>().Get().Include(d => d.symptoms).FirstOrDefaultAsync(c => c.CaseId == caseId);
        if (caseEntity == null)
            return false;
        var x = _mapper.Map<Symptoms>(symptom);
        caseEntity.symptoms.Add(x);
        await _unitOfWork.GetRepositories<Case>().Update(caseEntity);
        return true;
    }

    public async Task<bool> AddDiagnosisToCaseAsync(int caseId, string diagnosis)
    {
        var caseEntity = await _unitOfWork.GetRepositories<Case>().Get().FirstOrDefaultAsync(c => c.CaseId == caseId);
        if (caseEntity == null)
            return false;
        caseEntity.Diagnosis = diagnosis;
        await _unitOfWork.GetRepositories<Case>().Update(caseEntity);
        return true;
    }
}
