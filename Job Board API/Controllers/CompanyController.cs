using Application.DTOs;
using Application.Interface;
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
    public async Task<ActionResult> Create(CompanyCreateDto company)
    {
        var newCompany = await _companyService.CreateAsync(company);
        return CreatedAtAction(nameof(GetById), new{ id = newCompany.Id },newCompany);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CompanyDto>>> GetAll()
    {
        var getAll = await _companyService.GetAllAsync();
        return Ok(getAll);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CompanyDto>> GetById(int id)
    {
        var getById = await _companyService.GetByIdAsync(id);
        return Ok(getById);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<CompanyDto>> Update(int id, CompanuUpdateDto company)
    {
        var updateCompany = await _companyService.UpdateAsync(id,company);
        return Ok(updateCompany);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
         await _companyService.DeleteAsync(id);
        _logger.LogInformation(" Company deleted");
        return NoContent();
    }
}