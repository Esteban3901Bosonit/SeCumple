using MediatR;
using SeCumple.Application.Components.Assignments.Dtos;
using SeCumple.Application.Dtos.Request;
using SeCumple.Application.Dtos.Response;
using SeCumple.Application.Specifications;
using SeCumple.Application.Specifications.Assignments;
using SeCumple.Domain.Entities;
using SeCumple.Infrastructure.Persistence.Interfaces;

namespace SeCumple.Application.Components.Assignments.Queries.ListAssignment;

public class ListAssignmentsQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<ListAssignmentsQuery, ProcessResult<PaginationResponse<AssignmentResponse>>>
{
    public async Task<ProcessResult<PaginationResponse<AssignmentResponse>>> Handle(ListAssignmentsQuery request,
        CancellationToken cancellationToken)
    {
        var assignmentSpecParams = new SpecificationParams
        {
            PageIndex = request.PageIndex,
            PageSize = request.PageSize,
            Filters = request.Filters,
            Sort = request.Sort,
        };

        var assignmentSpec = new AssignmentSpecification(assignmentSpecParams);
        var assignments = await unitOfWork.Repository<Assignment>().GetAllWithSpec(assignmentSpec);

        var specCount = new SectorForCountingSpecification(assignmentSpecParams);
        var totalAssignments = await unitOfWork.Repository<Assignment>().CountAsync(specCount);

        var rounded = Math.Ceiling(Convert.ToDecimal(totalAssignments) / Convert.ToDecimal(request.PageSize));
        var totalPages = Convert.ToInt32(rounded);

        var assignmentResponse = assignments.Select(d => new AssignmentResponse
        {
            iMovAsignacion = d.Id,
            cSectorNombre = d.Sector!.Name,
            iRol = d.RoleId,
            iCodUsuario= d.UserId,
            cEstado = d.Status == '1' ? "ACTIVO" : "INACTIVO"
        });

        return new ProcessResult<PaginationResponse<AssignmentResponse>>
        {
            Data = new PaginationResponse<AssignmentResponse>
            {
                Count = totalAssignments,
                Data = assignmentResponse.ToList(),
                PageCount = totalPages,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                ResultByPage = assignments.Count
            }
        };
    }
}

public class ListAssignmentsQuery : PaginationRequest,
    IRequest<ProcessResult<PaginationResponse<AssignmentResponse>>>
{
}