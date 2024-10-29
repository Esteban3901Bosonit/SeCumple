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
            DocumentCode = request.DocumentCode,
            DocumentDate = request.DocumentDate.Date,
            DocumentTypeId = request.DocumentTypeId,
            Url = request.Url,
            Validated = request.Validated,
            Active = request.Active,
            CreatedBy = request.UserId
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
                cEstado = document.Active == '1' ? "SI" : "NO"
            }
        };
    }
}

public class CreateDocumentCommand: IRequest<ProcessResult<DocumentResponse>>
{
    public string? DocumentCode { get; set; }
    public DateTime DocumentDate { get; set; }
    public int DocumentTypeId { get; set; }
    public string? Url { get; set; }
    public char Active { get; set; } = '1';
    public int Validated { get; set; } = 1;
    public int UserId { get; set; }
}