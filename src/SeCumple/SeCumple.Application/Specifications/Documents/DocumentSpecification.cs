using SeCumple.Domain.Entities;

namespace SeCumple.Application.Specifications.Documents;

public class DocumentSpecification : BaseSpecification<Document>
{
    public DocumentSpecification(SpecificationParams documentsParams) : base(
        x =>
            string.IsNullOrEmpty(documentsParams.Search) || x.DocumentCode!.Contains(documentsParams.Search)
    )
    {
        AddInclude(x=> x.DocumentType!);
        ApplyPaging(documentsParams.PageSize * (documentsParams.PageIndex - 1), documentsParams.PageSize);

        if (!string.IsNullOrEmpty(documentsParams.Sort))
        {
            switch (documentsParams.Sort)
            {
                case "documentAsc":
                    AddOrderBy(x => x.DocumentCode!);
                    break;
                case "documentDesc":
                    AddOrderByDescending(x => x.DocumentCode!);
                    break;
                case "dateAsc":
                    AddOrderBy(x => x.DocumentDate!);
                    break;
                case "dateDesc":
                    AddOrderByDescending(x => x.DocumentDate!);
                    break;
                case "documentTypeAsc":
                    AddOrderBy(x => x.DocumentTypeId!);
                    break;
                case "documentTypeDesc":
                    AddOrderByDescending(x => x.DocumentTypeId!);
                    break;
                default:
                    AddOrderBy(x => x.DocumentCode!);
                    break;
            }
        }
        else
        {
            AddOrderByDescending(x => x.DocumentCode!);
        }
    }
}

public class DocumentForCountingSpecification(SpecificationParams userParams) : BaseSpecification<Document>(x =>
    string.IsNullOrEmpty(userParams.Search) || x.DocumentCode!.Contains(userParams.Search)
);