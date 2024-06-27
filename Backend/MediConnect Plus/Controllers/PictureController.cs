using Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class PictureController : ControllerBase
{
    private readonly IPictureService _pictureService;
    private readonly ILogger<PictureController> _logger;

    public PictureController(IPictureService pictureService, ILogger<PictureController> logger)
    {
        _pictureService = pictureService;
        _logger = logger;
    }

    [HttpPost("upload/{username}")]
    public async Task<ActionResult> UploadPicture(string username ,[FromBody] ImageUploadRequestDTO request)
    {
        try
        {
            if (request == null || string.IsNullOrEmpty(request.Base64Image))
            {
                return BadRequest("Invalid image data");
            }

            // Decode the base64 string to byte array
            var imageData = Convert.FromBase64String(request.Base64Image);

            var pictureUrl = await _pictureService.SavePictureAsync(username ,request.FileName, imageData);
            return Ok(new { Url = pictureUrl });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error uploading picture");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPicture(int id)
    {
        var picture = await _pictureService.GetPictureAsync(id);
        if (picture == null)
        {
            return NotFound();
        }

        return File(picture.Data, "image/jpeg", picture.FileName);
    }
}
