using MediatR;
using SeCumple.Application.Components.Plans.Dtos;
using SeCumple.Application.Dtos.Response;
using SeCumple.Domain.Entities;
using SeCumple.Infrastructure.Persistence.Interfaces;

namespace SeCumple.Application.Components.Plans.Queries.SelectPlan;

public class SelectPlanQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<SelectPlanQuery, ProcessResult<IReadOnlyList<PlanResponse>>>
{
    public async Task<ProcessResult<IReadOnlyList<PlanResponse>>> Handle(SelectPlanQuery request,
        CancellationToken cancellationToken)
    {
        var plans = await unitOfWork.Repository<Plan>().GetAsync(x => x.Status == '1');

        return new ProcessResult<IReadOnlyList<PlanResponse>>
        {
            Data = plans.Select(s => new PlanResponse
            {
                iDetPlanCumplimiento = s.Id,
                cNombre = s.Name!,
                cEstado = s.Status == '1' ? "ACTIVO" : "INACTIVO"
            }).ToList()
        };
    }
}

public class SelectPlanQuery : IRequest<ProcessResult<IReadOnlyList<PlanResponse>>>
{
}