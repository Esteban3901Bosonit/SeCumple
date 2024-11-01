using SeCumple.Domain.Entities;

namespace SeCumple.Application.Specifications.Interventions;

public class InterventionSpecification : BaseSpecification<Intervention>
{
    public InterventionSpecification(SpecificationParams interventionsParams) : base(
        x =>
            (!interventionsParams.Filters!.ContainsKey("PlanCumplimiento") ||
             ParseIds(interventionsParams.Filters["PlanCumplimiento"]).Contains(x.PlanId)) &&
            (!interventionsParams.Filters!.ContainsKey("iMaeLineamiento") ||
             ParseIds(interventionsParams.Filters["iMaeLineamiento"]).Contains(x.GuidelineId)) &&
            (!interventionsParams.Filters!.ContainsKey("iMaeSector") ||
             ParseIds(interventionsParams.Filters["iMaeSector"]).Contains(x.OrganicUnit!.SectorId)) &&
            (!interventionsParams.Filters.ContainsKey("cNombre") ||
             x.Name!.Contains(interventionsParams.Filters["cNombre"])) &&
            (!interventionsParams.Filters.ContainsKey("Flag") ||
             (bool.Parse(interventionsParams.Filters["Flag"]) == true &&
              x.AssignedUsers!.Any(assignedUser =>
                  assignedUser.Assigment != null &&
                  assignedUser.Assigment.UserId == int.Parse(interventionsParams.Filters["iCodUsuario"]))
             )
            )
    )
    {
        AddInclude(x => x.AssignedUsers!);
        AddInclude(x => x.GuideLine!);
        AddInclude(x => x.OrganicUnit!);
        AddInclude(x => x.OrganicUnit!.Sector!);
        AddInclude(x => x.GuideLine!.Axis!);
        AddInclude(x => x.Regions!);
        ApplyPaging(interventionsParams.PageSize * (interventionsParams.PageIndex - 1), interventionsParams.PageSize);

        if (!string.IsNullOrEmpty(interventionsParams.Sort))
        {
            switch (interventionsParams.Sort)
            {
                // case "documentAsc":
                //     AddOrderBy(x => x.DocumentCode!);
                //     break;
                // case "documentDesc":
                //     AddOrderByDescending(x => x.DocumentCode!);
                //     break;
                // case "dateAsc":
                //     AddOrderBy(x => x.DocumentDate!);
                //     break;
                // case "dateDesc":
                //     AddOrderByDescending(x => x.DocumentDate!);
                //     break;
                // case "documentTypeAsc":
                //     AddOrderBy(x => x.DocumentTypeId!);
                //     break;
                // case "documentTypeDesc":
                //     AddOrderByDescending(x => x.DocumentTypeId!);
                //     break;
                default:
                    AddOrderBy(x => x.Id!);
                    break;
            }
        }
        else
        {
            AddOrderByDescending(x => x.Id!);
        }
    }
}

public class InterventionForCountingSpecification(SpecificationParams interventionsParams)
    : BaseSpecification<Intervention>(x =>
        (!interventionsParams.Filters!.ContainsKey("PlanCumplimiento") ||
         ParseIds(interventionsParams.Filters["PlanCumplimiento"]).Contains(x.PlanId)) &&
        (!interventionsParams.Filters!.ContainsKey("iMaeLineamiento") ||
         ParseIds(interventionsParams.Filters["iMaeLineamiento"]).Contains(x.GuidelineId)) &&
        (!interventionsParams.Filters!.ContainsKey("iMaeSector") ||
         ParseIds(interventionsParams.Filters["iMaeSector"]).Contains(x.OrganicUnit!.SectorId)) &&
        (!interventionsParams.Filters.ContainsKey("cNombre") ||
         x.Name!.Contains(interventionsParams.Filters["cNombre"])) &&
        (!interventionsParams.Filters.ContainsKey("Flag") ||
         (bool.Parse(interventionsParams.Filters["Flag"]) == true &&
          x.AssignedUsers!.Any(assignedUser =>
              assignedUser.Assigment != null &&
              assignedUser.Assigment.UserId == int.Parse(interventionsParams.Filters["iCodUsuario"]))
         )
        )
    );