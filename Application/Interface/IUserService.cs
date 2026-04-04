using Domain.Entities;

namespace Application.Interface;

public interface IUserService
{
   
    Task<User> CreateAsync(User user);
    Task<List<User>> GetAllAsync();
    Task<User?> GetByIdAsync(int id);
    Task<User> UpdateAsync(int id,User user);
    Task<bool> DeleteAsync(int id);
}