using Capital.Placement.Api.Enums;
using Capital.Placement.Api.Mapping;
using Capital.Placement.Api.Model;

namespace Capital.Placement.Api.Dto.Workflow;

public class UpdateWorkflowDto : IMapWith<Model.Workflow>
{
    public List<StageDto> Stages { get; set; } = null!;
}

public class StageDto : IMapWith<Stage>
{
    public string StageName { get; set; } = string.Empty;
    public string StageType { get; set; } = string.Empty;
    public List<GenericOptionStruct> StageTypeOptions { get; set; } = null!;
}