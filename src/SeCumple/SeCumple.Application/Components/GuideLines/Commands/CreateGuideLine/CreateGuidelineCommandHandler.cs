using MediatR;
using SeCumple.Application.Components.GuideLines.Dtos;
using SeCumple.Application.Dtos.Response;
using SeCumple.Domain.Entities;
using SeCumple.Infrastructure.Persistence.Interfaces;

namespace SeCumple.Application.Components.GuideLines.Commands.CreateGuideLine;

public class CreateGuidelineCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<CreateGuidelineCommand, ProcessResult<GuideLineResponse>>
{
    public async Task<ProcessResult<GuideLineResponse>> Handle(CreateGuidelineCommand request,
        CancellationToken cancellationToken)
    {
        var guideline = new GuideLine
        {
            AxisId = request.iMaeEje,
            Numeral = request.cNum,
            Description = request.cDescripcion,
            CreatedBy = request.iCodUsuarioRegistro,
            Validated = '0'
        };

        var axis = await unitOfWork.Repository<Axis>().GetByIdAsync(request.iMaeEje);

        await unitOfWork.Repository<GuideLine>().AddAsync(guideline);

        return new ProcessResult<GuideLineResponse>
        {
            Data = new GuideLineResponse
            {
                cTipoRegistro = "Lineamiento",
                iMaeEje = guideline.AxisId,
                cNumEje = axis.Numeral!,
                iMaeLineamiento = guideline.Id,
                cEstado = guideline.Status == '1' ? "SI" : "NO",
                cNum = guideline.Numeral!,
            }
        };
    }
}

public class CreateGuidelineCommand : IRequest<ProcessResult<GuideLineResponse>>
{
    public int iMaeEje { get; set; }
    public string cNum { get; set; }
    public string cDescripcion { get; set; }
    public int iCodUsuarioRegistro { get; set; }
}