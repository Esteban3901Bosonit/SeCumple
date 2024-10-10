using MediatR;
using SeCumple.Application.Components.Documents.Dtos;
using SeCumple.Application.Dtos.Request;
using SeCumple.Application.Dtos.Response;
using SeCumple.Application.Specifications;
using SeCumple.Application.Specifications.Documents;
using SeCumple.Domain.Entities;
using SeCumple.Infrastructure.Persistence.Context;

namespace SeCumple.Application.Components.Documents.Queries.ListDocument;

public class ListDocumentsQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<ListDocumentQuery, ProcessResult<PaginationResponse<DocumentResponse>>>
{
    public async Task<ProcessResult<PaginationResponse<DocumentResponse>>> Handle(ListDocumentQuery request,
        CancellationToken cancellationToken)
    {
        var documentSpecParams = new SpecificationParams
        {
            PageIndex = request.PageIndex,
            PageSize = request.PageSize,
            Filters = request.Filters,
            Sort = request.Sort,
        };

        var documentSpec = new DocumentSpecification(documentSpecParams);
        var documents = await unitOfWork.Repository<Document>().GetAllWithSpec(documentSpec);

        var specCount = new DocumentForCountingSpecification(documentSpecParams);
        var totalDocuments = await unitOfWork.Repository<Document>().CountAsync(specCount);

        var rounded = Math.Ceiling(Convert.ToDecimal(totalDocuments) / Convert.ToDecimal(request.PageSize));
        var totalPages = Convert.ToInt32(rounded);

        var documentResponse = documents.Select(d => new DocumentResponse
        {
            Id = d.Id,
            DocumentCode = d.DocumentCode,
            DocumentDate = d.DocumentDate,
            DocumentTypeId = d.DocumentTypeId,
            DocumentType = d.DocumentType?.Name,
            Url = d.Url,
            Active = d.Active == '1' ? "SI" : "NO"
        });
        
        return new ProcessResult<PaginationResponse<DocumentResponse>>
        {
            Result = new PaginationResponse<DocumentResponse>
            {
                Count = totalDocuments,
                Data = documentResponse.ToList(),
                PageCount = totalPages,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                ResultByPage = documents.Count
            }
        };
    }
}

public class ListDocumentQuery : PaginationRequest, IRequest<ProcessResult<PaginationResponse<DocumentResponse>>>
{
}