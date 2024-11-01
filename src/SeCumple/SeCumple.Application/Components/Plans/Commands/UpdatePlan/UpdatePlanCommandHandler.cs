using System.Linq.Expressions;
using MediatR;
using SeCumple.Application.Components.Plans.Commands.CreatePlan;
using SeCumple.Application.Components.Plans.Dtos;
using SeCumple.Application.Dtos.Response;
using SeCumple.Domain.Entities;
using SeCumple.Infrastructure.Persistence.Interfaces;

namespace SeCumple.Application.Components.Plans.Commands.UpdatePlan;

public class UpdatePlanCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<UpdatePlanCommand, ProcessResult<PlanResponse>>
{
    public async Task<ProcessResult<PlanResponse>> Handle(UpdatePlanCommand request,
        CancellationToken cancellationToken)
    {
        var includes = new List<Expression<Func<Plan, object>>>
        {
            x => x.Sectors!
        };

        var plan = await unitOfWork.Repository<Plan>()
            .GetEntityAsync(x => x.Id == request.iDetPlanCumplimiento, includes);

        plan.Name = request.cNombre;
        plan.DocumentId = request.iMaeDispositivo;
        plan.DocumentTypeId = request.iTipoDispositivo;
        plan.Annotation = request.cObservacion;
        plan.ModifiedBy = request.iCodUsuarioRegistro;
        plan.StartDate = request.dFechaInicio;
        plan.EndDate = request.dFechaFin;
        plan.CurrentTitle = request.cTituloEstadoActual;
        plan.ActionsDescription = request.cTituloDescripAcciones;
        plan.PlanStatusId = request.iEstadoPlan ?? plan.PlanStatusId;
        plan.AlertsDescription = request.cTituloDescripAlertas;
        
        await unitOfWork.Repository<Plan>().UpdateAsync(plan);

        var sectorToRemove = plan.Sectors!.Where(x => !request.iMaeSector.Contains(x.SectorId)).ToList();
        var newSectors = request.iMaeSector
            .Except(plan.Sectors!.Where(y => y.Status == '1').Select(x => x.SectorId)).ToList();

        foreach (var sector in sectorToRemove)
        {
            sector.Status = '0';
            sector.ModifiedBy = request.iCodUsuarioRegistro;
            await unitOfWork.Repository<SectorsPlan>().UpdateAsync(sector);
        }
        
        var insertSectors = newSectors.Select(x => new SectorsPlan
        {
            PlanId = plan.Id,
            SectorId = x,
            CreatedBy = request.iCodUsuarioRegistro
        }).ToList();

       unitOfWork.Repository<SectorsPlan>().AddRange(insertSectors);

        return new ProcessResult<PlanResponse>
        {
            Data = new PlanResponse
            {
                iDetPlanCumplimiento = plan.Id,
                cNombreEstado = plan.Status == '1' ? "ACTIVO" : "INACTIVO"
            }
        };
    }
}

public class UpdatePlanCommand : CreatePlanCommand
{
    public int iDetPlanCumplimiento { get; set; }
    public string cTituloEstadoActual { get; set; }
    public string cTituloDescripAcciones { get; set; }
    public string cTituloDescripAlertas { get; set; }
    public int? iEstadoPlan { get; set; }
}