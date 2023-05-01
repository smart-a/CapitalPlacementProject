using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Capital.Placement.Api.Enums;

namespace Capital.Placement.Api.Model;

public class Program : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    [MaxLength(250)]
    public string? Summary { get; set; }
    public string Description { get; set; } = string.Empty;
    public List<string>? RequiredSkills { get; set; }
    public string? Benefit { get; set; }
    public string? Criteria { get; set; }

    [Column(TypeName = "varchar(20)")]
    public ProgramType ProgramType { get; set; }
    public DateTime? ProgramState { get; set; }
    public DateTime ApplicationOpen { get; set; }
    public DateTime ApplicationClose { get; set; }
    public int? Duration { get; set; }
    public string Location { get; set; } = string.Empty;
    
    [Column(TypeName = "varchar(20)")]
    public Qualification? MinimumQualification { get; set; }
    public int? MaximumApplication { get; set; }
    
    // Relationship
    public ApplicationTemplate ApplicationTemplate { get; set; } = null!;
    public Workflow Workflow { get; set; } = null!;
}