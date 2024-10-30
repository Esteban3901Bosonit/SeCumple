using SeCumple.Domain.Entities;

namespace SeCumple.Application.Specifications.Plans;

public class PlanSpecification : BaseSpecification<Plan>
{
    public PlanSpecification(SpecificationParams plansParams) : base(
        x =>
            (!plansParams.Filters!.ContainsKey("documentTypeIds") || 
             ParseIds(plansParams.Filters["documentTypeIds"]).Contains(x.DocumentTypeId)) &&
            
            (!plansParams.Filters!.ContainsKey("Name") ||
             plansParams.Filters["Name"].Contains(x.Name!)) &&
            
            x.Status.Equals('1')
    )
    {
        AddInclude(x => x.Document!);
        AddInclude(x => x.Assigments!);
        ApplyPaging(plansParams.PageSize * (plansParams.PageIndex - 1), plansParams.PageSize);

        if (!string.IsNullOrEmpty(plansParams.Sort))
        {
            switch (plansParams.Sort)
            {
                default:
                    AddOrderBy(x => x.CreationDate!);
                    break;
            }
        }
        else
        {
            AddOrderByDescending(x => x.CreationDate!);
        }
    }
}

public class PlanForCountingSpecification(SpecificationParams plansParams) : BaseSpecification<Plan>(x =>
    (!plansParams.Filters!.ContainsKey("iTipoDispositivo") || 
     ParseIds(plansParams.Filters["iTipoDispositivo"]).Contains(x.DocumentTypeId)) &&
    
    (!plansParams.Filters!.ContainsKey("cNombre") ||
     x.Name!.Contains(plansParams.Filters["cNombre"])) &&
            
    x.Status.Equals('1')
);