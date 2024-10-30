namespace SeCumple.Application.Components.Assignments.Dtos;

public class AssignmentResponse
{
    public int iDetPlanCumplimiento;
    public string? cNombrePlan { get; set; }
    public int iMovAsignacion { get; set; }
    public int iCodUsuario { get; set; }
    public int iMaeSector { get; set; }
    public int iMaeUnidadOrganica { get; set; }
    public int iRol { get; set; }   
    public string? cSectorNombre { get; set; }
    public string? cSectorSigla { get; set; }
    public string? cSectorDesCorta { get; set; }
    public string? cEstado { get; set; }
    public string? cUnidadOrgNombre { get; set; }
    public string? cUnidadOrgSigla { get; set; }
    public string? cUserName { get; set; }
}