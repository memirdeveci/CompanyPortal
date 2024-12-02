using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CompanyPortal.ExternalServices.ExternalServiceModels;
using CompanyPortal.ExternalServices.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;

namespace CompanyPortal.ExternalServices
{
    public class PhotoServices : IPhotoService
    {
        private readonly Cloudinary _cloudinary;
        public PhotoServices(IOptions<CloudinarySettings> cloudinarySettings)
        {
            var account = new Account
            {
                Cloud = cloudinarySettings.Value.CloudName,
                ApiSecret = cloudinarySettings.Value.ApiSecret,
                ApiKey = cloudinarySettings.Value.ApiKey
            };
            _cloudinary = new Cloudinary(account);
        }
        public async Task<ImageUploadResult> AddPhotoAsync(IFormFile file)
        {
            var imageResult = new ImageUploadResult();

            try
            {
                if (file.Length == 0) throw new Exception();

                using var stream = file.OpenReadStream();

                var uploadParams = new ImageUploadParams {
                    File = new FileDescription(file.FileName, stream),
                    Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face")
                };

                imageResult = await _cloudinary.UploadAsync(uploadParams);

                return imageResult;
            }
            catch (Exception)
            {
                return imageResult;
            }
        }

        public async Task<DeletionResult> DeletePhotoAsync(string publicUrl)
        {
            var publicId = publicUrl.Split('/').Last().Split('.')[0];
            var deleteParams = new DeletionParams(publicId);
            return await _cloudinary.DestroyAsync(deleteParams);
        }
    }
}
