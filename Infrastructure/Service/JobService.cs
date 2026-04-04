using Application.DTOs;
using Application.Interface;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using Job_Board_API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Service;

public class JobService : IJobService
{
    private readonly AppDbContext  _db;
    private readonly IMapper _mapper;
    private readonly ILogger<JobService> _logger;

    public JobService(AppDbContext db, IMapper mapper, ILogger<JobService> logger)
    {
        _db = db;
        _mapper = mapper;
        _logger = logger;
    }
    
    public async Task<JobDto> CreateAsync(JobCreateDto job)
    {
        if (string.IsNullOrEmpty(job.Title) || job.CompanyId <= 0)
        {
            throw new ApiException(
                "Title cannot be empty",
                "BadRequest",
                400,
                "Title is required",
                "/api/Task/CreateTask"
            );
        } 
        
        var jobEntity = _mapper.Map<Job>(job);
        
        await _db.Jobs.AddAsync(jobEntity);
        await _db.SaveChangesAsync();
        
        return _mapper.Map<JobDto>(jobEntity);
        
    }

    public async Task<IEnumerable<JobDto>> GetAllAsync() => 
     _mapper.Map<IEnumerable<JobDto>>(await _db.Jobs.ToListAsync());

    public async Task<JobDto> GetByIdAsync(int id)
    {
        var jobId = await _db.Jobs.FindAsync(id);
        if (jobId == null)
        {
            throw new ApiException(
                "Job Id not found",
                "NotFound",
                404,
                "Job Id not found",
                "/api/job/GetByIdAsync"
            );
        }
        return _mapper.Map<JobDto>(jobId);
    }

    public async Task<JobDto> UpdateAsync(int id, JobUpdateDto job)
    {
        if (id <= 0)
        {
            throw new ApiException(
                "Invalid task id",
                "BadRequest",
                400,
                "Task id must be greater than 0",
                "/api/Task/UpdateTask"
            );
        }
        if (job == null)
        {
            throw new ApiException(
                "Job  cannot be null or empty",
                "BadRequest",
                400,
                "Job Id cannot be null",
                "/api/job/UpdateAsync"
            );
        }
        
        var update = await _db.Jobs.FindAsync(id);
        if (update == null)
        {
            throw new ApiException(
                "Job Id not found",
                "NotFound",
                404,
                "Job Id not found",
                "/api/job/UpdateAsync"
            );
        }

        if (!string.IsNullOrEmpty(job.Title)) 
            update.Title = job.Title;
       
     
        if(!string.IsNullOrEmpty(job.Description))
            update.Description = job.Description;
       
        if(job.CompanyId > 0)
            update.CompanyId = job.CompanyId;
        
        await _db.SaveChangesAsync();
        return _mapper.Map<JobDto>(update);

    }

    public async Task<bool> DeleteAsync(int id)
    {
        var delate = await _db.Jobs.FindAsync(id);
        if (delate == null)
        {
            _logger.LogWarning("Job Id not found");
            throw new ApiException(
                "Job Id not found",
                "NotFound",
                404,
                "Job Id not found",
                "/api/job/DeleteAsync"
            );
        } 
        _db.Jobs.Remove(delate);
        await _db.SaveChangesAsync();
        return true;
    }
}