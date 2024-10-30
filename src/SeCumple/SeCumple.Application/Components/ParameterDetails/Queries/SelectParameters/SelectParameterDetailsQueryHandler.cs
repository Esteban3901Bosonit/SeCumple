using MediatR;
using SeCumple.Application.Components.ParameterDetails.Dtos;
using SeCumple.Application.Dtos.Response;
using SeCumple.Domain.Entities;
using SeCumple.Infrastructure.Persistence.Interfaces;

namespace SeCumple.Application.Components.ParameterDetails.Queries.SelectParameters;

public class SelectParameterDetailsQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<SelectParameterDetailsQuery, ProcessResult<IReadOnlyList<ParameterDetailResponse>>>
{
    public async Task<ProcessResult<IReadOnlyList<ParameterDetailResponse>>> Handle(SelectParameterDetailsQuery request,
        CancellationToken cancellationToken)
    {
        var detailsParameter =
            await unitOfWork.Repository<ParameterDetail>().GetAsync(x => x.ParentId == request.ParentId);

        return new ProcessResult<IReadOnlyList<ParameterDetailResponse>>
        {
            Data = detailsParameter.Select(d => new ParameterDetailResponse
            {
                iDetParameter = d.Id,
                cNombre = d.Name!,
                cEstado = d.Status == '1' ? "ACTIVO" : "INACTIVO"
            }).ToList()
        };
    }
}

public class SelectParameterDetailsQuery : IRequest<ProcessResult<IReadOnlyList<ParameterDetailResponse>>>
{
    public int ParentId { get; set; }
}