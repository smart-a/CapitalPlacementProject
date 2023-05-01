using AutoMapper;
using Capital.Placement.Api.Config;
using Capital.Placement.Api.Dto.ApplicationTemplate;
using Capital.Placement.Api.Dto.Program;
using Capital.Placement.Api.Dto.Workflow;
using Capital.Placement.Api.Interfaces;
using Capital.Placement.Api.Model;
using Microsoft.Extensions.Options;

namespace Capital.Placement.Api.Services;

public class ProgramService : CosmosDbService<Model.Program>, IProgramService
{
    private readonly IMapper _mapper;

    public ProgramService(IOptions<CosmosDbSetting> options, IMapper mapper) 
        : base(nameof(Model.Program), options, mapper)
    {
        _mapper = mapper;
    }

    public async Task<ProgramDto> CreateProgramAsync(UpsertProgramDto programDto)
    {
        var program = _mapper.Map<Model.Program>(programDto);
        program.Id = Guid.NewGuid();
        program.CreatedAt = DateTime.Now;

        await AddAsync(program, program.Id.ToString());
        
        var result = _mapper.Map<ProgramDto>(program);
        return result;
    }

    public async Task<ProgramDto?> UpdateProgramAsync(Guid id, UpsertProgramDto programDto)
    {
        var existingProgram = await GetAsync(id.ToString());
        if (existingProgram == null)
        {
            return null;
        }
        
        var program  = _mapper.Map<Model.Program>(programDto);
        program.Id = existingProgram.Id;
        program.ApplicationTemplate = existingProgram.ApplicationTemplate;
        program.Workflow = existingProgram.Workflow;
        program.CreatedAt = existingProgram.CreatedAt;
        program.UpdatedAt = DateTime.Now;

        await UpdateAsync(id.ToString(), program);
        
        var result = _mapper.Map<ProgramDto>(program);
        return result;
    }

    public async Task<ProgramDto?> GetProgramAsync(Guid id)
    {
        var program = await GetAsync(id.ToString());
        return program == null ? null : _mapper.Map<ProgramDto>(program);
    }

    public async Task<List<ProgramDto>> GetAllProgramAsync()
    {
        return (await GetMultipleAsync<ProgramDto>())
            .ToList();
    }

    public async Task<ProgramDetailsDto?> GetProgramDetailsAsync(Guid id)
    {
        var program = await GetAsync(id.ToString());
        return program == null ? null : ProgramDetailsDto.Parse(program, _mapper);
    }

    public async Task<AppTemplateDto?> CreateAppTemplateAsync(Guid programId, UpdateAppTemplateDto appTemplateDto)
    {
        var program = await GetAsync(programId.ToString());
        if (program == null)
        {
            return null;
        }

        var appTemplate = appTemplateDto.ToAppTemplate(_mapper);

        program.ApplicationTemplate = appTemplate;
        await UpdateAsync(programId.ToString(), program);
        
        var result = AppTemplateDto.Parse(appTemplate, _mapper);
        return result;
    }

    public async Task<AppTemplateDto?> GetAppTemplateAsync(Guid programId)
    {
        var program = await GetAsync(programId.ToString());
        return program?.ApplicationTemplate == null ? null 
            : AppTemplateDto.Parse(program.ApplicationTemplate, _mapper);
    }

    public async Task<WorkflowDto?> CreateWorkflowAsync(Guid programId, UpdateWorkflowDto dto)
    {
        var program = await GetAsync(programId.ToString());
        if (program == null)
        {
            return null;
        }
        
        var workflow = new Workflow(dto.Stages)
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.Now
        };

        program.Workflow = workflow;
        await UpdateAsync(programId.ToString(), program);
        
        return WorkflowDto.Parse(workflow);
    }

    public async Task<WorkflowDto?> GetWorkflowAsync(Guid programId)
    {
        var program = await GetAsync(programId.ToString());
        return program?.Workflow == null ? null 
            : WorkflowDto.Parse(program.Workflow);
    }
}