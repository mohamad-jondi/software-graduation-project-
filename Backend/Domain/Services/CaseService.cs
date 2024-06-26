using AutoMapper;
using Data.Interfaces;
using Data.Models;
using Domain.DTOs;
using Domain.DTOs.Cases;
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
            .Where(c => c.Doctor.Username == doctorUsername)
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

    public async Task<CaseDTO> AddCaseAsync(CaseDTO caseDTO)
    {
        var caseEntity = _mapper.Map<Case>(caseDTO);
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
    public async Task<IEnumerable<DocumentDTO>> ViewDocuments( int CaseID)
    {
        var doctor = await _unitOfWork.GetRepositories<Case>()
            .Get()
            .Include(d => d.RelatedDocuments)
            .FirstOrDefaultAsync(d => d.CaseId == CaseID);

        if (doctor == null)
            return null;

        return _mapper.Map<IEnumerable<DocumentDTO>>(doctor.RelatedDocuments);
    }

    public async Task<DocumentDTO> UploadDocument(int CaseID, DocumentDTO documentDTO)
    {
        var RCase = await _unitOfWork.GetRepositories<Case>().Get().Include(r=>r.RelatedDocuments).FirstOrDefaultAsync(d => d.CaseId == CaseID);
        if (RCase == null)
            return null;

        var document = _mapper.Map<Documents>(documentDTO);
        RCase.RelatedDocuments.Add(document); 
        await _unitOfWork.GetRepositories<Case>().Update(RCase);

        return _mapper.Map<DocumentDTO>(document);
    }

    public async Task<bool> DeleteDocument(int documentId)
    {
        var document = await _unitOfWork.GetRepositories<Documents>().Get().FirstOrDefaultAsync(d => d.DocumentId == documentId);
        if (document == null)
            return false;

        await _unitOfWork.GetRepositories<Documents>().Delete(document);

        return true;
    }
}
