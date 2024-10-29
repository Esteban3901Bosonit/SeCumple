using MediatR;
using SeCumple.Application.Components.Interventions.Dtos;
using SeCumple.Application.Dtos.Request;
using SeCumple.Application.Dtos.Response;
using SeCumple.Application.Specifications;
using SeCumple.Application.Specifications.Interventions;
using SeCumple.Domain.Entities;
using SeCumple.Infrastructure.Persistence.Interfaces;

namespace SeCumple.Application.Components.Interventions.Queries.ListIntervention;

public class ListInterventionQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<ListInterventionQuery, ProcessResult<PaginationResponse<InterventionResponse>>>
{
    public async Task<ProcessResult<PaginationResponse<InterventionResponse>>> Handle(ListInterventionQuery request,
        CancellationToken cancellationToken)
    {
        var interventionSpecParams = new SpecificationParams
        {
            PageIndex = request.PageIndex,
            PageSize = request.PageSize,
            Filters = request.Filters,
            Sort = request.Sort,
        };

        var interventionSpec = new InterventionSpecification(interventionSpecParams);
        var interventions = await unitOfWork.Repository<Intervention>().GetAllWithSpec(interventionSpec);

        var spectCount = new InterventionForCountingSpecification(interventionSpecParams);
        var totalInterventions = await unitOfWork.Repository<Intervention>().CountAsync(spectCount);

        var rounded = Math.Ceiling(Convert.ToDecimal(totalInterventions) / Convert.ToDecimal(request.PageSize));
        var totalPages = Convert.ToInt32(rounded);


        var interventionResponse = interventions.Select(i => new InterventionResponse
        {
            iMovIntervencion = i.Id,
            cNombre = i.Name!,
            iMaeEje = i.GuideLine!.Axis!.Id!,
            cNombreEje = i.GuideLine!.Axis!.Title!,
            cNum = i.GuideLine!.Axis!.Numeral!,
            iMaeLineamiento = i.GuidelineId,
            cNombreLineamiento = i.GuideLine!.Description!,
            cNumLineamiento = i.GuideLine!.Numeral!,
            iMaeUnidadOrganica = i.OrganicUnitId,
            cNombreUnidadOrganica = i.OrganicUnit!.Name!,
            IMaeSector = i.OrganicUnit!.SectorId!,
            cNombreSector = i.OrganicUnit!.Sector!.Name!,
            cEstado = unitOfWork.Repository<ParameterDetail>().GetByIdAsync(i.InterventionStatusId).Result.Name!
        });

        return new ProcessResult<PaginationResponse<InterventionResponse>>
        {
            Data = new PaginationResponse<InterventionResponse>
            {
                Count = totalInterventions,
                Data = interventionResponse.ToList(),
                PageCount = totalPages,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                ResultByPage = interventions.Count
            }
        };
    }
}

public class ListInterventionQuery : PaginationRequest,
    IRequest<ProcessResult<PaginationResponse<InterventionResponse>>>
{
}