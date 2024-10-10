using MediatR;
using SeCumple.Application.Components.Documents.Dtos;
using SeCumple.Application.Dtos.Response;
using SeCumple.Domain.Entities;
using SeCumple.Infrastructure.Persistence.Context;

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
            Result = new DocumentResponse
            {
                Id = document.Id,
                DocumentCode = document.DocumentCode,
                DocumentDate = document.DocumentDate,
                DocumentTypeId = document.DocumentTypeId,
                DocumentType = document.DocumentType?.Name,
                Url = document.Url,
                Active = document.Active == '1' ? "SI" : "NO"
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