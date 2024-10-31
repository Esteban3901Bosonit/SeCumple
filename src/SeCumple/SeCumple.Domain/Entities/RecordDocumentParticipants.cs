using SeCumple.CrossCutting.Entities;

namespace SeCumple.Domain.Entities;

public class RecordDocumentParticipants: Base
{
    public int RecordDocumentId { get; set; }
    public int ParticipantId { get; set; }
    public bool Attended { get; set; } = false;
}