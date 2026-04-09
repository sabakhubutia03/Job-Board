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

public class CompanyServiceTest
{
    private readonly AppDbContext _db;
    private readonly Mock<IMapper> _mapperMock;
    private readonly CompanyService _companyService;
    private readonly Mock<ILogger<CompanyService>> _loggerMock;

    public CompanyServiceTest()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) 
            .Options;
        _db = new AppDbContext(options);
        _mapperMock = new Mock<IMapper>();
        _loggerMock = new Mock<ILogger<CompanyService>>();
        _companyService= new CompanyService(_db, _mapperMock.Object , _loggerMock.Object);
    }

    [Fact]
    public async Task CreateAsync_ShouldThrowException_WhenNameIsNull()
    {
        var compnay = new CompanyCreateDto
        {
            Address = "Test_Address",
            Name = "",
            Industry = "Test_Industry",
        };
        
        var action = () => _companyService.CreateAsync(compnay);
        await Assert.ThrowsAsync<ApiException>(action);
    }

    [Fact]
    public async Task CreateAsync_ShouldThrowException_WheneIndustryIsEmpty()
    {
        var copmany = new CompanyCreateDto
        {
            Name = "Test_Company",
            Industry = "",
            Address = "Test_Address"
        };
        var action= () => _companyService.CreateAsync(copmany);
        await Assert.ThrowsAsync<ApiException>(action);
    }

    [Fact]
    public async Task CreateAsync_ValidCompnay_WhenIsValid()
    {
        var compnay = new CompanyCreateDto
        {
            Name = "Test_Company",
            Industry = "Test_Industry",
            Address = "Test_Address"
        };
        
        var entity = new Company
        {
            Id = 1,
            Name = "Test_Company",
            Industry = "Test_Industry",
            Address = "Test_Address"
        };
        var expectedDto = new CompanyDto
            { Id = 1, Name = "Test_Company", Industry = "Test_Industry", Address = "Test_Address" };
        
        _mapperMock.Setup(m => m.Map<Company>(compnay)).Returns(entity);
        _mapperMock.Setup(m => m.Map<CompanyDto>(entity)).Returns(expectedDto);
        
        var result = await _companyService.CreateAsync(compnay);
        Assert.NotNull(result);
        
        var companyInDb = await _db.Companies.FirstOrDefaultAsync();
        Assert.NotNull(companyInDb);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldThrowException_WhenIdIsNegative()
    {
        var invalidId = 999;
        
        var action = () => _companyService.GetByIdAsync(invalidId);
        await Assert.ThrowsAsync<ApiException>(action);
    }

    [Fact]
    public async Task GetByIdAsync_ValidId_ShouldReturnDto()
    {
        var companyGet = new Company
        {
            Id = 1,
            Name = "Test_Company",
            Industry = "Test_Industry",
            Address = "Test_Address"
        };
        
        _db.Add(companyGet);
        await _db.SaveChangesAsync();
        
        var expectedDto = new CompanyDto{Id = companyGet.Id, Name = companyGet.Name, Industry = companyGet.Industry, Address = "Test_Address"};
        _mapperMock.Setup(m => m.Map<CompanyDto>(It.IsAny<Company>())).Returns(expectedDto);
        
        var result = await _companyService.GetByIdAsync(companyGet.Id);
        Assert.NotNull(result);
    }

    [Fact]
    public async Task DeleteAsync_ShouldThrowException_WhenIdIsNegative()
    {
        var invalidId = 999;
        var action = () => _companyService.DeleteAsync(invalidId);
        await Assert.ThrowsAsync<ApiException>(action);
    }

    [Fact]
    public async Task DeleteAsync_ValidId_ReturnDeleteCompany()
    {
        var company = new Company
        {
            Id = 1,
            Name = "Test_Company",
            Industry = "Test_Industry",
            Address = "Test_Address"
        };
        _db.Add(company);
        await _db.SaveChangesAsync();
        
        await _companyService.DeleteAsync(company.Id);
        var companyInDb = await _db.Companies.FindAsync(company.Id);
        Assert.Null(companyInDb);
    }

    [Fact]
    public async Task UpdateAsync_ShouldThrowException_WhenIdIsNegative()
    {
        var invalidId = 999;
        var compnay = new CompanuUpdateDto
        {
            Name = "Test_Company",
            Industry = "Test_Industry",
            Address = "Test_Address"
        };
        
        var action = () => _companyService.UpdateAsync(invalidId, compnay);
        await Assert.ThrowsAsync<ApiException>(action);
    }

    [Fact]
    public async Task UpdateAsync_ValidId_ShouldReturnDto()
    {
        var company = new Company
        {
            Id = 1,
            Name = "Test_Company",
            Industry = "Test_Industry",
            Address = "Test_Address"
        };
        _db.Add(company);
        await _db.SaveChangesAsync();

        var update = new CompanuUpdateDto
        {
            Name = company.Name,
            Industry = company.Industry,
            Address = company.Address
        };
        _mapperMock.Setup(m => m.Map(update, It.IsAny<Company>()));
        
        var expectedDto = new CompanyDto
        {
            Id = company.Id,
            Name = company.Name,
            Industry = company.Industry,
        };
        
        _mapperMock.Setup(m => m.Map<CompanyDto>(It.IsAny<Company>())).Returns(expectedDto);
        
        var result = await _companyService.UpdateAsync(company.Id, update);
        Assert.NotNull(result);
        var companyInDb = await _db.Companies.FirstOrDefaultAsync();
        Assert.NotNull(companyInDb);
    }
}