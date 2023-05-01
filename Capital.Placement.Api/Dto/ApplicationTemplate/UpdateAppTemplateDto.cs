using AutoMapper;
using Capital.Placement.Api.Enums;
using Capital.Placement.Api.Helper;
using Capital.Placement.Api.Mapping;
using Capital.Placement.Api.Model;

namespace Capital.Placement.Api.Dto.ApplicationTemplate;

public class UpdateAppTemplateDto : IMapWith<Model.ApplicationTemplate>
{
    public string CoverImagePath { get; set; } = string.Empty;
    public DefaultInfoOption Phone { get; set; } = new();
    public DefaultInfoOption Nationality { get; set; } = new();
    public DefaultInfoOption CurrentResident { get; set; } = new();
    public DefaultInfoOption IdNumber { get; set; } = new();
    public DefaultInfoOption DateOfBirth { get; set; } = new();
    public DefaultInfoOption Gender { get; set; } = new();
    
    public List<QuestionDetails> PersonalInfo { get; set; } = null!;

    public DefaultProfileOption Education { get; set; } = new();
    public DefaultProfileOption Experience { get; set; } = new();
    public DefaultProfileOption Resume { get; set; } = new();
    
    public List<QuestionDetails> Profile { get; set; } = null!;
    
    public List<QuestionDetails> AdditionalQuestion { get; set; } = null!;

    public Model.ApplicationTemplate ToAppTemplate(IMapper mapper)
    {
        var template = mapper.Map<Model.ApplicationTemplate>(this);
        template.PersonalInfo = PersonalInfo.ToJsonString();
        template.Profile = Profile.ToJsonString();
        template.AdditionalQuestion = AdditionalQuestion.ToJsonString();
        
        template.Id = Guid.NewGuid();
        template.CreatedAt = DateTime.Now;
        return template;
    }
}

public class QuestionDetails
{
    public string QuestionType { get; set; } = string.Empty;
    public string Question { get; set; } = string.Empty;
    public List<GenericOptionStruct> QuestionOption { get; set; } = null!;
}