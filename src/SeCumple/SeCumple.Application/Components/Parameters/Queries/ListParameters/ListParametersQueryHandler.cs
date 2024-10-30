using MediatR;
using SeCumple.Application.Components.ParameterDetails.Dtos;
using SeCumple.Application.Dtos.Request;
using SeCumple.Application.Dtos.Response;
using SeCumple.Application.Specifications;
using SeCumple.Application.Specifications.Parameters;
using SeCumple.Domain.Entities;
using SeCumple.Infrastructure.Persistence.Interfaces;

namespace SeCumple.Application.Components.Parameters.Queries.ListParameters;

public class ListParametersQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<ListParametersQuery, ProcessResult<PaginationResponse<ParameterDetailResponse>>>
{
    public async Task<ProcessResult<PaginationResponse<ParameterDetailResponse>>> Handle(ListParametersQuery request,
        CancellationToken cancellationToken)
    {
        var parameterSpecParams = new SpecificationParams
        {
            PageIndex = request.PageIndex,
            PageSize = request.PageSize,
            Filters = request.Filters,
            Sort = request.Sort,
        };

        var parameterSpec = new ParameterSpecification(parameterSpecParams);
        var parameters = await unitOfWork.Repository<Parameter>().GetAllWithSpec(parameterSpec);

        var specCount = new ParameterForCountingSpecification(parameterSpecParams);
        var totalParameters = await unitOfWork.Repository<Parameter>().CountAsync(specCount);

        var rounded = Math.Ceiling(Convert.ToDecimal(totalParameters) / Convert.ToDecimal(request.PageSize));
        var totalPages = Convert.ToInt32(rounded);

        var parameterResponse = parameters.Select(d => new ParameterDetailResponse
        {
            iDetParameterId = d.Id,
            cNombre = d.Name!,
            cEstado = d.Status == '1' ? "ACTIVO" : "INACTIVO"
        });

        return new ProcessResult<PaginationResponse<ParameterDetailResponse>>
        {
            Data = new PaginationResponse<ParameterDetailResponse>
            {
                Count = totalParameters,
                Data = parameterResponse.ToList(),
                PageCount = totalPages,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                ResultByPage = parameters.Count
            }
        };
    }
}

public class ListParametersQuery : PaginationRequest,
    IRequest<ProcessResult<PaginationResponse<ParameterDetailResponse>>>
{
}