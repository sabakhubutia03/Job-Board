using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using Infrastructure.Service;
using Job_Board_API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace Job_Board_API.Test.Services;

public class JobServiceTest
{
    private readonly AppDbContext _db;
    private readonly Mock<IMapper> _mapperMock;
    private readonly JobService _jobService;
    private readonly Mock<ILogger<JobService>> _loggerMock;

    public JobServiceTest()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) 
            .Options;
        _db = new AppDbContext(options);
        _mapperMock = new Mock<IMapper>();
        
        _loggerMock = new Mock<ILogger<JobService>>();
        _jobService = new JobService(_db, _mapperMock.Object , _loggerMock.Object);
    }

    [Fact]
    public async Task CreateAsync_ShouldThrowException_WhenTitleIsEmpty()
    {
        var invalidJob = new JobCreateDto
        {
            Title = "",
            Description = "Description",
            CompanyId = 1
        };
        
        var action  = () => _jobService.CreateAsync(invalidJob);
        
        await Assert.ThrowsAnyAsync<ApiException>(action);
    }

    [Fact]
    public async Task CreateAsync_ShouldThrowException_WhenCompanyIdIsEmpty()
    {
        var invalidJob = new JobCreateDto
        {
            Title = "Test-xUnit",
            Description = "Description",
            CompanyId = -1
        };
        
        var action = () => _jobService.CreateAsync(invalidJob);
        
        await Assert.ThrowsAnyAsync<ApiException>(action);
    }

    [Fact]
    public async Task CreateAsync_ValidJob_ReturnJobDto()
    {
        var createJob = new JobCreateDto
        {
            Title = "Test-xUnit",
            Description = "Description",
            CompanyId = 1
        };
        
        var jobEntity = new Job{Id = 1, Title = "Test-xUnit", Description = "Description"};
        var expectedDto = new JobDto{Id = 1, Title = createJob.Title, Description = createJob.Description, CompanyId = createJob.CompanyId};
        
        _mapperMock.Setup(m => m.Map<Job>(createJob)).Returns(jobEntity);
        _mapperMock.Setup(m => m.Map<JobDto>(jobEntity)).Returns(expectedDto);
        
        
        var result = await _jobService.CreateAsync(createJob);
        
        Assert.NotNull(result);
        Assert.Equal(createJob.Title, result.Title);
        
        var jobInDb = await _db.Jobs.FirstOrDefaultAsync();
        Assert.NotNull(jobInDb);
    }
    
}