using MediatR;
using SeCumple.Application.Components.Documents.Dtos;
using SeCumple.Application.Dtos.Response;
using SeCumple.Domain.Entities;
using SeCumple.Infrastructure.Persistence.Interfaces;

namespace SeCumple.Application.Components.Documents.Commands.CreateDocument;

public class CreateDocumentCommandHandler(IUnitOfWork unitOfWork): IRequestHandler<CreateDocumentCommand, ProcessResult<DocumentResponse>>
{
    public async Task<ProcessResult<DocumentResponse>> Handle(CreateDocumentCommand request, CancellationToken cancellationToken)
    {
        var document = new Document
        {
            DocumentCode = request.cNumDispositivo,
            DocumentDate = request.dFechaDispositivo.Date,
            DocumentTypeId = request.iTipoDispositivo,
            Url = request.cLink,
            Validated = request.iValidado,
            Active = request.cEstVigencia,
            CreatedBy = request.iCodUsuarioRegistro
        };
        
        await unitOfWork.Repository<Document>().AddAsync(document);

        return new ProcessResult<DocumentResponse>
        {
            Data = new DocumentResponse
            {
                iMaeDispositivo = document.Id,
                cNumDispositivo = document.DocumentCode,
                dFechaDispositivo = document.DocumentDate,
                iTipoDispositivo = document.DocumentTypeId,
                DocumentType = document.DocumentType?.Name,
                cLink = document.Url,
                cEstado = document.Status == '1' ? "SI" : "NO"
            }
        };
    }
}

public class CreateDocumentCommand: IRequest<ProcessResult<DocumentResponse>>
{
    public string? cNumDispositivo { get; set; }
    public DateTime dFechaDispositivo { get; set; }
    public int iTipoDispositivo { get; set; }
    public string? cLink { get; set; }
    public char cEstVigencia { get; set; } = '0';
    public char iValidado { get; set; } = '0';
    public int iCodUsuarioRegistro { get; set; }
}