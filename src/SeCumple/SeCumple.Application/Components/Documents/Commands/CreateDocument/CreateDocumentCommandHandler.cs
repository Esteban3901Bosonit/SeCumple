using MediatR;
using SeCumple.Application.Components.Documents.Dtos;

namespace SeCumple.Application.Components.Documents.Commands.CreateDocument;

public class CreateDocumentCommandHandler: IRequestHandler<CreateDocumentCommand, DocumentResponse>
{
    public Task<DocumentResponse> Handle(CreateDocumentCommand request, CancellationToken cancellationToken)
    {

        return null!;
    }
}

public class CreateDocumentCommand: IRequest<DocumentResponse>
{
    
}