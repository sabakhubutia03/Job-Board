using Application.Interface;
using Domain.Entities;
using Domain.Exceptions;
using Job_Board_API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Job_Board_API.JobServices;

public class JobService : IJobService
{
    private readonly AppDbContext _db;
    private readonly ILogger<JobService> _logger;

    public JobService(AppDbContext db, ILogger<JobService> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task<Job> CreateAsync(Job job)
    { 
    
        if (string.IsNullOrEmpty(job.Title) || job.CompanyId <= 0)
        {
            _logger.LogError("Job Title and Company Id cannot be null");
            throw new ApiException(
                "Title cannot be empty",
                "BadRequest",
                400,
                "Title is required",
                "/api/Task/CreateTask"
            );
        } 
        
        await _db.Jobs.AddAsync(job);
        await _db.SaveChangesAsync();
        return job;
    }

    public async Task<List<Job>> GetAllAsync()
    {
        var jobs = await _db.Jobs.ToListAsync();
        if (jobs.Count == 0)
        {
            _logger.LogInformation("No Jobs found");
        }
        return jobs;
    }

    public  async Task<Job> GetByIdAsync(int id)
    {
        var jobId = await _db.Jobs.FindAsync(id);
        if (jobId == null)
        {
            _logger.LogError("Job Id not found");
            throw new ApiException(
                "Job Id not found",
                "NotFound",
                404,
                "Job Id not found",
                "/api/job/GetByIdAsync"
            );
        }
        return jobId;
    }

    public async Task<Job> UpdateAsync(int id, Job job)
    {
        if (id <= 0)
        {
            _logger.LogError("Job Id cannot be negative");
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
            _logger.LogError("Job  cannot be null");
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
           _logger.LogError("Job Id not found");
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
       return update;
    }

    public async Task DeleteAsync(int id)
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
    }
}