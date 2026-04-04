using Application.DTOs;
using Application.Interface;

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
    public async Task<ActionResult> CreateAsync(UserCreate user)
    {
        var createdUser = await _userService.CreateAsync(user);
        return CreatedAtAction(nameof(GetById), new { id = createdUser.Id }, createdUser);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetAllAsync()
    {
        var getAllUsers = await _userService.GetAllAsync();
        return Ok(getAllUsers);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetById(int id)
    {
        var getUser = await _userService.GetByIdAsync(id);
        return Ok(getUser);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<UserDto>> UpdateAsync(int id,UserUpdate user)
    {
        var update = await _userService.UpdateAsync(id, user);
        _logger.LogInformation("Update User");
        return Ok(update);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAsync(int id)
    {
         await _userService.DeleteAsync(id);
         _logger.LogInformation("Delete User");
         return NoContent();
    }

}