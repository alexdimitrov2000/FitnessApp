namespace FitnessApp.Services.Implementation
{
    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Common.Constants;
    using Contracts;
    using FitnessApp.Data;
    using FitnessApp.Models;
    using Microsoft.AspNetCore.Http;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class CloudinaryService : ICloudinaryService
    {
        private const string RootFolder = "FitnessApp";

        private const string PostsImagesFolder = "PostsImages";
        private const string ProfilePicturesFolder = "UserProfileImages";

        private Dictionary<Type, string> EntityFolders = new Dictionary<Type, string>
        {
            { typeof(Post), PostsImagesFolder },
            { typeof(FitnessUser), ProfilePicturesFolder },
        };

        private readonly Cloudinary cloudinary;
        private readonly FitnessDbContext context;

        public CloudinaryService(FitnessDbContext context)
        {
            this.cloudinary = new Cloudinary(
                new Account(
                    CloudinaryDataConstants.Cloud,
                    CloudinaryDataConstants.ApiKey,
                    CloudinaryDataConstants.ApiSecret));

            this.context = context;
        }

        public async Task<Image> UploadImageAsync(Type entityType, IFormFile imageFile)
        {
            if (!this.IsImageType(imageFile))
                return null;

            var folder = string.Format(CloudinaryDataConstants.FileRoute, RootFolder, this.EntityFolders[entityType]);
            var fileStream = imageFile.OpenReadStream();

            ImageUploadParams imageUploadParams = new ImageUploadParams
            {
                File = new FileDescription(imageFile.FileName, fileStream),
                Folder = folder,
            };

            var uploadResults = this.cloudinary.Upload(imageUploadParams);

            var image = new Image
            {
                ImagePublicId = uploadResults.PublicId,
                ImageVersion = uploadResults.Version
            };

            await this.context.Images.AddAsync(image);
            await this.context.SaveChangesAsync();

            return image;
        }

        public string BuildPictureUrl(Image image)
        {
            return this.cloudinary.Api.UrlImgUp.Version(image.ImageVersion).BuildUrl(image.ImagePublicId);
        }

        public async Task<Image> GetDefaultProfilePictureAsync()
        {
            var image = new Image { ImagePublicId = CloudinaryDataConstants.DefaultImagePublicId, ImageVersion = CloudinaryDataConstants.DefaultImageVersion };
            
            if (!this.context.Images.Any(i => i.ImagePublicId == image.ImagePublicId && i.ImageVersion == image.ImageVersion))
            {
                await this.context.Images.AddAsync(image);
                await this.context.SaveChangesAsync();
            }

            return image;
        }

        private bool IsImageType(IFormFile image)
        {
            if (image.ContentType.Contains("image/jpg") || image.ContentType.Contains("image/jpeg") || image.ContentType.Contains("image/png"))
                return true;
            return false;
        }
    }
}
