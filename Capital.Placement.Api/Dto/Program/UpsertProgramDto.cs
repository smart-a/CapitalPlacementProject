using System.ComponentModel.DataAnnotations;
using Capital.Placement.Api.Mapping;

namespace Capital.Placement.Api.Dto.Program;

public class UpsertProgramDto : IMapWith<Capital.Placement.Api.Model.Program>
{
    public string Title { get; set; } = string.Empty;
    [MaxLength(250)]
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
}