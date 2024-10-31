using System.Linq.Expressions;
using MediatR;
using SeCumple.Application.Dtos.Response;
using SeCumple.Domain.Entities;
using SeCumple.Infrastructure.Persistence.Interfaces;

namespace SeCumple.Application.Components.Axles.Commands.ValidateAxis;

public class ValidateAxisCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<ValidateAxisCommand, ProcessResult<string>>
{
    public async Task<ProcessResult<string>> Handle(ValidateAxisCommand request, CancellationToken cancellationToken)
    {
        var includes = new List<Expression<Func<Axis, object>>>
        {
            x => x.GuideLines!
        };

        var axles = await unitOfWork.Repository<Axis>()
            .GetAsync(x => x.DocumentId == request.iMaeDispositivo, null, includes);
        
        foreach (var axis in axles)
        {
            axis.Validated = '1';
            axis.ModifiedBy = request.iCodUsuarioRegistro;
            await unitOfWork.Repository<Axis>().UpdateAsync(axis);

            if (axis.GuideLines == null) continue;
            foreach (var guideLine in axis.GuideLines)
            {
                guideLine.Validated = '1';
                guideLine.ModifiedBy = request.iCodUsuarioRegistro;
                await unitOfWork.Repository<GuideLine>().UpdateAsync(guideLine);
            }
        }

        return new ProcessResult<string>
        {
            Data = "Ejes y Lineamientos validados"
        };
    }
}

public class ValidateAxisCommand : IRequest<ProcessResult<string>>
{
    public int iMaeDispositivo { get; set; }
    public int iCodUsuarioRegistro { get; set; }
}