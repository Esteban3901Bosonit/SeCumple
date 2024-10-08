using System.Linq.Expressions;
using MediatR;
using SeCumple.Application.Dtos.Request;
using SeCumple.Application.Dtos.Response;
using SeCumple.Application.Specifications;
using SeCumple.Application.Specifications.Documents;
using SeCumple.Domain.Entities;
using SeCumple.Infrastructure.Persistence.Context;

namespace SeCumple.Application.Components.Documents.Queries.ListDocument;

public class ListDocumentsQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<ListDocumentCommand, PaginationResponse<Document>>
{
    public async Task<PaginationResponse<Document>> Handle(ListDocumentCommand request,
        CancellationToken cancellationToken)
    {
        var documentSpecParams = new SpecificationParams
        {
            PageIndex = request.PageIndex,
            PageSize = request.PageSize,
            Search = request.Search,
            Sort = request.Sort,
        };
        
        var documentSpec = new DocumentSpecification(documentSpecParams);
        var documents = await unitOfWork.Repository<Document>().GetAllWithSpec(documentSpec);
        
        var specCount = new DocumentForCountingSpecification(documentSpecParams);
        var totalDocuments = await unitOfWork.Repository<Document>().CountAsync(specCount);
        
        var rounded = Math.Ceiling(Convert.ToDecimal(totalDocuments) / Convert.ToDecimal(request.PageSize));
        var totalPages = Convert.ToInt32(rounded);
        
        var includes = new List<Expression<Func<Document, object>>>
        {
            x => x.DocumentType!
        };

        return new PaginationResponse<Document>()
        {
            Count = totalDocuments,
            Data = documents,
            PageCount = totalPages,
            PageIndex = request.PageIndex,
            PageSize = request.PageSize,
            ResultByPage = documents.Count
        };
    }
}

public class ListDocumentCommand : PaginationRequest, IRequest<PaginationResponse<Document>>
{
}