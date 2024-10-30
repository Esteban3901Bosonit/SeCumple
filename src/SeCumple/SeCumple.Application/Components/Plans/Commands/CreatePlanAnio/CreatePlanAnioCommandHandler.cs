using MediatR;
using SeCumple.Application.Components.Plans.Dtos;
using SeCumple.Application.Dtos.Response;
using SeCumple.Domain.Entities;
using SeCumple.Infrastructure.Persistence.Interfaces;

namespace SeCumple.Application.Components.Plans.Commands.CreatePlanAnio;

public class CreatePlanAnioCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<CreatePlanAnioCommand, ProcessResult<PlanAnioResponse>>
{
    public async Task<ProcessResult<PlanAnioResponse>> Handle(CreatePlanAnioCommand request,
        CancellationToken cancellationToken)
    {
        var planAnio = new PlanAnio
        {
            Anio = request.iAnio,
            PlanId = request.iDetPlanCumplimiento,
            Status = '1',
            CreatedBy = request.iCodUsuarioRegistro
        };

        await unitOfWork.Repository<PlanAnio>().AddAsync(planAnio);

        return new ProcessResult<PlanAnioResponse>
        {
            Data = new PlanAnioResponse
            {
                iDetPlanCumplimiento = planAnio.PlanId,
                iAnio = planAnio.Anio,
                iDetPlanCumpAnio = planAnio.Id,
                cEstado = planAnio.Status == '1' ? "ACTIVO" : "INACTIVO"
            }
        };
    }
}

public class CreatePlanAnioCommand : IRequest<ProcessResult<PlanAnioResponse>>
{
    public int iDetPlanCumplimiento { get; set; }
    public int iAnio { get; set; }
    public int iCodUsuarioRegistro { get; set; }
}