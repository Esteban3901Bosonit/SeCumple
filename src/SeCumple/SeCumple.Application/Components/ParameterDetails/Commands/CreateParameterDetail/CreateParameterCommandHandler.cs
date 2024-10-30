using MediatR;
using SeCumple.Application.Components.ParameterDetails.Dtos;
using SeCumple.Application.Dtos.Response;
using SeCumple.Domain.Entities;
using SeCumple.Infrastructure.Persistence.Interfaces;

namespace SeCumple.Application.Components.ParameterDetails.Commands.CreateParameterDetail;

public class CreateParameterDetailCommandHandler(IUnitOfWork unitOfWork): IRequestHandler<CreateParameterDetailCommand, ProcessResult<ParameterDetailResponse>>
{
    public async Task<ProcessResult<ParameterDetailResponse>> Handle(CreateParameterDetailCommand request, CancellationToken cancellationToken)
    {
        var parameter = new ParameterDetail
        {
            ParentId = request.iMaeParameter,
            Name = request.cNombre,
            CreatedBy = request.iCodUsuarioRegistro
        };
        
        await unitOfWork.Repository<ParameterDetail>().AddAsync(parameter);

        return new ProcessResult<ParameterDetailResponse>
        {
            Data = new ParameterDetailResponse
            {
                iDetParameterId = parameter.Id,
                cNombre = parameter.Name,
                cEstado = parameter.Status == '1' ? "ACTIVO" : "INACTIVO"
            }
        };
    }
}

public class CreateParameterDetailCommand: IRequest<ProcessResult<ParameterDetailResponse>>
{
    public int iMaeParameter { get; set; }
    public string cNombre { get; set; }
    public int iCodUsuarioRegistro { get; set; }
}