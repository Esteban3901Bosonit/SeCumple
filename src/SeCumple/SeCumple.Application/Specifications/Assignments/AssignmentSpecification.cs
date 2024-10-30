using SeCumple.Domain.Entities;

namespace SeCumple.Application.Specifications.Assignments;

public class AssignmentSpecification : BaseSpecification<Assignment>
{
    public AssignmentSpecification(SpecificationParams assignmentsParams) : base(
        x =>
            (!assignmentsParams.Filters!.ContainsKey("iCodUsuario") ||
             ParseIds(assignmentsParams.Filters["iCodUsuario"]).Contains(x.UserId)) &&
            (!assignmentsParams.Filters!.ContainsKey("iMaeSector") ||
             ParseIds(assignmentsParams.Filters["iMaeSector"]).Contains(x.SectorId)) &&
            (!assignmentsParams.Filters!.ContainsKey("iDetPlanCumplimiento") ||
             ParseIds(assignmentsParams.Filters["iDetPlanCumplimiento"]).Contains(x.PlanId))
    )
    {
        AddInclude(x=>x.Sector!);
        
        ApplyPaging(assignmentsParams.PageSize * (assignmentsParams.PageIndex - 1), assignmentsParams.PageSize);

        AddOrderBy(x => x.Username!);
    }
}

public class SectorForCountingSpecification(SpecificationParams assignmentsParams) : BaseSpecification<Assignment>(x =>
    (!assignmentsParams.Filters!.ContainsKey("iCodUsuario") ||
     ParseIds(assignmentsParams.Filters["iCodUsuario"]).Contains(x.UserId)) &&
    (!assignmentsParams.Filters!.ContainsKey("iMaeSector") ||
     ParseIds(assignmentsParams.Filters["iMaeSector"]).Contains(x.SectorId)) &&
    (!assignmentsParams.Filters!.ContainsKey("iDetPlanCumplimiento") ||
     ParseIds(assignmentsParams.Filters["iDetPlanCumplimiento"]).Contains(x.PlanId))
);