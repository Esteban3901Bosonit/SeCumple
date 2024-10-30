using MediatR;
using SeCumple.Application.Components.Interventions.Dtos;
using SeCumple.Application.Dtos.Response;
using SeCumple.Domain.Entities;
using SeCumple.Infrastructure.Persistence.Interfaces;

namespace SeCumple.Application.Components.Interventions.Commands.DeleteInverventionCommandHandler;

public class DeleteInterventionCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteInterventionCommand, ProcessResult<InterventionResponse>>
{
    public async Task<ProcessResult<InterventionResponse>> Handle(DeleteInterventionCommand request,
        CancellationToken cancellationToken)
    {
        var intervention = await unitOfWork.Repository<Intervention>()
            .GetEntityAsync(x => x.Id == request.iMovIntervencion);

        intervention.ModifiedBy = request.iCodUsuarioRegistro;
        intervention.Status = '0';

        await unitOfWork.Repository<Intervention>().UpdateAsync(intervention);

        return new ProcessResult<InterventionResponse>
        {
            Data = new InterventionResponse
            {
                iMovIntervencion = intervention.Id,
                cNombre = intervention.Name!,
                iMaeEje = intervention.GuideLine!.Axis!.Id!,
                cNombreEje = intervention.GuideLine!.Axis!.Title!,
                cNum = intervention.GuideLine!.Axis!.Numeral!,
                iMaeLineamiento = intervention.GuidelineId,
                cNombreLineamiento = intervention.GuideLine!.Description!,
                cNumLineamiento = intervention.GuideLine!.Numeral!,
                iMaeUnidadOrganica = intervention.OrganicUnitId,
                cNombreUnidadOrganica = intervention.OrganicUnit!.Name!,
                IMaeSector = intervention.OrganicUnit!.SectorId!,
                cNombreSector = intervention.OrganicUnit!.Sector!.Name!,
                cEstado = "ELIMINADO"
            }
        };
    }
}

public class DeleteInterventionCommand : IRequest<ProcessResult<InterventionResponse>>
{
    public int iMovIntervencion { get; set; }
    public int iCodUsuarioRegistro { get; set; }
}