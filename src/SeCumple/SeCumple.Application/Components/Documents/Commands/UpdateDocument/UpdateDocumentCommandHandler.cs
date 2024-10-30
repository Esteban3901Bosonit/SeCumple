using MediatR;
using SeCumple.Application.Components.Documents.Dtos;
using SeCumple.Application.Dtos.Response;
using SeCumple.Domain.Entities;
using SeCumple.Infrastructure.Persistence.Interfaces;

namespace SeCumple.Application.Components.Documents.Commands.UpdateDocument;

public class UpdateDocumentCommandHandler(IUnitOfWork unitOfWork): IRequestHandler<UpdateDocumentCommand, ProcessResult<DocumentResponse>>
{
    public async Task<ProcessResult<DocumentResponse>> Handle(UpdateDocumentCommand request, CancellationToken cancellationToken)
    {
        var document = await unitOfWork.Repository<Document>().GetByIdAsync(request.Id);
        document.DocumentCode = request.DocumentCode;
        document.DocumentDate = request.DocumentDate;
        document.DocumentTypeId = request.DocumentTypeId;
        document.Url = request.Url;
        document.Active = request.Active;
        document.Active = request.Validated;
        document.ModifiedBy = request.UserId;
        
        await unitOfWork.Repository<Document>().UpdateAsync(document);

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

public abstract class UpdateDocumentCommand: IRequest<ProcessResult<DocumentResponse>>
{
    public int Id { get; set; }
    public string? DocumentCode { get; set; }
    public DateTime DocumentDate { get; set; }
    public int DocumentTypeId { get; set; }
    public string? Url { get; set; }
    public char Active { get; set; }
    public char Validated { get; set; }
    public int UserId { get; set; }
}