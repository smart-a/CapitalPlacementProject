using AutoMapper;
using Capital.Placement.Api.Dto.ApplicationTemplate;
using Capital.Placement.Api.Dto.Workflow;
using Capital.Placement.Api.Mapping;

namespace Capital.Placement.Api.Dto.Program;

public class ProgramDetailsDto : IMapWith<Capital.Placement.Api.Model.Program>
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
    
    
    // Relationship
    public AppTemplateDto ApplicationTemplates { get; set; } = null!;
    public WorkflowDto Workflows { get; set; } = null!;
    
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    
    public static ProgramDetailsDto Parse(Capital.Placement.Api.Model.Program program, IMapper mapper)
    {
        var programDetailsDto = mapper.Map<ProgramDetailsDto>(program);
        programDetailsDto.ApplicationTemplates = AppTemplateDto.Parse(program.ApplicationTemplate, mapper);
        programDetailsDto.Workflows = WorkflowDto.Parse(program.Workflow);
        return programDetailsDto;
    }
}