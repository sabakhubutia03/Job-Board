using Application.DTOs;
using Domain.Entities;

namespace Application.Interface;

public interface IJobService
{
    Task<JobDto> CreateAsync(JobCreateDto job);
    Task<IEnumerable<JobDto>> GetAllAsync();
    Task<JobDto> GetByIdAsync(int id);
    Task<JobDto> UpdateAsync(int id, JobUpdateDto job);
    Task <bool> DeleteAsync(int id);
}