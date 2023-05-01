using System.Drawing.Drawing2D;
using AutoMapper;
using Capital.Placement.Api.Dto.ApplicationTemplate;
using Capital.Placement.Api.Dto.Program;
using Capital.Placement.Api.Dto.Workflow;
using Capital.Placement.Api.Helper;
using Capital.Placement.Api.Mapping;
using Capital.Placement.Api.Model;
using Capital.Placement.Api.Services;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Moq;

namespace Integration.Test;

public class ProgramServiceTest
{
    // private readonly ProgramService _programService;
    private readonly IConfigurationProvider _configuration;
    private readonly IMapper _mapper;
    public ProgramServiceTest()
    {
        var configuration = new MapperConfiguration(config =>
            config.AddProfile<MappingProfile>()); 
    
        _mapper = configuration.CreateMapper();
    }
    
    [Fact]
    public async void Should_Create_New_Program()
    {
        //Arrange
        var programDtoMock = new Mock<UpsertProgramDto>
        {
            Object =
            {
                Title = "Some test title",
                Duration = 5
            }
        };

        // Act
        using var dbFixture = new DbFixture(nameof(Program));
        var programService = new ProgramService(dbFixture.DbOption, _mapper);
        var result = await programService.CreateProgramAsync(programDtoMock.Object);
        
        // Assert
        Assert.IsType<ProgramDto>(result);
        Assert.Equal(programDtoMock.Object.Title, result.Title);
        Assert.Equal(programDtoMock.Object.Duration, 5);
    }
    
    [Fact]
    public async void Should_Return_A_Program()
    {
        //Arrange
        var id = Guid.NewGuid();
        var program = new Program
        {
            Id = id
        };
        
        using var dbFixture = new DbFixture(nameof(Program));
        await dbFixture.InsertAsync(id.ToString(), program);
            
        // Act
        var programService = new ProgramService(dbFixture.DbOption, _mapper);
        var result = await programService.GetProgramAsync(id);
        
        // Assert
        Assert.IsType<ProgramDto>(result);
        Assert.NotNull(result);
        Assert.Equal(id, result.Id);
    }
    
    [Fact]
    public async void Should_Return_Null_When_Program_Not_Exist()
    {
        //Arrange
        using var dbFixture = new DbFixture(nameof(Program));
            
        // Act
        var programService = new ProgramService(dbFixture.DbOption, _mapper);
        var result = await programService.GetProgramAsync(Guid.NewGuid());
        
        // Assert
        Assert.Null(result);
    }
    
    [Fact]
    public async void Should_Update_A_Program()
    {
        //Arrange
        var id = Guid.NewGuid();
        var program = new Program
        {
            Id = id
        };
        
        using var dbFixture = new DbFixture(nameof(Program));
        await dbFixture.InsertAsync(id.ToString(), program);
            
        // Act
        var newProgram = new UpsertProgramDto
        {
            Title = "Updated title"
        };
        
        var programService = new ProgramService(dbFixture.DbOption, _mapper);
        var result = await programService.UpdateProgramAsync(id, newProgram);
        
        // Assert
        Assert.IsType<ProgramDto>(result);
        Assert.NotNull(result);
        Assert.Equal(id, result.Id);
    }
    
    [Fact]
    public async void Should_Create_Application_Template()
    {
        //Arrange
        
        var program = new Program
        {
            Id = Guid.NewGuid()
        };
        
        using var dbFixture = new DbFixture(nameof(Program));
        await dbFixture.InsertAsync(program.Id.ToString(), program);
        
        var appTempDtoMock = new Mock<UpdateAppTemplateDto>();
            
        // Act
        var programService = new ProgramService(dbFixture.DbOption, _mapper);
        var result = await programService.CreateAppTemplateAsync(program.Id, appTempDtoMock.Object);
        
        // Assert
        Assert.IsType<AppTemplateDto>(result);
        Assert.NotNull(result);
    }
    
    [Fact]
    public async void Should_Return_Application_Template()
    {
        //Arrange
        var id = Guid.NewGuid();
        var templateId = Guid.NewGuid();
        var program = new Program
        {
            Id = id,
            ApplicationTemplate = new ApplicationTemplate
            {
                Id = templateId,
                Profile = new List<QuestionDetails>().ToJsonString(),
                PersonalInfo = new List<QuestionDetails>().ToJsonString(),
                AdditionalQuestion = new List<QuestionDetails>().ToJsonString()
            }
        };
        
        using var dbFixture = new DbFixture(nameof(Program));
        await dbFixture.InsertAsync(id.ToString(), program);
            
        // Act
        var programService = new ProgramService(dbFixture.DbOption, _mapper);
        var result = await programService.GetAppTemplateAsync(id);
        
        // Assert
        Assert.IsType<AppTemplateDto>(result);
        Assert.NotNull(result);
        Assert.Equal(templateId, result.Id);
    }
    
    [Fact]
    public async void Should_Create_WorkFlow()
    {
        //Arrange
        
        var program = new Program
        {
            Id = Guid.NewGuid()
        };
        
        using var dbFixture = new DbFixture(nameof(Program));
        await dbFixture.InsertAsync(program.Id.ToString(), program);
        
        var workflowDtoMock = new Mock<UpdateWorkflowDto>
        {
            Object =
            {
                Stages = new List<StageDto>()
            }
        };
            
        // Act
        var programService = new ProgramService(dbFixture.DbOption, _mapper);
        var result = await programService.CreateWorkflowAsync(program.Id, workflowDtoMock.Object);
        
        // Assert
        Assert.IsType<WorkflowDto>(result);
        Assert.NotNull(result);
    }
    
    [Fact]
    public async void Should_Return_Workflow()
    {
        //Arrange
        var id = Guid.NewGuid();
        var workflowId = Guid.NewGuid();
        var program = new Program
        {
            Id = id,
            Workflow = new Workflow()
            {
                Id = workflowId,
                Stages = new List<Stage>()
            }
        };
        
        using var dbFixture = new DbFixture(nameof(Program));
        await dbFixture.InsertAsync(id.ToString(), program);
            
        // Act
        var programService = new ProgramService(dbFixture.DbOption, _mapper);
        var result = await programService.GetWorkflowAsync(id);
        
        // Assert
        Assert.IsType<WorkflowDto>(result);
        Assert.NotNull(result);
        Assert.Equal(workflowId, result.Id);
    }
}