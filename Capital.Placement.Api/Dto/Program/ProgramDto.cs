using Capital.Placement.Api.Mapping;

namespace Capital.Placement.Api.Dto.Program;

public class ProgramDto : IMapWith<Capital.Placement.Api.Model.Program>
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Summary { get; set; }
    public string Description { get; set; } = string.Empty;
    public List<string>? RequiredSkills { get; set; }
    public string? Benefit { get; set; }
    public string? Criteria { get; set; }
    public string ProgramType { get; set; } = string.Empty;
    public DateTime? ProgramState { get; set; }
    public DateTime ApplicationOpen { get; set; }
    public DateTime ApplicationClose { get; set; }
    public int? Duration { get; set; }
    public string Location { get; set; } = string.Empty;
    public string? MinimumQualification { get; set; }
    public int? MaximumApplication { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}