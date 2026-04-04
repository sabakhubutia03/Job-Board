using Domain.Entities;

namespace Application.Interface;

public interface IJobService
{
    Task<Job> CreateAsync(Job job);
    Task<List<Job>> GetAllAsync();
    Task<Job> GetByIdAsync(int id);
    Task<Job> UpdateAsync(int id, Job job);
    Task DeleteAsync(int id);
}