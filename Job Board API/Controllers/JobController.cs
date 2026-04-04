using Application.DTOs;
using Application.Interface;
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
    public async Task<ActionResult> Create([FromBody]JobCreateDto job)
    {
        var createJob = await _jobService.CreateAsync(job);
        _logger.LogInformation("Job created");
        return CreatedAtAction(nameof(GetById),new { id = createJob.Id}, createJob);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<JobDto>>> GetAll()
    {
        var jobs = await _jobService.GetAllAsync();
        return Ok(jobs);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<JobDto>> GetById(int id)
    {
        var jobId = await _jobService.GetByIdAsync(id);
        return Ok(jobId);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<JobDto>> Update(int id, JobUpdateDto job)
    {
        var update = await _jobService.UpdateAsync(id,job);
        return Ok(update);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
       await _jobService.DeleteAsync(id);
        _logger.LogInformation("Job deleted");
        return NoContent();
    }
    
}