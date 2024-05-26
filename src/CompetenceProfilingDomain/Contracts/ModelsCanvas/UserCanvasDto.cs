using Newtonsoft.Json;

namespace CompetenceProfilingDomain.Contracts.ModelsCanvas;

public class UserCanvasDto
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("name")] public string Name { get; set; } = "";

    // [JsonProperty("created_at")]
    // public DateTime CreatedAt { get; set; }
    //
    // [JsonProperty("sortable_name")] public string SortableName { get; set; } = "";
    //
    // [JsonProperty("short_name")] public string ShortName { get; set; } = "";
    //
    // [JsonProperty("sis_user_id")] public string SisUserId { get; set; } = "";
    
    // [JsonProperty("integration_id")] public object IntegrationId { get; set; } = "";
    //
    // [JsonProperty("login_id")] public string LoginId { get; set; } = "";
    //
    // [JsonProperty("email")] public string Email { get; set; } = "";
}