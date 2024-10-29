using FluentValidation;

namespace SeCumple.Application.Components.Documents.Commands.UpdateDocument
{
    public class UpdateDocumentValidator: AbstractValidator<UpdateDocumentCommand>
    {
        public UpdateDocumentValidator()
        {
            RuleFor(x=>x.Id)
                .GreaterThan(0).WithMessage("DocumentId is required");
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
}
