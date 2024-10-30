using MediatR;
using SeCumple.Application.Components.Plans.Dtos;
using SeCumple.Application.Dtos.Response;
using SeCumple.Domain.Entities;
using SeCumple.Infrastructure.Persistence.Interfaces;

namespace SeCumple.Application.Components.Plans.Commands.DeletePlanAnio;

public class DeletePlanAnioCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<DeletePlanAnioCommand, ProcessResult<PlanAnioResponse>>
{
    public async Task<ProcessResult<PlanAnioResponse>> Handle(DeletePlanAnioCommand request,
        CancellationToken cancellationToken)
    {
        var planAnio = await unitOfWork.Repository<PlanAnio>().GetByIdAsync(request.iDetPlanCumpAnio);
        planAnio.Status = '0';
        planAnio.ModifiedBy = request.iCodUsuarioRegistro;

        await unitOfWork.Repository<PlanAnio>().UpdateAsync(planAnio);

        return new ProcessResult<PlanAnioResponse>
        {
            Data = new PlanAnioResponse
            {
                iDetPlanCumpAnio = planAnio.Id!,
                iDetPlanCumplimiento = planAnio.PlanId,
                cEstado = planAnio.Status == '1' ? "ACTIVO" : "INACTIVO"
            }
        };
    }
}

public class DeletePlanAnioCommand : IRequest<ProcessResult<PlanAnioResponse>>
{
    public int iDetPlanCumpAnio { get; set; }
    public int iCodUsuarioRegistro { get; set; }
}