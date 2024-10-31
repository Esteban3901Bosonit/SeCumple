using FluentValidation;

namespace SeCumple.Application.Components.Documents.Commands.UpdateDocument
{
    public class UpdateDocumentValidator: AbstractValidator<UpdateDocumentCommand>
    {
        public UpdateDocumentValidator()
        {
            RuleFor(x=>x.iMaeDispositivo)
                .GreaterThan(0).WithMessage("DocumentId is required");
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
}
