using System.Linq.Expressions;
using MediatR;
using SeCumple.Application.Components.Axles.Dtos;
using SeCumple.Application.Components.GuideLines.Dtos;
using SeCumple.Application.Dtos.Response;
using SeCumple.Domain.Entities;
using SeCumple.Infrastructure.Persistence.Interfaces;

namespace SeCumple.Application.Components.Axles.Queries.ListAxis;

public class GetAxisByDocumentIdQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetAxisByDocumentIdQuery, ProcessResult<IReadOnlyList<AxisResponse>>>
{
    public async Task<ProcessResult<IReadOnlyList<AxisResponse>>> Handle(GetAxisByDocumentIdQuery request,
        CancellationToken cancellationToken)
    {
        var includes = new List<Expression<Func<Axis, object>>>
        {
            x => x.GuideLines!
        };

        var axles = await unitOfWork.Repository<Axis>().GetAsync(
            x => x.Status == '1' && x.Validated == '1' && x.DocumentId == request.DocumentId, null, includes);

        return new ProcessResult<IReadOnlyList<AxisResponse>>
        {
            Data = axles.Select(a => new AxisResponse
            {
                cEstado = a.Status.ToString(),
                cTitulo = a.Title!,
                cNum = a.Numeral!,
                iMaeEje = a.Id,
                listLineamiento = a.GuideLines?.Select(g => new GuideLineResponse
                {
                    cEstado = g.Status.ToString(),
                    cNum = g.Numeral!,
                    cDescripcion = g.Description!,
                    iMaeLineamiento = g.Id
                }).ToList()
            }).ToList()
        };
    }
}

public class GetAxisByDocumentIdQuery : IRequest<ProcessResult<IReadOnlyList<AxisResponse>>>
{
    public int DocumentId { get; set; }
}