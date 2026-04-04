using Application.DTOs;
using Application.Interface;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using Job_Board_API.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Service;

public class UserService : IUserService
{
    private readonly AppDbContext _db;
    private readonly IMapper _mapper;

    public UserService(AppDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }
    public async Task<UserDto> CreateAsync(UserCreate user)
    {
        var emailchek =  await _db.Users.AnyAsync(e => e.Email == user.Email);
        if (emailchek)
        {
            throw new ApiException(
                "Email is already registered",     
                "Conflict Error",                    
                409,                             
                "A user with this email address already exists in our system.", 
                "/api/user/register"   
            );
        }
        var userEntity = _mapper.Map<User>(user);
        
        _db.Users.Add(userEntity);
        await _db.SaveChangesAsync();
        
        return _mapper.Map<UserDto>(userEntity);
        
    }

    public async Task<IEnumerable<UserDto>> GetAllAsync() =>
        _mapper.Map<IEnumerable<UserDto>>(await _db.Users.ToListAsync());

    public async Task<UserDto?> GetByIdAsync(int id)
    {
        var getById = await _db.Users.FindAsync(id);
        if (getById == null)
        {
            throw new ApiException(
                "User not found",
                "User not found",
                404,
                "A user with this id does not exist in our system.",
                "/api/user/GetByIdAsync"
            );
        }
        return _mapper.Map<UserDto>(getById);
    }

    public async Task<UserDto> UpdateAsync(int id, UserUpdate user)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user));
        }
        
        var update = await _db.Users.FirstOrDefaultAsync(i => i.Id == id);
        if (update == null)
        {
            throw new ApiException(
                "User not found",
                "Not Found", 
                404, 
                "No user exists with the provided ID", 
                $"/api/user/update {id} - id");
        }
        
        var emailExist = await _db.Users.AnyAsync(e => e.Email == user.Email && e.Id != id);
        if (emailExist)
        {
            throw new ApiException(
                "Email is already registered",
                "Conflict Error",
                409,
                "The email address you are trying to use is already taken by another user",
                "/api/user/Update");
        }

        if (!string.IsNullOrWhiteSpace(user.FirstName))
        {
            update.FirstName = user.FirstName;
        }

        if (!string.IsNullOrWhiteSpace(user.LastName))
        {
            update.LastName = user.LastName;
        }

        if (!string.IsNullOrWhiteSpace(user.Email))
        {
            update.Email = user.Email; 
        }
        
        await _db.SaveChangesAsync();
        return _mapper.Map<UserDto>(update);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var deleteUser = await _db.Users.FirstOrDefaultAsync(i => i.Id == id);
        if (deleteUser == null)
        {
            return false;
        }
        
        _db.Users.Remove(deleteUser);
        await _db.SaveChangesAsync();
        
        return true;
    }
}
    