using SeCumple.Domain.Entities;

namespace SeCumple.Application.Specifications.Alerts;

public class AlertSpecification : BaseSpecification<Alert>
{
    public AlertSpecification(SpecificationParams alertsParams) : base(
        x =>
            // (!alertsParams.Filters!.ContainsKey("iMaeSector") ||
            //  ParseIds(alertsParams.Filters["iMaeSector"])
            //      .Intersect(SplitIds(x.SectorIds!, ','))
            //      .Any()) &&
            // (!alertsParams.Filters!.ContainsKey("cModulo") ||
            //  SplitString(alertsParams.Filters["cModulo"], ',')
            //      .Intersect(SplitString(x.Module!, ','))
            //      .Any()) &&
            (!alertsParams.Filters!.ContainsKey("iTipoAlerta") ||
             alertsParams.Filters["iTipoAlerta"].Contains(x.AlertTypeId.ToString())) &&
            (!alertsParams.Filters!.ContainsKey("iPeriodicidad") ||
             alertsParams.Filters["iPeriodicidad"].Contains(x.PeriodicityId.ToString())) &&
            (!alertsParams.Filters!.ContainsKey("dFechaInicio") ||
             DateTime.Parse(alertsParams.Filters["dFechaInicio"]).Date <= x.StartDate.Date) &&
            (!alertsParams.Filters!.ContainsKey("dFechaFin") ||
             DateTime.Parse(alertsParams.Filters["dFechaFin"]).Date >= x.EndDate.Date) &&
            (!alertsParams.Filters.ContainsKey("cEstado") ||
             x.Status.Equals(char.Parse(alertsParams.Filters["cEstado"])))
    )
    {
        ApplyPaging(alertsParams.PageSize * (alertsParams.PageIndex - 1), alertsParams.PageSize);

        AddOrderBy(x => x.Name!);
    }
}

public class AlertForCountingSpecification(SpecificationParams alertsParams) : BaseSpecification<Alert>(x =>
    // (!alertsParams.Filters!.ContainsKey("iMaeSector") ||
    //  ParseIds(alertsParams.Filters["iMaeSector"])
    //      .Intersect(SplitIds(x.SectorIds!, ','))
    //      .Any()) &&
    // (!alertsParams.Filters!.ContainsKey("cModulo") ||
    //  SplitString(alertsParams.Filters["cModulo"], ',')
    //      .Intersect(SplitString(x.Module!, ','))
    //      .Any()) &&
    (!alertsParams.Filters!.ContainsKey("iTipoAlerta") ||
     alertsParams.Filters["iTipoAlerta"].Contains(x.AlertTypeId.ToString())) &&
    (!alertsParams.Filters!.ContainsKey("iPeriodicidad") ||
     alertsParams.Filters["iPeriodicidad"].Contains(x.PeriodicityId.ToString())) &&
    (!alertsParams.Filters!.ContainsKey("dFechaInicio") ||
     DateTime.Parse(alertsParams.Filters["dFechaInicio"]).Date <= x.StartDate.Date) &&
    (!alertsParams.Filters!.ContainsKey("dFechaFin") ||
     DateTime.Parse(alertsParams.Filters["dFechaFin"]).Date >= x.EndDate.Date) &&
    (!alertsParams.Filters.ContainsKey("cEstado") ||
     x.Status.Equals(char.Parse(alertsParams.Filters["cEstado"])))
);