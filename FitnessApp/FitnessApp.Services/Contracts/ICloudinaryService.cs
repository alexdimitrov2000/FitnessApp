namespace FitnessApp.Services.Contracts
{
    using FitnessApp.Models;

    using Microsoft.AspNetCore.Http;

    using System;
    using System.Threading.Tasks;

    public interface ICloudinaryService
    {
        Task<Image> UploadImageAsync(Type entityType, IFormFile image);
    }
}
