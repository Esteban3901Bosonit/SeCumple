using MediatR;
using SeCumple.Application.Components.Documents.Commands.CreateDocument;
using SeCumple.Application.Components.Documents.Dtos;
using SeCumple.Application.Dtos.Response;
using SeCumple.Domain.Entities;
using SeCumple.Infrastructure.Persistence.Interfaces;

namespace SeCumple.Application.Components.Documents.Commands.UpdateDocument;

public class UpdateDocumentCommandHandler(IUnitOfWork unitOfWork): IRequestHandler<UpdateDocumentCommand, ProcessResult<DocumentResponse>>
{
    public async Task<ProcessResult<DocumentResponse>> Handle(UpdateDocumentCommand request, CancellationToken cancellationToken)
    {
        var document = await unitOfWork.Repository<Document>().GetByIdAsync(request.iMaeDispositivo);
        document.DocumentCode = request.cNumDispositivo;
        document.DocumentDate = request.dFechaDispositivo;
        document.DocumentTypeId = request.iTipoDispositivo;
        document.Url = request.cLink;
        document.Active = request.cEstVigencia;
        document.Active = request.iValidado;
        document.ModifiedBy = request.iCodUsuarioRegistro;
        
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

public class UpdateDocumentCommand: CreateDocumentCommand
{
    public int iMaeDispositivo { get; set; }
}