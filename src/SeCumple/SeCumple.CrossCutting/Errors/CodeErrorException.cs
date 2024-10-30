using Newtonsoft.Json;

namespace SeCumple.CrossCutting.Errors;

public class CodeErrorException(int statusCode, string[]? messages = null, string? details = null)
    : CodeErrorResponse(statusCode, messages)
{
    [JsonProperty(PropertyName = "errorDetails")]
    public string? Details { get; set; } = details;

    [JsonProperty(PropertyName = "messages")]
    public new string[]? Messages { get; set; } = messages;
}