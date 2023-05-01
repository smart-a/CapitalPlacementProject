using AutoMapper;
using Capital.Placement.Api.Helper;
using Capital.Placement.Api.Mapping;
using Capital.Placement.Api.Model;

namespace Capital.Placement.Api.Dto.ApplicationTemplate;

public class AppTemplateDto : IMapWith<Model.ApplicationTemplate>
{
    public Guid Id { get; set; }
    public string CoverImagePath { get; set; } = string.Empty;
    public DefaultInfoOption Phone { get; set; } = new();
    public DefaultInfoOption Nationality { get; set; } = new();
    public DefaultInfoOption CurrentResident { get; set; } = new();
    public DefaultInfoOption IdNumber { get; set; } = new();
    public DefaultInfoOption DateOfBirth { get; set; } = new();
    public DefaultInfoOption Gender { get; set; } = new();

    public List<QuestionDetails> MorePersonalInfo { get; set; } = null!;

    public DefaultProfileOption Education { get; set; } = new();
    public DefaultProfileOption Experience { get; set; } = new();
    public DefaultProfileOption Resume { get; set; } = new();
    
    public List<QuestionDetails> MoreProfileInfo { get; set; } = null!;
    
    public List<QuestionDetails> MoreAdditionalQuestion { get; set; } = null!;
    
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public static AppTemplateDto Parse(Model.ApplicationTemplate template, IMapper mapper)
    {
        var templateDto = mapper.Map<AppTemplateDto>(template);
        templateDto.MorePersonalInfo = template.PersonalInfo.ToObject<List<QuestionDetails>>();
        templateDto.MoreProfileInfo = template.Profile.ToObject<List<QuestionDetails>>();
        templateDto.MoreAdditionalQuestion = template.AdditionalQuestion.ToObject<List<QuestionDetails>>();
        return templateDto;
    }
    
}