using MediatR;
using Microsoft.AspNetCore.Http;
using SeCumple.Application.Components.Plans.Dtos;
using SeCumple.Application.Dtos.Response;
using SeCumple.Application.Interfaces;
using SeCumple.CrossCutting.Utilities;
using SeCumple.Domain.Entities;
using SeCumple.Infrastructure.Persistence.Interfaces;

namespace SeCumple.Application.Components.Plans.Commands.ApprovePlan;

public class UpdatePlanStatusCommandHandler(IUnitOfWork unitOfWork, IFileManagement fileManagement)
    : IRequestHandler<UpdatePlanStatusCommand, ProcessResult<PlanResponse>>
{
    public async Task<ProcessResult<PlanResponse>> Handle(UpdatePlanStatusCommand request,
        CancellationToken cancellationToken)
    {
        var plan = await unitOfWork.Repository<Plan>().GetByIdAsync(request.iDetPlanCumplimiento);

        switch (request.cNombreEstado)
        {
            case "EN ELABORACION":
                plan.PlanStatusId = 1; ////TODO: crear enum plan staud y asociar aprobado

                plan.Annotation = request.cObservacion;
                break;
            case "APROBADO":
                plan.PlanStatusId = 2; //TODO: crear enum plan staud y asociar aprobado

                if (request.fArchivo != null)
                {
                    var uploadedFile = await fileManagement.UploadFile(request.fArchivo);

                    var file = new FileUploaded
                    {
                        Name = uploadedFile.Name,
                        FileExtension = uploadedFile.Extension,
                        Size = $"{request.fArchivo!.Length / 1024:F2} KB",
                        CreatedBy = request.iCodUsuarioRegistro,
                        Location = uploadedFile.Url,
                        FileSignature = uploadedFile.Signature
                    };

                    var shaFile = await FileUtiilies.GetFileSha256HashAsync(request.fArchivo);

                    var fileExists = await unitOfWork.Repository<FileUploaded>()
                        .GetEntityAsync(x => x.FileSignature == shaFile);
                    
                    if (fileExists != null)
                    {
                        file = fileExists;
                    }
                    else
                    {
                        await unitOfWork.Repository<FileUploaded>().AddAsync(file);
                    }

                    var code = (await unitOfWork.Repository<Plan>().GetAllAsync())
                        .LastOrDefault(x => x.ApprovalDocumentCode != null)?.ApprovalDocumentCode;

                    plan.ApprovalFileId = file.Id;
                    plan.ApprovalDocumentCode = plan.ApprovalDocumentCode ??
                        (string.IsNullOrEmpty(code) ? "0001" : (int.Parse(code) + 1).ToString("D4"));
                }

                break;
            default:
                plan.PlanStatusId = 3; ////TODO: crear enum plan staud y asociar el buscado
                break;
        }

        await unitOfWork.Repository<Plan>().UpdateAsync(plan);

        return new ProcessResult<PlanResponse>
        {
            Data = new PlanResponse
            {
                iDetPlanCumplimiento = plan.Id,
                cNombreEstado = request.cNombreEstado,
            }
        };
    }
}

public class UpdatePlanStatusCommand : IRequest<ProcessResult<PlanResponse>>
{
    public int iDetPlanCumplimiento { get; set; }
    public string cNombreEstado { get; set; }
    public string? cObservacion { get; set; }
    public IFormFile? fArchivo { get; set; }
    public int iCodUsuarioRegistro { get; set; }
}