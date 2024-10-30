using MediatR;
using SeCumple.Application.Components.GuideLines.Dtos;
using SeCumple.Application.Dtos.Response;
using SeCumple.Domain.Entities;
using SeCumple.Infrastructure.Persistence.Interfaces;

namespace SeCumple.Application.Components.GuideLines.Commands.DeleteGuideLine;

public class DeleteGuidelineCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteGuidelineCommand, ProcessResult<GuideLineResponse>>
{
    public async Task<ProcessResult<GuideLineResponse>> Handle(DeleteGuidelineCommand request,
        CancellationToken cancellationToken)
    {
        var guideline = await unitOfWork.Repository<GuideLine>().GetByIdAsync(request.iMaeLineamiento);

        guideline.Status = '0';
        guideline.ModifiedBy = request.iCodUsuarioRegistro;

        await unitOfWork.Repository<GuideLine>().UpdateAsync(guideline);

        return new ProcessResult<GuideLineResponse>
        {
            Data = new GuideLineResponse
            {
                iMaeEje = guideline.Id,
                cEstado = guideline.Status == '1' ? "SI" : "NO",
                cNum = guideline.Numeral!,
                cValidado = guideline.Validated!,
                cTipoRegistro = "Lineamiento"
            }
        };
    }
}

public class DeleteGuidelineCommand : IRequest<ProcessResult<GuideLineResponse>>
{
    public int iMaeLineamiento { get; set; }
    public int iCodUsuarioRegistro { get; set; }
}