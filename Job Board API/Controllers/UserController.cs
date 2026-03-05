using Job_Board_API.Job_Board.Data;
using Job_Board_API.JobServices;
using Job_Board_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Job_Board_API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    public readonly IUserService _userService;
    public readonly ILogger<UserController> _logger;

    public UserController(IUserService userService, ILogger<UserController> logger)
    {
        _userService = userService;
        _logger = logger; 
    }

    [HttpPost]
    public async Task<ActionResult<User>> CreateAsync(User user)
    {
        var createdUser = await _userService.CreateAsync(user);
        _logger.LogInformation("Created User");
        return Ok(createdUser);
    }

    [HttpGet]
    public async Task<ActionResult<List<User>>> GetAllAsync()
    {
        var getAllUsers = await _userService.GetAllAsync();
        _logger.LogInformation("Get all Users");
        return Ok(getAllUsers);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetById(int id)
    {
        var getUser = await _userService.GetByIdAsync(id);
            _logger.LogInformation("Get User");
            return Ok(getUser);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<User>> UpdateAsync(int id,User user)
    {
        var update = await _userService.UpdateAsync(id, user);
        _logger.LogInformation("Update User");
        return Ok(update);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<User>> DeleteAsync(int id)
    {
         await _userService.DeleteAsync(id);
        _logger.LogInformation("Delete User");
        return Ok();
    }

}