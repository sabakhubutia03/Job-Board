using Job_Board_API.Models;

namespace Job_Board_API.JobServices;

public interface IJobService
{
    Task<Job> CreateAsync(Job job);
    Task<List<Job>> GetAllAsync();
    Task<Job> GetByIdAsync(int id);
    Task<Job> UpdateAsync(int id, Job job);
    Task<bool> DeleteAsync(int id);
}