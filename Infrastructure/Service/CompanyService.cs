using Application.DTOs;
using Application.Interface;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using Job_Board_API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Service;

public class CompanyService : ICompanyService
{
    private readonly AppDbContext _db;
    private readonly IMapper _mapper;
    private readonly ILogger<CompanyService> _logger;

    public CompanyService(AppDbContext db, IMapper mapper, ILogger<CompanyService> logger)
    {
        _db = db;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<CompanyDto> CreateAsync(CompanyCreateDto company)
    {
        if (string.IsNullOrEmpty(company.Name))
        {
            throw new ApiException(
                "Company name is empty or null",
                "BadRequest",
                400,
                "Company name cannot be empty",
                "/api/company/name");
        }

        if (string.IsNullOrEmpty(company.Industry))
        {
            throw new ApiException(
                "Industry is empty or null",
                "BadRequest",
                400,
                "Industry cannot be empty",
                "/api/company/industry"
            );
        }
        
        var companyEntity = _mapper.Map<Company>(company);
        await _db.Companies.AddAsync(companyEntity);
        await _db.SaveChangesAsync();
        return _mapper.Map<CompanyDto>(companyEntity);

    }

    public async Task<IEnumerable<CompanyDto>> GetAllAsync() 
    => _mapper.Map<IEnumerable<CompanyDto>>(await _db.Companies.ToListAsync());

    public async Task<CompanyDto> GetByIdAsync(int id)
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
        return _mapper.Map<CompanyDto>(getById);
    }

    public async Task<CompanyDto> UpdateAsync(int id, CompanuUpdateDto company)
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
        
        _mapper.Map(company, updateCompany);
        await _db.SaveChangesAsync();
        return _mapper.Map<CompanyDto>(updateCompany);
    }

    public async Task<bool> DeleteAsync(int id)
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
        
        _db.Companies.Remove(deleteCompany);
        await _db.SaveChangesAsync();
        return true;
    }
}