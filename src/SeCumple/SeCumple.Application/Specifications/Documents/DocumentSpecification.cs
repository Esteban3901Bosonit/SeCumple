using SeCumple.Domain.Entities;

namespace SeCumple.Application.Specifications.Documents;

public class DocumentSpecification : BaseSpecification<Document>
{
    public DocumentSpecification(SpecificationParams documentsParams) : base(
        x =>
            (!documentsParams.Filters!.ContainsKey("documentTypeIds") || 
             ParseIds(documentsParams.Filters["documentTypeIds"]).Contains(x.DocumentTypeId)) &&
            
            (!documentsParams.Filters.ContainsKey("Active") || 
             x.Active.Equals(char.Parse(documentsParams.Filters["Active"]))) &&
            
            (!documentsParams.Filters.ContainsKey("DocumentCode") || 
             x.DocumentCode!.Contains(documentsParams.Filters["DocumentCode"]))
    )
    {
        AddInclude(x => x.DocumentType!);
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

public class DocumentForCountingSpecification(SpecificationParams documentsParams) : BaseSpecification<Document>(x =>
    (!documentsParams.Filters!.ContainsKey("documentTypeIds") || 
     ParseIds(documentsParams.Filters["documentTypeIds"]).Contains(x.DocumentTypeId)) &&
    
    (!documentsParams.Filters.ContainsKey("Active") || 
     x.Active.Equals(char.Parse(documentsParams.Filters["Active"]))) &&
    
    (!documentsParams.Filters.ContainsKey("DocumentCode") || 
     x.DocumentCode!.Contains(documentsParams.Filters["DocumentCode"]))
);