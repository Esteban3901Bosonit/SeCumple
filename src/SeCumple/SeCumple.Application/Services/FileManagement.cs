using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using SeCumple.Application.Dtos;
using SeCumple.Application.Interfaces;
using SeCumple.CrossCutting.Utilities;

namespace SeCumple.Application.Services;

public class FileManagement(IConfiguration configuration) : IFileManagement
{
    public async Task<UploadedFile> UploadFile(IFormFile file, string? path = null)
    {
        var extension = Path.GetExtension(file.FileName);
        var filename =
            $"{Path.GetFileNameWithoutExtension(file.FileName)}_{DateTime.Now.ToString("yyyyMMdd_hhmmss")}{extension}";

        var folder = Path.Combine(Directory.GetCurrentDirectory(), configuration["Settings:cContenedorArchivos"],
            path ?? string.Empty);

        if (!Directory.Exists(folder))
        {
            Directory.CreateDirectory(folder);
        }

        var filePath = Path.Combine(folder, filename);

        using (var memoryStream = new MemoryStream())
        {
            await file.CopyToAsync(memoryStream);
            var contentFile = memoryStream.ToArray();
            await File.WriteAllBytesAsync(filePath, contentFile);
        }

        var uploadedFile = new UploadedFile
        {
            Name = filename,
            Extension = extension,
            Signature = await FileUtiilies.GetFileSha256HashAsync(file),
            Url = filePath.Replace("\\", "/")
        };

        return uploadedFile;
    }
    
    public async Task<byte[]> DownloadFile(string path)
    {
        return await File.ReadAllBytesAsync(path);
    }
}