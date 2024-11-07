using MediatR;
using SeCumple.Application.Components.Plans.Dtos;
using SeCumple.Application.Dtos.Response;
using SeCumple.CrossCutting.Enums;
using SeCumple.Domain.Entities;
using SeCumple.Infrastructure.Persistence.Interfaces;

namespace SeCumple.Application.Components.Plans.Commands.CreatePlan;

public class CreatePlanCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<CreatePlanCommand, ProcessResult<PlanResponse>>
{
    public async Task<ProcessResult<PlanResponse>> Handle(CreatePlanCommand request,
        CancellationToken cancellationToken)
    {
        var plan = new Plan()
        {
            Name = request.cNombre,
            Annotation = request.cObservacion,
            StartDate = request.dFechaInicio,
            EndDate = request.dFechaFin,
            CreatedBy = request.iCodUsuarioRegistro,
            DocumentId = request.iMaeDispositivo,
            DocumentTypeId = request.iTipoDispositivo,
            Version = 1,
            PlanStatusId = (await unitOfWork.Repository<ParameterDetail>().GetEntityAsync(x =>
                x.ParentId == (int?)ParameterEnum.PlanStatus && x.Name!.ToUpper().Equals("PENDIENTE"))).Id
        };

        await unitOfWork.Repository<Plan>().AddAsync(plan);
        
        foreach (var sectorId in request.iMaeSector)
        {
            var sectorPlan = new SectorsPlan
            {
                PlanId = plan.Id,
                SectorId = sectorId,
                CreatedBy = request.iCodUsuarioRegistro
            };

            await unitOfWork.Repository<SectorsPlan>().AddAsync(sectorPlan);
        }

        var document = await unitOfWork.Repository<Document>().GetByIdAsync(plan.DocumentId);

        return new ProcessResult<PlanResponse>
        {
            Data = new PlanResponse
            {
                iDetPlanCumplimiento = plan.Id,
                cNombre = plan.Name!,
                cObservacion = plan.Annotation!,
                dFechaInicio = plan.StartDate,
                dFechaFin = plan.EndDate,
                cNombreEstado = plan.PlanStatusId.ToString(),
                cNombreDispositivo = document.DocumentCode!,
                cTituloEstadoActual = plan.CurrentTitle!,
                cTituloDescripAcciones = plan.ActionsDescription!,
                cTituloDescripAlertas = plan.AlertsDescription!
            }
        };
    }
}

public class CreatePlanCommand : IRequest<ProcessResult<PlanResponse>>
{
    public string? cNombre { get; set; }
    public string? cObservacion { get; set; }
    public int iCodUsuarioRegistro { get; set; }
    public int iMaeDispositivo { get; set; }
    public int iTipoDispositivo { get; set; }
    public DateTime dFechaInicio { get; set; }
    public DateTime dFechaFin { get; set; }
    public int[]? iMaeSector { get; set; }
}