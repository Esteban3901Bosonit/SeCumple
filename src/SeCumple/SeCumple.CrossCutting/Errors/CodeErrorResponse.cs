using Newtonsoft.Json;

namespace SeCumple.CrossCutting.Errors;

public class CodeErrorResponse
{
    [JsonProperty(PropertyName = "statusCode")]
    public int StatusCode { get; set; }

    [JsonProperty(PropertyName = "messages")]
    public string[]? Messages { get; set; }

    public CodeErrorResponse(int statusCode, string[]? messages = null)
    {
        StatusCode = statusCode;
        if (messages != null) Messages = messages;
        Messages = new[] { GetDefaultMessageStatusCode(statusCode) };
    }

    private static string GetDefaultMessageStatusCode(int statusCode)
    {
        return statusCode switch
        {
            400 => "Bad Request",
            401 => "You're not authorized to this resource",
            404 => "Resource not found",
            500 => "Internal Server Error",
            _ => string.Empty
        };
    }
}