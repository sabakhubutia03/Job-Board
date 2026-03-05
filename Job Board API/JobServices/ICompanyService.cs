using Job_Board_API.Models;

namespace Job_Board_API.JobServices;

public interface ICompanyService
{
    Task<Company> CreateAsync(Company company);
    Task<List<Company>> GetAllAsync();
    Task<Company> GetByIdAsync(int id);
    Task<Company> UpdateAsync(int id,Company company);
    Task<Company> DeleteAsync(int id);
}