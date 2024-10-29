using System.ComponentModel.DataAnnotations.Schema;
using SeCumple.CrossCutting.Entities;

namespace SeCumple.Domain.Entities;

[Table("MovIndicador")]
public class Indicator : Base
{
    [Column("iMovIndicador")]
    public int Id { get; set; }
    [Column("cNombre")]
    public string? Name { get; set; }      
    [Column("cDescripcion")]
    public string? Description { get; set; }
    [Column("iUnidadMedida")]
    public int MeasureUnit { get; set; }
    [Column("iMovIntervencion")]
    public int InterventionId { get; set; }
    public virtual Intervention? Intervention { get; set; }
    [Column("iTipoIndicador")]
    public int IndicatorTypeId { get; set; }
    public virtual ParameterDetail? IndicatorType { get; set; }
    [Column("cAccion")]
    public string? Action { get; set; }
    [Column("iMovIndicadorPadre")]
    public int? ParentId { get; set; }
    [Column("iEstadoIndicador")]
    public int IndicatorStatusId { get; set; }
}