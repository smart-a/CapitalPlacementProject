namespace Capital.Placement.Api.Model;

public class ApplicationTemplate : BaseEntity
{
    public string CoverImagePath { get; set; } = string.Empty;
    public DefaultInfoOption Phone { get; set; } = new();
    public DefaultInfoOption Nationality { get; set; } = new();
    public DefaultInfoOption CurrentResident { get; set; } = new();
    public DefaultInfoOption IdNumber { get; set; } = new();
    public DefaultInfoOption DateOfBirth { get; set; } = new();
    public DefaultInfoOption Gender { get; set; } = new();
    
    public string PersonalInfo { get; set; } = null!;

    public DefaultProfileOption Education { get; set; } = new();
    public DefaultProfileOption Experience { get; set; } = new();
    public DefaultProfileOption Resume { get; set; } = new();
    
    public string Profile { get; set; } = null!;
    
    public string AdditionalQuestion { get; set; } = null!;
}

public class DefaultInfoOption
{
    public bool IsInternal { get; set; } = false;
    public bool Hide { get; set; } = false;
}

public class DefaultProfileOption
{
    public bool IsMandatory { get; set; } = false;
    public bool Hide { get; set; } = false;
}