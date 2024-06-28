using Data.Interfaces;
using Data.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

public class PictureService : IPictureService
{
    private readonly IUnitOfWork _context;
    private readonly IWebHostEnvironment _environment;

    public PictureService(IUnitOfWork context, IWebHostEnvironment environment)
    {
        _context = context;
        _environment = environment;
    }

    public async Task<string> SavePictureAsync(string username,string fileName, byte[] imageData)
    {

        var user = await _context.GetRepositories<User>().Get().Where(c => c.Username == username).FirstOrDefaultAsync();
        
        if (user == null) { throw new Exception(); }

        var filePath = Path.Combine(_environment.WebRootPath, "uploads", fileName);

        // Ensure the uploads directory exists
        Directory.CreateDirectory(Path.GetDirectoryName(filePath));

        await File.WriteAllBytesAsync(filePath, imageData);

        var picture = new Picture
        {
            FileName = fileName,
            Data = imageData,
            Url = $"/uploads/{fileName}",
            User = user
        };

        try
        {
            await _context.GetRepositories<Picture>().Add(picture);
        } catch (Exception ex)
        {

        }

        return picture.Url;
    }

    public async Task<Picture> GetPictureAsync(int id)
    {
        return await _context.GetRepositories<Picture>().Get().Where(c=> c.Id == id).FirstOrDefaultAsync();
    }
}
