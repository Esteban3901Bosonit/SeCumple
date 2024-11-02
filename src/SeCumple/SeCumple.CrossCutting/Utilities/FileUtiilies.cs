using System.Security.Cryptography;
using Microsoft.AspNetCore.Http;

namespace SeCumple.CrossCutting.Utilities;

public static class FileUtiilies
{
    public static async Task<string> GetFileSha256HashAsync(IFormFile file)
    {
        using var sha256 = SHA256.Create();
        await using var stream = file.OpenReadStream();
        var hash = await sha256.ComputeHashAsync(stream);
        return BitConverter.ToString(hash).Replace("-", "").ToLower();
    }
}