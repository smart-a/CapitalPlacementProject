using System.ComponentModel.DataAnnotations.Schema;
using Capital.Placement.Api.Dto.Workflow;
using Capital.Placement.Api.Enums;
using Capital.Placement.Api.Helper;

namespace Capital.Placement.Api.Model;

public class Workflow : BaseEntity
{
    public Workflow()
    {
    }
    public Workflow(List<StageDto> stages)
    {
        Stages = stages.Select(s => new Stage
        {
            StageName = s.StageName,
            StageType =  Enum.Parse<StageType>(s.StageType),
            StageOptions = s.StageTypeOptions.ToJsonString()
        }).ToList();
    }
    public List<Stage> Stages { get; set; } = null!;
}

public class Stage
{
    public string StageName { get; set; } = string.Empty;
    [Column(TypeName = "varchar(15)")]
    public StageType StageType { get; set; }
    public string StageOptions { get; set; } = null!;
}
