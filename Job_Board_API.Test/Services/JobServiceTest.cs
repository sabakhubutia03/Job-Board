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

    [Fact]
    public async Task GetByIdAsync_ShouldThrowException_WhenIdIsNotFound()
    {  
        var invalidId = 999;
       await Assert.ThrowsAnyAsync<ApiException>(async () => await _jobService.GetByIdAsync(invalidId));
    }

    [Fact]
    public async Task UpdateAsync_ShouldThrowException_WhenIdIsNotFound()
    {
        var nonExistingId = 999;
        var updateDto = new JobUpdateDto
        {
            Title = "Test-xUnit",
            Description = "Description",
            CompanyId = 1
        };
        
        var action = () => _jobService.UpdateAsync(nonExistingId, updateDto);
        
        var exception = await Assert.ThrowsAnyAsync<ApiException>(action);
        
        await Assert.ThrowsAnyAsync<ApiException>(action);
        Assert.Equal(404,exception.StatusCode );
    }

    [Fact]
    public async Task UpdateAsync_ValidRequest_UpdatesJobInDatabase()
    {

        var company = new Company 
        { 
            Id = 1, 
            Name = "Test Company", 
            Address = "Tbilisi",     
            Industry = "IT"         
        };
    
        _db.Companies.Add(company);
    
    
        var job = new Job { Id = 1, Title = "Old Title", CompanyId = 1 ,Description = "Description" };
        _db.Jobs.Add(job);
        await _db.SaveChangesAsync();
        _db.ChangeTracker.Clear();

        var updateDto = new JobUpdateDto { Title = "New Title", CompanyId = 1 };

        _mapperMock.Setup(m => m.Map(updateDto, It.IsAny<Job>()));
        _mapperMock.Setup(m => m.Map<JobDto>(It.IsAny<Job>())).Returns(new JobDto { Title = "New Title" });


        var result = await _jobService.UpdateAsync(1, updateDto);

        Assert.NotNull(result);
        Assert.Equal("New Title", result.Title);
    }

    [Fact]
    public async Task DeleteAsync_ShouldThrowException_WhenIdIsNotFound()
    {
        var nonExistingId = 999;
        
        var action = () => _jobService.DeleteAsync(nonExistingId);
        var exception = await Assert.ThrowsAnyAsync<ApiException>(action);
        
        Assert.Equal(404, exception.StatusCode);
        
    }

    [Fact]
    public async Task DeleteAsync_ValidRequest_DeletesJobInDatabase()
    {
        var job = new Job { Id = 1, Title = "Old Title", CompanyId = 1, Description = "Description"};
        _db.Jobs.Add(job);
        await _db.SaveChangesAsync();
        
       await _jobService.DeleteAsync(job.Id);
       
       var deleteJob = await _db.Jobs.FindAsync(job.Id);
       Assert.Null(deleteJob);
    }
    
}