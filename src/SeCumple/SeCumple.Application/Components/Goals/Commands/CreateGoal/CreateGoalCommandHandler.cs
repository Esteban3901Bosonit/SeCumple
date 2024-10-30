using MediatR;
using SeCumple.Application.Components.Goals.Dtos;
using SeCumple.Application.Dtos.Response;
using SeCumple.Domain.Entities;
using SeCumple.Infrastructure.Persistence.Interfaces;

namespace SeCumple.Application.Components.Goals.Commands.CreateGoal;

public class CreateGoalCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<CreateGoalCommand, ProcessResult<string>>
{
    public async Task<ProcessResult<string>> Handle(CreateGoalCommand request,
        CancellationToken cancellationToken)
    {
        var indicatorPeriod = new Period
        {
            IndicatorId = request.iMovIndicador,
            PlanAnioId = request.iDetPlanCumpAnio,
            AnnualBaseline = request.iLineaBaseAnual,
            AnnualGoal = request.iMetaAnual,
            MeasureTypeId = request.iTipoMedicion,
            PeriodicityId = request.iMaePeriodicidad,
            CreatedBy = request.iCodUsuarioRegistro
        };

        await unitOfWork.Repository<Period>().AddAsync(indicatorPeriod);

        var goalCount = 1;
        foreach (var meta in request.ListaMetas!)
        {
            var goal = new Goal
            {
                PeriodId = indicatorPeriod.Id,
                GoalValue = meta.iMeta,
                Order = goalCount,
                CreatedBy = request.iCodUsuarioRegistro
            };

            await unitOfWork.Repository<Goal>().AddAsync(goal);
            goalCount++;
        }

        return new ProcessResult<string>
        {
            Data = "Metas creadas"
        };
    }
}

public class CreateGoalCommand : IRequest<ProcessResult<string>>
{
    public int iMovIndicador { get; set; }
    public int iDetPlanCumpAnio { get; set; }
    public int iLineaBaseAnual { get; set; }
    public int iTipoMedicion { get; set; }
    public int iMetaAnual { get; set; }
    public int iMaePeriodicidad { get; set; }
    public int? indicador { get; set; }
    public List<GoalRequest>? ListaMetas { get; set; }
    public int iCodUsuarioRegistro { get; set; }
}