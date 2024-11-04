using MediatR;
using SeCumple.Application.Components.Monitorings.Dtos;
using SeCumple.Domain.Entities;
using SeCumple.Infrastructure.Persistence.Interfaces;

namespace SeCumple.Application.Components.Monitorings.Commads.RegisterRecordDocument;

public class RegisterRecordDocumentCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<RegisterRecordDocumentCommand, string>
{
    public async Task<string> Handle(RegisterRecordDocumentCommand request, CancellationToken cancellationToken)
    {
        var elementsByRecordId = await unitOfWork.Repository<RecordDocumentElement>()
            .GetAsync(x => x.RecordDocumentId == request.iDetActa);

        var elementsToDelete = elementsByRecordId
            .Where(x => !request.oElementosActa.Select(y => y.iDetElementoActa).Contains(x.Id));

        var elementsToAdd = request.oElementosActa.Where(x => x.iDetElementoActa is null or < 1);

        var elementsToEdit = elementsByRecordId.Where(x => request.oElementosActa.Where(e => e.iDetElementoActa > 1)
            .Select(y => y.iDetElementoActa)
            .Contains(x.Id));

        foreach (var element in elementsToDelete)
        {
            element.Status = '0';
            element.ModifiedBy = request.iCodUsuarioRegistro;

            await unitOfWork.Repository<RecordDocumentElement>().UpdateAsync(element);
        }

        foreach (var element in elementsToEdit)
        {
            var elementRequest = request.oElementosActa.First(x => x.iDetElementoActa == element.Id);

            element.Numeral = elementRequest.iNumeral;
            element.RecordDocumentElementTypeId = elementRequest.iTipoElementoActa;
            element.Content = elementRequest.cTema;
            element.ModifiedBy = request.iCodUsuarioRegistro;

            await unitOfWork.Repository<RecordDocumentElement>().UpdateAsync(element);
        }

        var elementsAdd = elementsToAdd
            .Select(x => new RecordDocumentElement
            {
                RecordDocumentId = request.iDetActa,
                Numeral = x.iNumeral,
                RecordDocumentElementTypeId = x.iTipoElementoActa,
                Content = x.cTema,
                CreatedBy = request.iCodUsuarioRegistro
            });

        unitOfWork.Repository<RecordDocumentElement>().AddRange(elementsAdd.ToList());


        throw new NotImplementedException();
    }
}

public class RegisterRecordDocumentCommand : IRequest<string>
{
    public int iDetActa { get; set; }
    public IReadOnlyList<RecordDocumentRequest> oElementosActa { get; set; }
    public int iCodUsuarioRegistro { get; set; }
}