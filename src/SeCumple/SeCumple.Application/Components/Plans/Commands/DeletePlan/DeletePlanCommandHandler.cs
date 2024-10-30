using System.Numerics;
using MediatR;
using SeCumple.Application.Components.Assignments.Dtos;
using SeCumple.Application.Components.Plans.Dtos;
using SeCumple.Application.Dtos.Response;
using SeCumple.Domain.Entities;
using SeCumple.Infrastructure.Persistence.Interfaces;

namespace SeCumple.Application.Components.Plans.Commands.DeletePlan;

public class DeletePlanCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<DeletePlanCommand, ProcessResult<PlanResponse>>
{
    public async Task<ProcessResult<PlanResponse>> Handle(DeletePlanCommand request,
        CancellationToken cancellationToken)
    {
        var plan = await unitOfWork.Repository<Plan>().GetByIdAsync(request.iDetPlanCumplimiento);
        plan.Status = '0';
        plan.ModifiedBy = request.iCodUsuarioRegistro;

        await unitOfWork.Repository<Plan>().UpdateAsync(plan);

        return new ProcessResult<PlanResponse>
        {
            Data = new PlanResponse
            {
                iDetPlanCumplimiento = plan.Id!,
                cNombre = plan.Name!,
                cNombreEstado = plan.Status == '1' ? "ACTIVO" : "INACTIVO"
            }
        };
    }
}

public abstract class DeletePlanCommand : IRequest<ProcessResult<PlanResponse>>
{
    public int iDetPlanCumplimiento { get; set; }
    public int iCodUsuarioRegistro { get; set; }
}