using MediatR;
using SeCumple.Application.Components.Documents.Commands.UpdateDocument;
using SeCumple.Application.Components.Documents.Dtos;
using SeCumple.Application.Dtos.Response;
using SeCumple.Domain.Entities;
using SeCumple.Infrastructure.Persistence.Interfaces;

namespace SeCumple.Application.Components.Documents.Commands.DeleteDocument;

public class DeleteDocumentCommandHandler(IUnitOfWork unitOfWork): IRequestHandler<DeleteDocumentCommand, ProcessResult<DocumentResponse>>
{
    public async Task<ProcessResult<DocumentResponse>> Handle(DeleteDocumentCommand request, CancellationToken cancellationToken)
    {
        var document = await unitOfWork.Repository<Document>().GetByIdAsync(request.Id);
        document.Status = '0';
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

public class DeleteDocumentCommand: IRequest<ProcessResult<DocumentResponse>>
{
    public int Id { get; set; }
    public int UserId { get; set; }
}