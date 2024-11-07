using MediatR;
using SeCumple.Application.Components.Plans.Dtos;
using SeCumple.Application.Dtos.Response;
using SeCumple.Domain.Entities;
using SeCumple.Infrastructure.Persistence.Interfaces;

namespace SeCumple.Application.Components.Plans.Commands.Duplicate;

public class DuplicatePlanCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<DuplicatePlanCommand, ProcessResult<PlanResponse>>
{
    public async Task<ProcessResult<PlanResponse>> Handle(DuplicatePlanCommand request,
        CancellationToken cancellationToken)
    {
        var plan = await unitOfWork.Repository<Plan>().GetByIdAsync(request.iDetPlanCumplimiento);

        var newPlan = plan.Duplicate();
        newPlan.CreatedBy = request.iCodUsuarioRegistro;
        newPlan.ParentPlanId = plan.ParentPlanId ?? plan.Id;

        await unitOfWork.Repository<Plan>().AddAsync(newPlan);
        
        plan.Status = '0';
        plan.ModifiedBy = request.iCodUsuarioRegistro;

        await unitOfWork.Repository<Plan>().UpdateAsync(plan);

        return new ProcessResult<PlanResponse>
        {
            Data = new PlanResponse
            {
                iDetPlanCumplimiento = newPlan.Id!,
                cNombre = newPlan.Name!,
                cNombreEstado = newPlan.Status == '1' ? "ACTIVO" : "INACTIVO"
            }
        };
    }
}

public class DuplicatePlanCommand : IRequest<ProcessResult<PlanResponse>>
{
    public int iDetPlanCumplimiento { get; set; }
    public int iCodUsuarioRegistro { get; set; }
}