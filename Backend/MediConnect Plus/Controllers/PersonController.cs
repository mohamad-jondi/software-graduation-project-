using Domain.DTOs.Person;
using Domain.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Domain.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //[Authorize]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;
        private readonly ILogger _logger;

        public PersonController(IPersonService personService, ILogger<PersonController> logger)
        {
            _personService = personService;
            _logger = logger;
        }
        [HttpPost("EditPersonType/{Username}")]
        public async Task<ActionResult> editPersonType(string Username,personTypeDTO type)
        {
            try
            {
                var updatedInfo = await _personService.editPersonType(Username,type);
                if (updatedInfo != null)
                    return Ok(updatedInfo);
                else
                    return NotFound("User not found");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating person information");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("updateinfo")]
        public async Task<IActionResult> UpdateInfo(InfoUpdateDTO infoUpdate)
        {
            try
            {
                var updatedInfo = await _personService.UpdateInfo(infoUpdate);
                if (updatedInfo != null)
                    return Ok(updatedInfo);
                else
                    return NotFound("User not found");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating person information");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("edit-height-weight")]
        public async Task<IActionResult> EditHeightAndWeight(HeightAndWeightDTO infoUpdate)
        {
            try
            {
                var updatedInfo = await _personService.EditHightAndWeight(infoUpdate);
                if (updatedInfo != null)
                    return Ok(updatedInfo);
                else
                    return NotFound("User not found");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error editing height and weight");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("edit-occupation")]
        public async Task<IActionResult> EditOccupation(newOccupationDTO newOccupation)
        {
            try
            {
                var updatedInfo = await _personService.EditOccupation(newOccupation);
                if (updatedInfo != null)
                    return Ok(updatedInfo);
                else
                    return NotFound("User not found");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error editing occupation");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
