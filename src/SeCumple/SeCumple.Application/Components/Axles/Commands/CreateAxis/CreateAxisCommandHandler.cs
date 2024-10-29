using MediatR;
using SeCumple.Application.Components.Axles.Dtos;
using SeCumple.Application.Dtos.Response;
using SeCumple.Domain.Entities;
using SeCumple.Infrastructure.Persistence.Interfaces;

namespace SeCumple.Application.Components.Axles.Commands.CreateAxis;

public class CreateAxisCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<CreateAxisCommand, ProcessResult<AxisResponse>>
{
    public async Task<ProcessResult<AxisResponse>> Handle(CreateAxisCommand request,
        CancellationToken cancellationToken)
    {
        var axis = new Axis
        {
            DocumentId = request.iMaeDispositivo,
            Numeral = request.cNum,
            Title = request.cTitulo,
            Description = request.cDescripcion,
            CreatedBy = request.iCodUsuarioRegistro,
            Validated = '0'
        };

        await unitOfWork.Repository<Axis>().AddAsync(axis);

        return new ProcessResult<AxisResponse>
        {
            Data = new AxisResponse
            {
                cTipoRegistro = "Eje",
                iMaeEje = axis.Id,
                cEstado = axis.Status == '1' ? "SI" : "NO",
                cNum = axis.Numeral!,
                cTitulo = request.cTitulo!
            }
        };
    }
}

public class CreateAxisCommand : IRequest<ProcessResult<AxisResponse>>
{
    public int iMaeDispositivo { get; set; }
    public string? cNum { get; set; }
    public string? cTitulo { get; set; }
    public string? cDescripcion { get; set; }
    public int iCodUsuarioRegistro { get; set; }
}