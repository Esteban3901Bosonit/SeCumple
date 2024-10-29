using SeCumple.Domain.Entities;

namespace SeCumple.Application.Specifications.Parameters;

public class ParameterSpecification : BaseSpecification<Parameter>
{
    public ParameterSpecification(SpecificationParams parametersParams) : base(
        x =>
            (!parametersParams.Filters!.ContainsKey("cNombre") ||
             parametersParams.Filters["cNombre"].Contains(x.Name!)) &&
            (!parametersParams.Filters.ContainsKey("Active") ||
             x.Status.Equals(char.Parse(parametersParams.Filters["Active"])))
    )
    {
        ApplyPaging(parametersParams.PageSize * (parametersParams.PageIndex - 1), parametersParams.PageSize);

        AddOrderBy(x => x.Name!);
    }
}

public class ParameterForCountingSpecification(SpecificationParams parametersParams) : BaseSpecification<Parameter>(x =>
    (!parametersParams.Filters!.ContainsKey("cNombre") ||
     parametersParams.Filters["cNombre"].Contains(x.Name!)) &&
    (!parametersParams.Filters.ContainsKey("Active") ||
     x.Status.Equals(char.Parse(parametersParams.Filters["Active"])))
);