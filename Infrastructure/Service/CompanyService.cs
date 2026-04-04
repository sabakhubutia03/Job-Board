using Application.Interface;
using Domain.Entities;
using Domain.Exceptions;
using Job_Board_API.Data;
using Job_Board_API.JobServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Service;

public class CompanyService : ICompanyService
{
    private readonly AppDbContext _db;
    private readonly ILogger<CompanyService> _logger;

    public CompanyService(AppDbContext db, ILogger<CompanyService> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task<Company> CreateAsync(Company company)
    {
        if (string.IsNullOrEmpty(company.Name))
        {
            _logger.LogWarning("Company name is empty");
            throw new ApiException(
                "Company name is empty or null",
                "BadRequest",
                400,
                "Company name cannot be empty",
                "/api/company/name");
        }

        if (string.IsNullOrEmpty(company.Industry))
        {
            _logger.LogWarning("Industry is empty");
            throw new ApiException(
                "Industry is empty or null",
                "BadRequest",
                400,
                "Industry cannot be empty",
                "/api/company/industry"
                );
        }
        
        await _db.Companies.AddAsync(company);
        await _db.SaveChangesAsync();
        return company;
    }

    public async Task<List<Company>> GetAllAsync() 
        => await _db.Companies.ToListAsync();

    public async Task<Company> GetByIdAsync(int id)
    {
        var getById = await _db.Companies.FindAsync(id);
        if (getById == null)
        {
            _logger.LogWarning("Company not found");
            throw new ApiException(
                "Company not found",
                "NotFound",
                404,
                "Company not found",
                "/api/company/id"
            );
        }
        return getById;
    }

    public async Task<Company> UpdateAsync(int id, Company company)
    {

        if (company == null)
        {
            _logger.LogWarning("Company not found");
            throw new ApiException(
                "Company cannot be null",
                "BadRequest",
                400,
                "Company cannot be null",
                "/api/company/update"
            );
        }
        
        var updateCompany = await _db.Companies.FindAsync(id);
        if (updateCompany == null)
        {
            _logger.LogWarning("Company not found");
            throw new ApiException(
                "Company not found",
                "NotFound",
                404,
                "Company not found",
                "/api/company/company"
            );
        }
        
        if(!string.IsNullOrEmpty(company.Name)) 
            updateCompany.Name = company.Name;
        
        if(!string.IsNullOrEmpty(company.Industry)) 
            updateCompany.Industry = company.Industry;
        
        if(!string.IsNullOrEmpty(company.Address)) 
            updateCompany.Address = company.Address;
        
        await _db.SaveChangesAsync();
        return updateCompany;
    }

    public async Task DeleteAsync(int id)
    {
        var deleteCompany = await _db.Companies.FindAsync(id);
        if (deleteCompany == null)
        {
            _logger.LogWarning("Company not found");
            throw new ApiException(
                "Company not found",
                "NotFound",
                404,
                "Company not found",
                "/api/company/delete"
            );
        }
        
        _db.Remove(deleteCompany);
        await _db.SaveChangesAsync();
      
    }
}