namespace SeCumple.Application.Dtos.Response;

public class ProcessResult<T> where T : class
{
    public bool IsSuccess { get; set; } = true;
    public T? Data { get; set; }
    public string[]? Message { get; set; }
}