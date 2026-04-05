using Application.DTOs;
using Domain.Entities;

namespace Application.Interface;

public interface ICompanyService
{
    Task<CompanyDto> CreateAsync(CompanyCreateDto company);
    Task<IEnumerable<CompanyDto>> GetAllAsync();
    Task<CompanyDto> GetByIdAsync(int id);
    Task<CompanyDto> UpdateAsync(int id,CompanuUpdateDto company);
    Task <bool> DeleteAsync(int id);
}