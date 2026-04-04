using Application.DTOs;

namespace Application.Interface;

public interface IUserService
{
   
    Task<UserDto> CreateAsync(UserCreate user);
    Task<IEnumerable<UserDto>> GetAllAsync();
    Task<UserDto?> GetByIdAsync(int id);
    Task<UserDto> UpdateAsync(int id,UserUpdate user);
    Task<bool> DeleteAsync(int id);
}