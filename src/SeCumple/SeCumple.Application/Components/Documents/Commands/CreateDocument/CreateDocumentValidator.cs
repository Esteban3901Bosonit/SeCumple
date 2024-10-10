using FluentValidation;

namespace SeCumple.Application.Components.Documents.Commands.CreateDocument;

public class CreateDocumentValidator: AbstractValidator<CreateDocumentCommand>
{
    public CreateDocumentValidator()
    {
            RuleFor(x => x.UserId)
            .GreaterThan(0).WithMessage("UserId is required");
        
        RuleFor(x => x.DocumentCode)
            .NotEmpty().WithMessage("DocumentCode is required")
            .NotNull().WithMessage("DocumentCode is required");

        RuleFor(x => x.DocumentTypeId)
            .GreaterThan(0).WithMessage("DocumentTypeId is required");
        
        RuleFor(x =>x.DocumentDate)
            .NotNull().WithMessage("DocumentDate is required")
            .GreaterThan(DateTime.Now);
    }
}