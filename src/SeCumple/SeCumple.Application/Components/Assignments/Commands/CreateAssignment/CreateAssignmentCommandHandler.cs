using MediatR;
using SeCumple.Application.Components.Assignments.Dtos;
using SeCumple.Application.Dtos.Response;
using SeCumple.Domain.Entities;
using SeCumple.Infrastructure.Persistence.Interfaces;

namespace SeCumple.Application.Components.Assignments.Commands.CreateAssignment;

public class CreateAssignmentCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<CreateAssignmentCommand, ProcessResult<List<AssignmentResponse>>>
{
    public async Task<ProcessResult<List<AssignmentResponse>>> Handle(CreateAssignmentCommand request,
        CancellationToken cancellationToken)
    {
        var response = new ProcessResult<List<AssignmentResponse>>
        {
            Data = new List<AssignmentResponse>()
        };

        var assignmentTasks = new List<Task>();

        foreach (var planId in request.iDetPlanCumplimiento)
        {
            foreach (var sectorId in request.iMaeSector)
            {
                var assignment = new Assignment
                {
                    UserId = request.iCodUsuario,
                    RoleId = request.iRol,
                    PlanId = planId,
                    SectorId = sectorId,
                    Username = request.cUserName,
                    Status = '1',
                    CreatedBy = request.iCodUsuarioRegistro
                };

                assignmentTasks.Add(Task.Run(async () =>
                {
                    await unitOfWork.Repository<Assignment>().AddAsync(assignment);
                    response.Data.Add(new AssignmentResponse
                    {
                        iDetPlanCumplimiento = assignment.PlanId,
                        iMaeSector = assignment.SectorId,
                        iRol = assignment.RoleId,
                        iCodUsuario = assignment.UserId,
                        cUserName = assignment.Username
                    });
                }));
            }
        }
        await Task.WhenAll(assignmentTasks);

        return response;
    }
}

public class CreateAssignmentCommand : IRequest<ProcessResult<List<AssignmentResponse>>>
{
    public int[] iDetPlanCumplimiento { get; set; }
    public int[] iMaeSector { get; set; }
    public int iCodUsuario { get; set; }
    public int iRol { get; set; }
    public int iCodUsuarioRegistro { get; set; }
    public string? cUserName { get; set; }
}