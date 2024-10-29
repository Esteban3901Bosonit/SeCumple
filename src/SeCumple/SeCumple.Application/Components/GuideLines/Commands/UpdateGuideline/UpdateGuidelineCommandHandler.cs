using MediatR;
using SeCumple.Application.Components.Axles.Dtos;
using SeCumple.Application.Components.GuideLines.Commands.CreateGuideLine;
using SeCumple.Application.Components.GuideLines.Dtos;
using SeCumple.Application.Dtos.Response;
using SeCumple.Domain.Entities;
using SeCumple.Infrastructure.Persistence.Interfaces;

namespace SeCumple.Application.Components.GuideLines.Commands.UpdateGuideline;

public class UpdateGuidelineCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateGuidelineCommand, ProcessResult<GuideLineResponse>>
{
    public async Task<ProcessResult<GuideLineResponse>> Handle(UpdateGuidelineCommand request,
        CancellationToken cancellationToken)
    {
        var guideline = await unitOfWork.Repository<GuideLine>().GetByIdAsync(request.iMaeLineamiento);

        guideline.Description = request.cDescripcion;
        guideline.ModifiedBy = request.iCodUsuarioRegistro;
        guideline.Numeral = request.cNum;
        guideline.AxisId = request.iMaeEje;

        await unitOfWork.Repository<GuideLine>().UpdateAsync(guideline);

        return new ProcessResult<GuideLineResponse>
        {
            Data = new GuideLineResponse
            {
                iMaeEje = guideline.Id,
                cEstado = guideline.Status == '1' ? "SI" : "NO",
                cNum = guideline.Numeral!,
                cValidado = guideline.Validated!,
                cTipoRegistro = "Eje"
            }
        };
    }
}

public class UpdateGuidelineCommand : CreateGuidelineCommand
{
    public int iMaeLineamiento { get; set; }
}