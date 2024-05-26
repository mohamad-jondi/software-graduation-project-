﻿using Domain.DTOs.Person;

namespace Domain.IServices
{
    public interface IPersonService
    {
        Task<PersonDTO> UpdateInfo(InfoUpdateDTO infoUpdate);
        Task<PersonDTO> EditOccupation(newOccupationDTO newOccupation);
        Task<PersonDTO> EditHightAndWeight(HeightAndWeightDTO infoUpdate);
    }
}