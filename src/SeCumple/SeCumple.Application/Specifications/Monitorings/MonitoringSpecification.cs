using SeCumple.Domain.Entities;

namespace SeCumple.Application.Specifications.Monitorings;

public class MonitoringSpecification : BaseSpecification<Monitoring>
{
    public MonitoringSpecification(SpecificationParams monitoringParams) : base(
        x =>
            (!monitoringParams.Filters!.ContainsKey("iMovIntervencion") ||
             monitoringParams.Filters["iMovIntervencion"].Contains(x.InterventionId.ToString())) &&
            (!monitoringParams.Filters!.ContainsKey("iTipoMonitoreo") ||
             monitoringParams.Filters["iTipoMonitoreo"].Contains(x.MonitoringTypeId.ToString())) &&
            (!monitoringParams.Filters!.ContainsKey("iPrioridadMonitoreo") ||
             monitoringParams.Filters["iPrioridadMonitoreo"].Contains(x.MonitoringPriorityId.ToString())) &&
            (!monitoringParams.Filters.ContainsKey("cLugar") ||
             x.Room!.Contains(monitoringParams.Filters["cLugar"])) &&
            (!monitoringParams.Filters!.ContainsKey("dFechaInicio") ||
             DateTime.Parse(monitoringParams.Filters["dFechaInicio"]).Date <= x.StartDate.Date) &&
            (!monitoringParams.Filters!.ContainsKey("dFechaFin") ||
             DateTime.Parse(monitoringParams.Filters["dFechaFin"]).Date >= x.EndDate.Date) &&
            (!monitoringParams.Filters.ContainsKey("cEstado") ||
             x.Status.Equals(char.Parse(monitoringParams.Filters["cEstado"])))
    )
    {
        ApplyPaging(monitoringParams.PageSize * (monitoringParams.PageIndex - 1), monitoringParams.PageSize);
        AddInclude(x => x.RecordDocument!);
        AddOrderBy(x => x.StartDate!);
    }
}

public class MonitoringForCountingSpecification(SpecificationParams monitoringParams) : BaseSpecification<Monitoring>(
    x =>
        (!monitoringParams.Filters!.ContainsKey("iMovIntervencion") ||
         monitoringParams.Filters["iMovIntervencion"].Contains(x.InterventionId.ToString())) &&
        (!monitoringParams.Filters!.ContainsKey("iTipoMonitoreo") ||
         monitoringParams.Filters["iTipoMonitoreo"].Contains(x.MonitoringTypeId.ToString())) &&
        (!monitoringParams.Filters!.ContainsKey("iPrioridadMonitoreo") ||
         monitoringParams.Filters["iPrioridadMonitoreo"].Contains(x.MonitoringPriorityId.ToString())) &&
        (!monitoringParams.Filters.ContainsKey("cLugar") ||
         x.Room!.Contains(monitoringParams.Filters["cLugar"])) &&
        (!monitoringParams.Filters!.ContainsKey("dFechaInicio") ||
         DateTime.Parse(monitoringParams.Filters["dFechaInicio"]).Date <= x.StartDate.Date) &&
        (!monitoringParams.Filters!.ContainsKey("dFechaFin") ||
         DateTime.Parse(monitoringParams.Filters["dFechaFin"]).Date >= x.EndDate.Date) &&
        (!monitoringParams.Filters.ContainsKey("cEstado") ||
         x.Status.Equals(char.Parse(monitoringParams.Filters["cEstado"])))
);