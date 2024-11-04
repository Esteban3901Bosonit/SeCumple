using MediatR;
using Microsoft.AspNetCore.Http;
using SeCumple.Application.Components.Files.Dtos;
using SeCumple.Application.Dtos.Response;
using SeCumple.Application.Interfaces;
using SeCumple.CrossCutting.Utilities;
using SeCumple.Domain.Entities;
using SeCumple.Infrastructure.Persistence.Interfaces;

namespace SeCumple.Application.Components.Files.Commands;

public class UploadFileCommandHandler(IUnitOfWork unitOfWork, IFileManagement fileManagement)
    : IRequestHandler<UploadFileCommand, ProcessResult<UploadedFileResponse>>
{
    public async Task<ProcessResult<UploadedFileResponse>> Handle(UploadFileCommand request,
        CancellationToken cancellationToken)
    {
        if (request.File == null)
        {
            throw new Exception("Se requiere un archivo");
        }

        var uploadedFile = await fileManagement.UploadFile(request.File);

        var file = new FileUploaded
        {
            Name = uploadedFile.Name,
            FileExtension = uploadedFile.Extension,
            Size = $"{request.File.Length / 1024:F2} KB",
            CreatedBy = request.iCodUsuarioRegistro,
            Location = uploadedFile.Url,
            FileSignature = uploadedFile.Signature
        };

        var shaFile = await FileUtiilies.GetFileSha256HashAsync(request.File);

        var fileExists = await unitOfWork.Repository<FileUploaded>()
            .GetEntityAsync(x => x.FileSignature == shaFile);

        if (fileExists != null)
        {
            file = fileExists;
        }
        else
        {
            await unitOfWork.Repository<FileUploaded>().AddAsync(file);
        }

        return new ProcessResult<UploadedFileResponse>()
        {
            Data = new UploadedFileResponse()
            {
                FileId = file.Id,
                FileName = file.Name,
                FilePath = file.Location
            }
        };
    }
}

public class UploadFileCommand : IRequest<ProcessResult<UploadedFileResponse>>
{
    public IFormFile File { get; set; }
    public int iCodUsuarioRegistro { get; set; }
}