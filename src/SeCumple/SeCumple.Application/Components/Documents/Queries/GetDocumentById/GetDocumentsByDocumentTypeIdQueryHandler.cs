using MediatR;
using SeCumple.Application.Components.Documents.Dtos;
using SeCumple.Application.Dtos.Response;
using SeCumple.Domain.Entities;
using SeCumple.Infrastructure.Persistence.Interfaces;

namespace SeCumple.Application.Components.Documents.Queries.GetDocumentById;

public class GetDocumentsByDocumentTypeIdQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetDocumentByDpcumentTypeIdQuery, ProcessResult<IReadOnlyList<DocumentResponse>>>
{
    public async Task<ProcessResult<IReadOnlyList<DocumentResponse>>> Handle(GetDocumentByDpcumentTypeIdQuery request,
        CancellationToken cancellationToken)
    {
        var documents = await unitOfWork.Repository<Document>()
            .GetAsync(x => x.DocumentTypeId == request.DocumentTypeId && x.Active == '1');

        return new ProcessResult<IReadOnlyList<DocumentResponse>>
        {
            Data = documents.Select(d => new DocumentResponse
            {
                iMaeDispositivo = d.Id,
                cNumDispositivo = d.DocumentCode
            }).ToList()
        };
    }
}

public class GetDocumentByDpcumentTypeIdQuery : IRequest<ProcessResult<IReadOnlyList<DocumentResponse>>>
{
    public int DocumentTypeId { get; set; }
}