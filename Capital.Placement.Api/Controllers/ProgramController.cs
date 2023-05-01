using Capital.Placement.Api.Dto.ApplicationTemplate;
using Capital.Placement.Api.Dto.Program;
using Capital.Placement.Api.Dto.Workflow;
using Capital.Placement.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Capital.Placement.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ProgramController : ControllerBase
{
    private readonly IProgramService _programService;

    public ProgramController(IProgramService programService)
    {
        _programService = programService;
    }
    
    [HttpPost]
    [Route("create")]
    public async Task<IActionResult> CreateProgram(UpsertProgramDto request)
    {
        return Ok(await _programService.CreateProgramAsync(request));
    }
    
    [HttpPut]
    [Route("{id:guid}/update")]
    public async Task<IActionResult> UpdateProgram(Guid id, UpsertProgramDto request)
    {
        var program = await _programService.UpdateProgramAsync(id, request);
        return program == null 
            ? BadRequest(new {Message = "Program Id not found"})
            : Ok(program);
    }
    
    [HttpGet]
    [Route("{id:guid}/get")]
    public async Task<IActionResult> GetProgram(Guid id)
    {
        var program = await _programService.GetProgramAsync(id);
        return program == null 
            ? NotFound(new {Message = "Program Id not found"})
            : Ok(program);
    }
    
    [HttpGet]
    [Route("list")]
    public async Task<IActionResult> GetAllProgramDetails()
    {
        return Ok(await _programService.GetAllProgramAsync());
    }
    
    [HttpGet]
    [Route("{id:guid}/preview")]
    public async Task<IActionResult> GetProgramDetails(Guid id)
    {
        var program = await _programService.GetProgramDetailsAsync(id);
        return program == null 
            ? NotFound(new {Message = "Program Id not found"})
            : Ok(program);
    }
    
    [HttpPut]
    [Route("{programId:guid}/app.template.create")]
    public async Task<IActionResult> CreateAppTemplate(Guid programId, UpdateAppTemplateDto appTemplateDto)
    {
        var program = await _programService.CreateAppTemplateAsync(programId, appTemplateDto);
        return program == null 
            ? BadRequest(new {Message = "Program Id not found"})
            : Ok(program);
    }
    
    [HttpGet]
    [Route("{programId:guid}/app.template")]
    public async Task<IActionResult> GetAppTemplate(Guid programId)
    {
        var program = await _programService.GetAppTemplateAsync(programId);
        return program == null 
            ? NotFound(new {Message = "Program Id not found"})
            : Ok(program);
    }
    
    [HttpPut]
    [Route("{programId:guid}/workflow.create")]
    public async Task<IActionResult> CreateWorkflow(Guid programId, UpdateWorkflowDto request)
    {
        var workflow = await _programService.CreateWorkflowAsync(programId, request);
        return workflow == null 
                ? BadRequest(new {Message = "Program Id not found"}) 
                : Ok(workflow);
    }
    
    [HttpGet]
    [Route("{programId:guid}/workflow")]
    public async Task<IActionResult> GetWorkflow(Guid programId)
    {
        var workflow = await _programService.GetWorkflowAsync(programId);
        return workflow == null 
            ? NotFound(new {Message = "Program Id not found"})
            : Ok(workflow);
    }
    
}