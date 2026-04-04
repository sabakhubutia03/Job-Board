using Domain.Entities;

namespace Application.Interface;

public interface ICompanyService
{
    Task<Company> CreateAsync(Company company);
    Task<List<Company>> GetAllAsync();
    Task<Company> GetByIdAsync(int id);
    Task<Company> UpdateAsync(int id,Company company);
    Task DeleteAsync(int id);
}