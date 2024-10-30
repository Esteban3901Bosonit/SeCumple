using MediatR;
using SeCumple.Application.Components.Assignments.Dtos;
using SeCumple.Application.Dtos.Response;
using SeCumple.Domain.Entities;
using SeCumple.Infrastructure.Persistence.Interfaces;

namespace SeCumple.Application.Components.Assignments.Commands.CreateAssignment;

public class CreateAssignmentCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<CreateAssignmentCommand, ProcessResult<AssignmentResponse>>
{
    public async Task<ProcessResult<AssignmentResponse>> Handle(CreateAssignmentCommand request,
        CancellationToken cancellationToken)
    {
        var assignment = new Assignment
        {
            UserId = request.iCodUsuario,
            RoleId = request.iRol,
            PlanId = request.iDetPlanCumplimiento,
            SectorId = request.iMaeSector,
            Username = request.cUserName,
            Status = '1',
            CreatedBy = request.iCodUsuarioRegistro
        };

        await unitOfWork.Repository<Assignment>().AddAsync(assignment);

        return new ProcessResult<AssignmentResponse>
        {
            Data = new AssignmentResponse
            {
                iMovAsignacion = assignment.Id,
                iMaeSector = assignment.SectorId,
                iRol = assignment.RoleId,
                iDetPlanCumplimiento = assignment.PlanId,
                cEstado = assignment.Status == '1' ? "ACTIVO" : "INACTIVO"
            }
        };
    }
}

public class CreateAssignmentCommand : IRequest<ProcessResult<AssignmentResponse>>
{
    public int iDetPlanCumplimiento { get; set; }
    public int iMaeSector { get; set; }
    public int iCodUsuario { get; set; }
    public int iRol { get; set; }
    public int iCodUsuarioRegistro { get; set; }
    public string cUserName { get; set; }
}