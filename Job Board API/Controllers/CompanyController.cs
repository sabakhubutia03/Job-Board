using Job_Board_API.JobServices;
using Job_Board_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Job_Board_API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CompanyController : ControllerBase
{
    private readonly ICompanyService _companyService;
    private readonly ILogger<CompanyController> _logger;

    public CompanyController(ICompanyService companyService, ILogger<CompanyController> logger)
    {
        _companyService = companyService;
        _logger = logger;
    }

    [HttpPost]
    public async Task<ActionResult<Company>> Create(Company company)
    {
        var newCompany = await _companyService.CreateAsync(company);
        _logger.LogInformation("New company created");
        return CreatedAtAction(nameof(GetById), new{ id = newCompany.Id },newCompany);
    }

    [HttpGet]
    public async Task<ActionResult<List<Company>>> GetAll()
    {
        var getAll = await _companyService.GetAllAsync();
        _logger.LogInformation("Company retrieved");
        return Ok(getAll);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Company>> GetById(int id)
    {
        var getById = await _companyService.GetByIdAsync(id);
        _logger.LogInformation("Company retrieved");
        return Ok(getById);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Company>> Update(int id, Company company)
    {
        var updateCompany = await _companyService.UpdateAsync(id,company);
        _logger.LogInformation(" Company updated");
        return Ok(updateCompany);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
         await _companyService.DeleteAsync(id);
        _logger.LogInformation(" Company deleted");
        return Ok();
    }
}