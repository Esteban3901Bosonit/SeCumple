namespace SeCumple.Application.Components.Documents.Dtos;

public class DocumentResponse
{
    public int iMaeDispositivo { get; set; }
    public string? cNumDispositivo { get; set; }
    public DateTime? dFechaDispositivo { get; set; }
    public int iTipoDispositivo { get; set; }
    public string? DocumentType { get; set; }
    public string? cLink { get; set; } 
    public string? cEstado { get; set; }
}