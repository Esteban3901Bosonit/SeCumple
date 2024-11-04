using MediatR;
using SeCumple.Application.Components.Monitorings.Dtos;
using SeCumple.Application.Dtos.Response;
using SeCumple.Domain.Entities;
using SeCumple.Infrastructure.Persistence.Interfaces;

namespace SeCumple.Application.Components.Monitorings.Commads.AddPersonsToMonitoring;

public class
    AddPersonsToMonitoringCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<AddPersonsToMonitoringCommand,
    ProcessResult<IReadOnlyList<PersonResponse>>>
{
    public async Task<ProcessResult<IReadOnlyList<PersonResponse>>> Handle(AddPersonsToMonitoringCommand request,
        CancellationToken cancellationToken)
    {
        var personsToAdd = new List<Person>();

        foreach (var participant in request.Persons)
        {
            var person = await unitOfWork.Repository<Person>().GetEntityAsync(
                x => x.Name == participant.cNombre && x.Institution == participant.cInstitucion);

            if (person == null)
            {
                person = new Person
                {
                    Name = participant.cNombre,
                    Email = participant.cEmail,
                    PhoneNumber = participant.cCelular,
                    Role = participant.cCargo,
                    Institution = participant.cInstitucion,
                    Office = participant.cOficina,
                    CreatedBy = request.iCodUsuarioRegistro
                };

                await unitOfWork.Repository<Person>().AddAsync(person);
            }

            personsToAdd.Add(person);
        }

        var records = personsToAdd.Select(p => new RecordDocumentParticipants
        {
            ParticipantId = p.Id,
            RecordDocumentId = request.iDetActa,
            Attended = false,
            CreatedBy = request.iCodUsuarioRegistro
        }).ToList();

        unitOfWork.Repository<RecordDocumentParticipants>().AddRange(records);

        var personResponse = personsToAdd.Select(x => new PersonResponse
        {
            iMaePersona = x.Id,
            cNombre = x.Name,
            cEmail = x.Email,
            cCelular = x.PhoneNumber,
            cCargo = x.Role,
            cInstitucion = x.Institution,
            cOficina = x.Office
        }).ToList();

        return new ProcessResult<IReadOnlyList<PersonResponse>>
        {
            Data = personResponse
        };
    }
}

public class AddPersonsToMonitoringCommand : IRequest<ProcessResult<IReadOnlyList<PersonResponse>>>
{
    public int iDetActa { get; set; }
    public List<PersonRequest> Persons { get; set; }
    public int iCodUsuarioRegistro { get; set; }
}