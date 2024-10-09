namespace SeCumple.Application.Components.Documents.Dtos;

public class DocumentResponse
{
    public int Id { get; set; }
    public string? DocumentCode { get; set; }
    public DateTime? DocumentDate { get; set; }
    public int DocumentTypeId { get; set; }
    public string? DocumentType { get; set; }
    public string? Url { get; set; } 
    public string? Active { get; set; }
}