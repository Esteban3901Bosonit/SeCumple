using System.Linq.Expressions;
using MediatR;
using SeCumple.Application.Components.Goals.Dtos;
using SeCumple.Application.Dtos.Response;
using SeCumple.Domain.Entities;
using SeCumple.Infrastructure.Persistence.Interfaces;

namespace SeCumple.Application.Components.Goals.Queries.ListGoals;

public class ListGoalsQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<ListGoalsQuery, ProcessResult<IReadOnlyList<PeriodResponse>>>
{
    public async Task<ProcessResult<IReadOnlyList<PeriodResponse>>> Handle(ListGoalsQuery request,
        CancellationToken cancellationToken)
    {
        var includes = new List<Expression<Func<Period, object>>>
        {
            x => x.Goals!
        };

        var parameterDetails = await unitOfWork.Repository<ParameterDetail>().GetAllAsync();
        
        var periods = await unitOfWork.Repository<Period>()
            .GetAsync(x => x.IndicatorId == request.iMovIndicadorPeriodo && x.Status == '1', null, includes);

        return new ProcessResult<IReadOnlyList<PeriodResponse>>
        {
            Data = periods.Select(s => new PeriodResponse
            {
                iMovIndicadorPeriodo = s.Id,
                iMovIndicador = s.IndicatorId,
                iDetPlanCumpAnio = s.PlanAnioId,
                iLineaBaseAnual = (int)s.AnnualBaseline,
                iMetaAnual = (int)s.AnnualGoal,
                iTipoMedicion = s.MeasureTypeId,
                iMaePeriodicidad = s.PeriodicityId,
                cEstado = s.Status,
                ListaMetas = s.Goals!.Select(g => new GoalResponse
                {
                    iMovMeta = g.Id,
                    iMeta = (int)g.FinalGoal!,
                    iMovIndicadorPeriodo = g.PeriodId,
                    iInformarMeta = (int)g.GoalValue,
                    iFlag = g.Flag,
                    iOrden = g.Order,
                    cEstado = g.Status,
                    iEstadoMeta = g.GoalStatusId,
                    cEstadoMeta = parameterDetails.FirstOrDefault(x=>x.Id==g.GoalStatusId)!.Name!,
                    iEstadoRealizado = g.DoneStatusId,
                    iEstadoObs = g.CheckedStatusId,
                    cObservado = g.Annotation,
                    cRespuesta = g.Answer,
                    cComentario = g.Comment
                }).ToList()
            }).ToList()
        };
    }
}

public class ListGoalsQuery : IRequest<ProcessResult<IReadOnlyList<PeriodResponse>>>
{
    public int iMovIndicadorPeriodo { get; set; }
}