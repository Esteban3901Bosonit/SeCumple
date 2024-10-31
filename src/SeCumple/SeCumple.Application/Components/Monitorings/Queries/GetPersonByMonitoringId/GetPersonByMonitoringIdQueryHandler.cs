using MediatR;
using SeCumple.Application.Components.Alerts.Dtos;
using SeCumple.Application.Components.Monitorings.Dtos;
using SeCumple.Application.Dtos.Response;
using SeCumple.Domain.Entities;
using SeCumple.Infrastructure.Persistence.Interfaces;

namespace SeCumple.Application.Components.Monitorings.Queries.GetPersonByMonitoringId;

public class GetPersonByMonitoringIdQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetPersonByMonitoringIdQuery, ProcessResult<IReadOnlyList<PersonResponse>>>
{
    public async Task<ProcessResult<IReadOnlyList<PersonResponse>>> Handle(GetPersonByMonitoringIdQuery request,
        CancellationToken cancellationToken)
    {
        var recordDocument = await unitOfWork.Repository<RecordDocument>()
            .GetEntityAsync(x => x.MonitoringId == request.MonitoringId);
        var recordDocumentPerson = await unitOfWork.Repository<RecordDocumentParticipants>()
            .GetAsync(x => x.RecordDocumentId == recordDocument.Id);

        var persons = await unitOfWork.Repository<Person>()
            .GetAsync(x => recordDocumentPerson.Select(r => r.ParticipantId).ToList().Contains(x.Id));

        return new ProcessResult<IReadOnlyList<PersonResponse>>
        {
            Data = persons.Select(p => new PersonResponse
            {
                iMaePersona = p.Id,
                cNombre = p.Name,
                cEmail = p.Email,
                cCelular = p.PhoneNumber,
                cCargo = p.Role,
                cInstitucion = p.Institution,
                cOficina = p.Office
            }).ToList()
        };
    }
}

public class GetPersonByMonitoringIdQuery : IRequest<ProcessResult<IReadOnlyList<PersonResponse>>>
{
    public int MonitoringId { get; set; }
}