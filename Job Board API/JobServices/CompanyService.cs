using Job_Board_API.Job_Board.Data;
using Job_Board_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Job_Board_API.JobServices;

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
            _logger.LogError("Company name is empty");
            throw new Exception("Company name is empty");
        }

        if (string.IsNullOrEmpty(company.Industry))
        {
            _logger.LogError("Industry is empty");
            throw new Exception("Industry is empty");
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
            _logger.LogInformation("Company not found");
            throw new Exception("Company not found");
        }
        return getById;
    }

    public async Task<Company> UpdateAsync(int id, Company company)
    {

        if (company == null)
        {
            _logger.LogError("Company not found");
            throw new Exception("Company not found");
        }
        var updateCompany = await _db.Companies.FindAsync(id);
        if (updateCompany == null)
        {
            _logger.LogInformation("Company not found");
            throw new  Exception("Company not found");
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

    public async Task<Company> DeleteAsync(int id)
    {
        var deleteCompany = await _db.Companies.FindAsync(id);
        if (deleteCompany == null)
        {
            _logger.LogError("Company not found");
        }
        
        _db.Remove(deleteCompany);
        await _db.SaveChangesAsync();
        return deleteCompany;
    }
}