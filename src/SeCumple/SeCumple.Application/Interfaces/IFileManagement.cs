using Microsoft.AspNetCore.Http;
using SeCumple.Application.Dtos;

namespace SeCumple.Application.Interfaces;

public interface IFileManagement
{
    Task<UploadedFile> UploadFile(IFormFile file, string? path = null);
}