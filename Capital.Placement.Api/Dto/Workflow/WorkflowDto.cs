using Capital.Placement.Api.Enums;
using Capital.Placement.Api.Helper;
using Capital.Placement.Api.Mapping;
using Capital.Placement.Api.Model;

namespace Capital.Placement.Api.Dto.Workflow;

public class WorkflowDto : IMapWith<Model.Workflow>
{
    public Guid Id { get; set; }
    public List<StageDto> Stages { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public static WorkflowDto Parse(Model.Workflow workflow)
    {
        return new WorkflowDto
        {
            Id = workflow.Id,
            Stages = StagesToDto(workflow.Stages),
            CreatedAt = workflow.CreatedAt,
            UpdatedAt = workflow.UpdatedAt
        };
    }
    
    private static List<StageDto> StagesToDto(List<Stage> stages)
    {
        return stages.Select(s => new StageDto
        {
            StageName = s.StageName,
            StageType = s.StageType.ToString(),
            StageTypeOptions = s.StageOptions.ToObject<List<GenericOptionStruct>>()
        }).ToList();
    }
}