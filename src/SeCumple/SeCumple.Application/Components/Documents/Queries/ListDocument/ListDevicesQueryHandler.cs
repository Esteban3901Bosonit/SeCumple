using System.Linq.Expressions;
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
        var includes = new List<Expression<Func<Document, object>>>
        {
            x => x.DocumentType!
        };
        
        var documents = await unitOfWork.Repository<Document>()
            .GetAsync(null, x => x.OrderBy(y => y.DocumentCode), includes);

        return documents;
    }
}

public class ListDocumentCommand : IRequest<IReadOnlyList<Document>>
{
}