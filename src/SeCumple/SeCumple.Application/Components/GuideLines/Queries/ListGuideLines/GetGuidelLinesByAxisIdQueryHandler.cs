using MediatR;
using SeCumple.Application.Components.GuideLines.Dtos;
using SeCumple.Application.Dtos.Response;
using SeCumple.Domain.Entities;
using SeCumple.Infrastructure.Persistence.Interfaces;

namespace SeCumple.Application.Components.GuideLines.Queries.ListGuideLines;

public class GetGuidelLinesByAxisIdQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetGuidelLinesByAxisIdQuery, ProcessResult<IReadOnlyList<GuideLineResponse>>>
{
    public async Task<ProcessResult<IReadOnlyList<GuideLineResponse>>> Handle(GetGuidelLinesByAxisIdQuery request,
        CancellationToken cancellationToken)
    {
        var guideLines = await unitOfWork.Repository<GuideLine>().GetAsync(
            x => x.Status == '1' && x.Validated == "1" && x.AxisId == request.AxisId);

        return new ProcessResult<IReadOnlyList<GuideLineResponse>>
        {
            Data = guideLines.Select(a => new GuideLineResponse
            {
                cEstado = a.Status.ToString(),
                cDescripcion = a.Description!,
                cNum = a.Numeral!,
                iMaeLineamiento = a.Id
            }).ToList()
        };
    }
}

public class GetGuidelLinesByAxisIdQuery : IRequest<ProcessResult<IReadOnlyList<GuideLineResponse>>>
{
    public int AxisId { get; set; }
}