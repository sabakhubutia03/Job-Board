using Job_Board_API.Models;

namespace Job_Board_API.JobServices;

public interface IUserService
{
   
    Task<User> CreateAsync(User user);
    Task<List<User>> GetAllAsync();
    Task<User?> GetByIdAsync(int id);
    Task<User> UpdateAsync(int id,User user);
    Task<bool> DeleteAsync(int id);
}