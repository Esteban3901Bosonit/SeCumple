using MediatR;
using SeCumple.Application.Components.Indicators.Dtos;
using SeCumple.Application.Dtos.Request;
using SeCumple.Application.Dtos.Response;
using SeCumple.Application.Specifications;
using SeCumple.Application.Specifications.Indicators;
using SeCumple.CrossCutting.Enums;
using SeCumple.Domain.Entities;
using SeCumple.Infrastructure.Persistence.Interfaces;

namespace SeCumple.Application.Components.Indicators.Queries.ListIndicator;

public class ListIndicatorQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<ListIndicatorQuery, ProcessResult<PaginationResponse<IndicatorResponse>>>
{
    public async Task<ProcessResult<PaginationResponse<IndicatorResponse>>> Handle(ListIndicatorQuery request,
        CancellationToken cancellationToken)
    {
        var indicatorSpecParams = new SpecificationParams
        {
            PageIndex = request.PageIndex,
            PageSize = request.PageSize,
            Filters = request.Filters,
            Sort = request.Sort,
        };

        var indicatorType = await unitOfWork.Repository<ParameterDetail>()
            .GetAsync(x => x.ParentId == (int)ParameterEnum.IndicatorType);

        var indicatorSpec = new IndicatorSpecification(indicatorSpecParams);
        var indicators = await unitOfWork.Repository<Indicator>().GetAllWithSpec(indicatorSpec);

        var specCount = new IndicatorForCountingSpecification(indicatorSpecParams);
        var totalIndicators = await unitOfWork.Repository<Indicator>().CountAsync(specCount);

        var rounded = Math.Ceiling(Convert.ToDecimal(totalIndicators) / Convert.ToDecimal(request.PageSize));
        var totalPages = Convert.ToInt32(rounded);

        var indicatorResponse = indicators.Select(p => new IndicatorResponse
        {
            cNombre = p.Name!,
            iNivelTipoIndicador = p.IndicatorType!.NumericalValue,
            Nivel = p.ParentId is null or < 1 ? 0 : 1 + (p.IndicatorType!.NumericalValue ?? 0),
            
        });

        return new ProcessResult<PaginationResponse<IndicatorResponse>>
        {
            Data = new PaginationResponse<IndicatorResponse>
            {
                Count = totalIndicators,
                Data = indicatorResponse.ToList(),
                PageCount = totalPages,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                ResultByPage = indicators.Count
            }
        };
    }
}

public class ListIndicatorQuery : PaginationRequest, IRequest<ProcessResult<PaginationResponse<IndicatorResponse>>>
{
}