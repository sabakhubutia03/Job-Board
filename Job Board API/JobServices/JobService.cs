using Job_Board_API.Job_Board.Data;
using Job_Board_API.Models;
using Microsoft.EntityFrameworkCore;

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
            throw new ArgumentException("Job Title and Company Id cannot be null");
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
            throw new ArgumentException("Job Id not found");
        }
        return jobId;
    }

    public async Task<Job> UpdateAsync(int id, Job job)
    {

        if (job == null)
        {
            _logger.LogError("Job  cannot be null");
            throw new ArgumentException("Job  cannot be null");
        }
        
        var update = await _db.Jobs.FindAsync(id);
       if (update == null)
       {
           _logger.LogError("Job Id not found");
           throw new ArgumentException("Job Id not found");
       }

       if (!string.IsNullOrEmpty(job.Title)) 
           update.Title = job.Title;
       
     
       if(!string.IsNullOrEmpty(job.Description))
           update.Description = job.Description;
       
       await _db.SaveChangesAsync();
       return update;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var delate = await _db.Jobs.FindAsync(id);
        if (delate == null)
        {
            _logger.LogWarning("Job Id not found");
            return false;
        } 
        _db.Jobs.Remove(delate);
        await _db.SaveChangesAsync();
        return true;
    }
}