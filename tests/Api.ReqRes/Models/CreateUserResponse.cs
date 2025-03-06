using System.Text.Json.Serialization;

namespace Api.ReqRes.Models;

public class CreateUserResponse
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
    
    [JsonPropertyName("job")]
    public string Job { get; set; } = string.Empty;
    
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;
    
    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; set; }
}