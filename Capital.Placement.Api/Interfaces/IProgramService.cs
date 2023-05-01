using Capital.Placement.Api.Dto.ApplicationTemplate;
using Capital.Placement.Api.Dto.Program;
using Capital.Placement.Api.Dto.Workflow;

namespace Capital.Placement.Api.Interfaces;

public interface IProgramService
{
    Task<ProgramDto> CreateProgramAsync(UpsertProgramDto programDto);
    Task<ProgramDto?> UpdateProgramAsync(Guid id, UpsertProgramDto programDto);
    Task<ProgramDto?> GetProgramAsync(Guid id);
    Task<List<ProgramDto>> GetAllProgramAsync();
    Task<ProgramDetailsDto?> GetProgramDetailsAsync(Guid id);
    
    Task<AppTemplateDto?> CreateAppTemplateAsync(Guid programId, UpdateAppTemplateDto appTemplateDto);
    Task<AppTemplateDto?> GetAppTemplateAsync(Guid programId);
    
    Task<WorkflowDto?> CreateWorkflowAsync(Guid programId, UpdateWorkflowDto workflowDto);
    Task<WorkflowDto?> GetWorkflowAsync(Guid programId);
}