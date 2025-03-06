using System.Text.Json.Serialization;

namespace Api.ReqRes.Models;

public class CreateUserRequest
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
    
    [JsonPropertyName("job")]
    public string Job { get; set; } = string.Empty;
}