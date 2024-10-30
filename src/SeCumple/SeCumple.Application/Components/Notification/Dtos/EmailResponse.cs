namespace SeCumple.Application.Components.Notification.Dtos;

public class EmailResponse
{
    public bool Sent { get; set; } = true;
    public string? Message { get; set; }
}