using Newtonsoft.Json;

namespace Capital.Placement.Api.Model;

public class BaseEntity
{
    [JsonProperty(PropertyName = "id")]
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}