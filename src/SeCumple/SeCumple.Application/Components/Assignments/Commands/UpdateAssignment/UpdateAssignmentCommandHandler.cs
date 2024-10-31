using MediatR;
using SeCumple.Application.Components.Assignments.Dtos;
using SeCumple.Application.Dtos.Response;
using SeCumple.Domain.Entities;
using SeCumple.Infrastructure.Persistence.Interfaces;

namespace SeCumple.Application.Components.Assignments.Commands.UpdateAssignment;

public class UpdateAssignmentCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateAssignmentCommand, ProcessResult<AssignmentResponse>>
{
    public async Task<ProcessResult<AssignmentResponse>> Handle(UpdateAssignmentCommand request,
        CancellationToken cancellationToken)
    {
        var assignment = await unitOfWork.Repository<Assignment>().GetByIdAsync(request.iMovAsignacion);

        assignment.PlanId = request.iDetPlanCumplimiento;
        assignment.Username = request.cUserName;
        assignment.UserId = request.iCodUsuario;
        assignment.RoleId = request.iRol;
        assignment.SectorId = request.iMaeSector;
        assignment.ModifiedBy = request.iCodUsuarioRegistro;

        await unitOfWork.Repository<Assignment>().UpdateAsync(assignment);

        return new ProcessResult<AssignmentResponse>
        {
            Data = new AssignmentResponse
            {
                iMovAsignacion = assignment.Id,
                iMaeSector = assignment.SectorId,
                iRol = assignment.RoleId,
                iCodUsuario = assignment.UserId,
                cEstado = assignment.Status == '1' ? "ACTIVO" : "INACTIVO"
            }
        };
    }
}

public class UpdateAssignmentCommand : IRequest<ProcessResult<AssignmentResponse>>
{
    public int iMovAsignacion { get; set; }
    public int iDetPlanCumplimiento { get; set; }
    public int iMaeSector { get; set; }
    public int iCodUsuario { get; set; }
    public int iRol { get; set; }
    public int iCodUsuarioRegistro { get; set; }
    public string cUserName { get; set; }
}