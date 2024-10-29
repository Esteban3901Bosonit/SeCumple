using MediatR;
using SeCumple.Application.Components.Plans.Dtos;
using SeCumple.Application.Dtos.Request;
using SeCumple.Application.Dtos.Response;
using SeCumple.Application.Specifications;
using SeCumple.Application.Specifications.Plans;
using SeCumple.Domain.Entities;
using SeCumple.Infrastructure.Persistence.Interfaces;

namespace SeCumple.Application.Components.Plans.Queries.ListPlan;

public class ListPlanQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<ListPlanQuery, ProcessResult<PaginationResponse<PlanResponse>>>
{
    public async Task<ProcessResult<PaginationResponse<PlanResponse>>> Handle(ListPlanQuery request,
        CancellationToken cancellationToken)
    {
        var planSpecParams = new SpecificationParams
        {
            PageIndex = request.PageIndex,
            PageSize = request.PageSize,
            Filters = request.Filters,
            Sort = request.Sort,
        };

        var planSpec = new PlanSpecification(planSpecParams);
        var plans = await unitOfWork.Repository<Plan>().GetAllWithSpec(planSpec);

        var specCount = new PlanForCountingSpecification(planSpecParams);
        var totalPlans = await unitOfWork.Repository<Plan>().CountAsync(specCount);

        var rounded = Math.Ceiling(Convert.ToDecimal(totalPlans) / Convert.ToDecimal(request.PageSize));
        var totalPages = Convert.ToInt32(rounded);

        var planResponse = plans.Select(p => new PlanResponse()
        {
            cNombre = p.Name!,
            cObservacion = p.Annotation!,
            cNombreEstado = p.PlanStatusId.ToString(), //todo: extraer desde detaparameter
            cNombreDispositivo = p.Document!.DocumentCode!,
            dFechaInicio = p.StartDate,
            dFechaFin = p.EndDate
        });

        return new ProcessResult<PaginationResponse<PlanResponse>>
        {
            Data = new PaginationResponse<PlanResponse>
            {
                Count = totalPlans,
                Data = planResponse.ToList(),
                PageCount = totalPages,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                ResultByPage = plans.Count
            }
        };
    }
}

public class ListPlanQuery : PaginationRequest, IRequest<ProcessResult<PaginationResponse<PlanResponse>>>
{
}