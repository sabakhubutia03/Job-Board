using Job_Board_API.JobServices;
using Job_Board_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Job_Board_API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class JobController : ControllerBase
{
    private readonly IJobService _jobService;
    private readonly ILogger<JobController> _logger;

    public JobController(IJobService jobService, ILogger<JobController> logger)
    {
        _jobService = jobService;
        _logger = logger;
    }

    [HttpPost]
    public async Task<ActionResult<Job>> Create([FromBody]Job job)
    {
        var createJob = await _jobService.CreateAsync(job);
        _logger.LogInformation("Job created");
        return CreatedAtAction(nameof(GetById),new { id = createJob.Id}, createJob);
    }

    [HttpGet]
    public async Task<ActionResult<List<Job>>> GetAll()
    {
        var jobs = await _jobService.GetAllAsync();
        _logger.LogInformation("Jobs retrieved");
        return Ok(jobs);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Job>> GetById(int id)
    {
        var jobId = await _jobService.GetByIdAsync(id);
        _logger.LogInformation("Jobs retrieved");
        return Ok(jobId);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Job>> Update(int id, Job job)
    {
        var update = await _jobService.UpdateAsync(id,job);
        _logger.LogInformation("Job updated");
        return Ok(update);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
       await _jobService.DeleteAsync(id);
        _logger.LogInformation("Job deleted");
        return Ok();
    }
    
}