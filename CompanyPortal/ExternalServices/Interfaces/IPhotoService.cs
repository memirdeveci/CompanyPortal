using CloudinaryDotNet.Actions;

namespace CompanyPortal.ExternalServices.Interfaces
{
    public interface IPhotoService
    {
        Task<ImageUploadResult> AddPhotoAsync(IFormFile file);
        Task<DeletionResult> DeletePhotoAsync(string publicUrl);
        Task<VideoUploadResult> AddVideoAsync(IFormFile file);
    }
}
