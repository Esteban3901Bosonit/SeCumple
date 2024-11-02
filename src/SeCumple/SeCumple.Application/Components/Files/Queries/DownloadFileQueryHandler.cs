using MediatR;
using SeCumple.Application.Components.Files.Dtos;
using SeCumple.Domain.Entities;
using SeCumple.Infrastructure.Persistence.Interfaces;

namespace SeCumple.Application.Components.Files.Queries;

public class DownloadFileQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<DownloadFileQuery, FileResponse>
{
    public async Task<FileResponse> Handle(DownloadFileQuery request, CancellationToken cancellationToken)
    {
        var file = await unitOfWork.Repository<FileUploaded>().GetByIdAsync(request.FileId);

        if (file == null || !File.Exists(file.Location))
            throw new Exception("El archivo no existe.");

        var fileBytes = File.ReadAllBytes(file.Location);

        return new FileResponse
        {
            Filename = file.Name + file.FileExtension,
            ContentFile = fileBytes
        };
    }
}

public class DownloadFileQuery : IRequest<FileResponse>
{
    public int FileId { get; set; }
}