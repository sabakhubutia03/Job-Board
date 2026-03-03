using Job_Board_API.Job_Board.Data;
using Job_Board_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Job_Board_API.JobServices;

public class UserServiceService : IUserService
{
    private readonly AppDbContext _db;
    private readonly ILogger<UserServiceService> _logger;
    public UserServiceService(AppDbContext db, ILogger<UserServiceService> logger)
    {
        _db = db;
        _logger = logger;
    }
    
    public async Task<User> CreateAsync(User user)
    {
        if (user == null)
        {
            _logger.LogError("User is null or empty");
            throw new ArgumentNullException(nameof(user));
        }
        
        var emailchek =  await _db.Users.AnyAsync(e => e.Email == user.Email);
        if (emailchek)
        {
            _logger.LogError("Email is already registered");
            throw new Exception("Email is already registered");
        }
        
        _db.Users.Add(user);
        await _db.SaveChangesAsync();
        
        return user;
    }

    public async Task<List<User>> GetAllAsync()
    {
        var  usersList = await _db.Users.ToListAsync();
        if (usersList.Count == 0)
        {
            _logger.LogError("Users is empty");
        }
        return usersList;
        
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        var getById = await _db.Users.FirstOrDefaultAsync(e => e.Id == id);
        if (getById == null)
        {
            _logger.LogWarning("User with id {Id} not found", id);
        }

        return getById;
    }

    public async Task<User> UpdateAsync(int id,User user)
    {
        if (user == null)
        {
            _logger.LogError("User is null or empty");
            throw new ArgumentNullException(nameof(user));
        }
        
        var update = await _db.Users.FirstOrDefaultAsync(i => i.Id == id);
        if (update == null)
        {
            _logger.LogError("User not found");
            throw new Exception("User not found");
        }
        
        var emailExist = await _db.Users.AnyAsync(e => e.Email == user.Email && e.Id != id);
        if (emailExist)
        {
            _logger.LogError("Email is already registered");
            throw new Exception("Email is already registered");
        }
        update.Email = user.Email;
        update.FirstName = user.FirstName;
        update.LastName = user.LastName;
        
        await _db.SaveChangesAsync();
        return update;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var deleteUser = await _db.Users.FirstOrDefaultAsync(i => i.Id == id);
        if (deleteUser == null)
        {
            _logger.LogWarning("User with id {Id} not found", id);
            return false;
        }
        
        _db.Users.Remove(deleteUser);
        await _db.SaveChangesAsync();
        
        return true;
    }
}