using SeCumple.Domain.Entities;

namespace SeCumple.Application.Specifications.Sectors;

public class SectorSpecification : BaseSpecification<Sector>
{
    public SectorSpecification(SpecificationParams sectorsParams) : base(
        x =>
            (!sectorsParams.Filters!.ContainsKey("cNombre") ||
             sectorsParams.Filters["cNombre"].Contains(x.Name!)) &&
            (!sectorsParams.Filters!.ContainsKey("cSigla") ||
             sectorsParams.Filters["cSigla"].Contains(x.Acronym!)) &&
            (!sectorsParams.Filters.ContainsKey("cEstado") ||
             x.Status.Equals(char.Parse(sectorsParams.Filters["cEstado"])))
    )
    {
        ApplyPaging(sectorsParams.PageSize * (sectorsParams.PageIndex - 1), sectorsParams.PageSize);

        AddOrderBy(x => x.Name!);
    }
}

public class SectorForCountingSpecification(SpecificationParams sectorsParams) : BaseSpecification<Sector>(x =>
    (!sectorsParams.Filters!.ContainsKey("cNombre") ||
     sectorsParams.Filters["cNombre"].Contains(x.Name!)) &&
    (!sectorsParams.Filters!.ContainsKey("cSigla") ||
     sectorsParams.Filters["cSigla"].Contains(x.Acronym!)) &&
    (!sectorsParams.Filters.ContainsKey("cEstado") ||
     x.Status.Equals(char.Parse(sectorsParams.Filters["cEstado"])))
);