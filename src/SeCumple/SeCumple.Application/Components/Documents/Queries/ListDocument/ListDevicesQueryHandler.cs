using MediatR;
using SeCumple.Domain.Entities;
using SeCumple.Infrastructure.Persistence.Context;

namespace SeCumple.Application.Components.Documents.Queries.ListDocument;

public class ListDocumentsQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<ListDocumentCommand, IReadOnlyList<Document>>
{
    public async Task<IReadOnlyList<Document>> Handle(ListDocumentCommand request,
        CancellationToken cancellationToken)
    {
        return await unitOfWork.Repository<Document>().GetAllAsync();
    }
}

public class ListDocumentCommand : IRequest<IReadOnlyList<Document>>
{
}