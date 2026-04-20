using System.Text.Json.Serialization;

namespace Backend.Models.Dto;

public class ConditionDto
{
    [JsonPropertyName("text")]
    public required string Text { get; set; }
    
    [JsonPropertyName("icon")]
    public required string Icon { get; set; }
    
    [JsonPropertyName("code")]
    public int Code { get; set; }
}