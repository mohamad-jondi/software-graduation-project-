using Data.Models;


public interface IPictureService
{
    Task<string> SavePictureAsync(string username, string fileName, byte[] imageData);
    Task<Picture> GetPictureAsync(int id);
}
