using MediatR;
using SeCumple.Application.Components.Assignments.Dtos;
using SeCumple.Application.Dtos.Response;
using SeCumple.Domain.Entities;
using SeCumple.Infrastructure.Persistence.Interfaces;

namespace SeCumple.Application.Components.Assignments.Commands.DeleteAssignment;

public class DeleteAssignmentCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteAssignmentCommand, ProcessResult<AssignmentResponse>>
{
    public async Task<ProcessResult<AssignmentResponse>> Handle(DeleteAssignmentCommand request,
        CancellationToken cancellationToken)
    {
        var assignment = await unitOfWork.Repository<Assignment>().GetByIdAsync(request.iMovAsignacion);
        assignment.Status = '0';
        assignment.ModifiedBy = request.iCodUsuarioRegistro;

        await unitOfWork.Repository<Assignment>().UpdateAsync(assignment);

        return new ProcessResult<AssignmentResponse>
        {
            Data = new AssignmentResponse
            {
                iMovAsignacion = assignment.Id!,
                iMaeSector = assignment.SectorId,
                cEstado = assignment.Status == '1' ? "ACTIVO" : "INACTIVO"
            }
        };
    }
}

public abstract class DeleteAssignmentCommand : IRequest<ProcessResult<AssignmentResponse>>
{
    public int iMovAsignacion { get; set; }
    public int iCodUsuarioRegistro { get; set; }
}