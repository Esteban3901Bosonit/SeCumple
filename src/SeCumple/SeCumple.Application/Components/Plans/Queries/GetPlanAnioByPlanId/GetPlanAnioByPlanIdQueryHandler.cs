using MediatR;
using SeCumple.Application.Components.Plans.Dtos;
using SeCumple.Application.Dtos.Response;
using SeCumple.Domain.Entities;
using SeCumple.Infrastructure.Persistence.Interfaces;

namespace SeCumple.Application.Components.Plans.Queries.GetPlanAnioByPlanId;

public class GetPlanAnioByPlanIdQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetPlanAnioByPlanIdQuery, ProcessResult<IReadOnlyList<PlanAnioResponse>>>
{
    public async Task<ProcessResult<IReadOnlyList<PlanAnioResponse>>> Handle(GetPlanAnioByPlanIdQuery request,
        CancellationToken cancellationToken)
    {
        var plansAnio = await unitOfWork.Repository<PlanAnio>()
            .GetAsync(x => x.PlanId == request.PlanId && x.Status == '1');

        return new ProcessResult<IReadOnlyList<PlanAnioResponse>>()
        {
            Data = plansAnio.Select(x => new PlanAnioResponse
            {
                iDetPlanCumpAnio = x.Id,
                iDetPlanCumplimiento = x.PlanId,
                iAnio = x.Anio
            }).ToList()
        };
    }
}

public class GetPlanAnioByPlanIdQuery : IRequest<ProcessResult<IReadOnlyList<PlanAnioResponse>>>
{
    public int PlanId { get; set; }
}