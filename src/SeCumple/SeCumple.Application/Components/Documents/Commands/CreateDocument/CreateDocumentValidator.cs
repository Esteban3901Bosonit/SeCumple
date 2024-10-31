using FluentValidation;

namespace SeCumple.Application.Components.Documents.Commands.CreateDocument;

public class CreateDocumentValidator: AbstractValidator<CreateDocumentCommand>
{
    public CreateDocumentValidator()
    {
            RuleFor(x => x.iCodUsuarioRegistro)
            .GreaterThan(0).WithMessage("UserId is required");
        
        RuleFor(x => x.cNumDispositivo)
            .NotEmpty().WithMessage("DocumentCode is required")
            .NotNull().WithMessage("DocumentCode is required");

        RuleFor(x => x.iTipoDispositivo)
            .GreaterThan(0).WithMessage("DocumentTypeId is required");
        
        RuleFor(x =>x.dFechaDispositivo)
            .NotNull().WithMessage("DocumentDate is required")
            .GreaterThan(DateTime.Now);
    }
}