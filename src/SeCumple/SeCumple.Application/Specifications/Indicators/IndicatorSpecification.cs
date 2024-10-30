using SeCumple.Domain.Entities;

namespace SeCumple.Application.Specifications.Indicators;

public class IndicatorSpecification : BaseSpecification<Indicator>
{
    public IndicatorSpecification(SpecificationParams indicatorsParams) : base(
        x =>
            (!indicatorsParams.Filters!.ContainsKey("iMovIntervention") ||
             ParseIds(indicatorsParams.Filters["iMovIntervention"]).Contains(x.InterventionId)) &&
            (!indicatorsParams.Filters!.ContainsKey("iDetPlanCumpAnio") ||
             x.Intervention!.Plan!.PlanAnios!.Any(pa =>
                 pa.Id == int.Parse(indicatorsParams.Filters["iDetPlanCumpAnio"]))
            ) &&
            x.Status.Equals('1')
    )
    {
        ApplyPaging(indicatorsParams.PageSize * (indicatorsParams.PageIndex - 1), indicatorsParams.PageSize);

        if (!string.IsNullOrEmpty(indicatorsParams.Sort))
        {
            switch (indicatorsParams.Sort)
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

public class IndicatorForCountingSpecification(SpecificationParams indicatorsParams) : BaseSpecification<Indicator>(x =>
    (!indicatorsParams.Filters!.ContainsKey("iMovIntervention") ||
     ParseIds(indicatorsParams.Filters["iMovIntervention"]).Contains(x.InterventionId)) &&
    (!indicatorsParams.Filters!.ContainsKey("iDetPlanCumpAnio") ||
     x.Intervention!.Plan!.PlanAnios!.Any(pa =>
         pa.Id == int.Parse(indicatorsParams.Filters["iDetPlanCumpAnio"]))
    ) &&
    x.Status.Equals('1')
);