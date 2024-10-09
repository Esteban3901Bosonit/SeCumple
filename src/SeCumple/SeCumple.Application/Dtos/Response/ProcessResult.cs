namespace SeCumple.Application.Dtos.Response;

public class ProcessResult<T> where T : class
{
    public bool IsSuccess { get; set; } = true;
    public T? Result { get; set; }
    public string[]? Messages { get; set; }
}