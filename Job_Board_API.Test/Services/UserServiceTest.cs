using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using Infrastructure.Service;
using Job_Board_API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace Job_Board_API.Test.Services;

public class UserServiceTest
{
    public readonly AppDbContext _db;
    public readonly Mock<IMapper> _mapperMock;
    public readonly UserService _userService;
    public readonly Mock<ILogger<UserService>> _loggerMock;

    public UserServiceTest()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        _db = new AppDbContext(options);
        _mapperMock = new Mock<IMapper>();
        _loggerMock = new Mock<ILogger<UserService>>();
        _userService = new UserService(_db, _mapperMock.Object);
    }

    [Fact]
    public async Task CreateAsync_ShouldThrowException_WhenEmailAlreadyExists()
    {
        var existingUser = new User
        {
            Id = 1,
            Email = "Test@gmail.com",
            FirstName = "Test",
            LastName = "Test",
            Password = "Test"
        };
        _db.Users.Add(existingUser);
        await _db.SaveChangesAsync();

        var newUser = new UserCreate
        {
            Email = existingUser.Email,
            FirstName = "NewUser",
            LastName = "NewUser",
            Password = "NewUser"
        };
        
        var action = () => _userService.CreateAsync(newUser);
        var exception = await Assert.ThrowsAnyAsync<ApiException>(action);
        
        Assert.Equal(409, exception.StatusCode);
    }

    [Fact]
    public async Task CreateAsync_ValidUser_ReturnsUserDto()
    {
        var userDto = new UserCreate
        {
            Email = "Test@gmail.com",
            FirstName = "Test",
            LastName = "Test",
            Password = "Test"
        };
        
        var userEntity = new User{Id = 1, Email = userDto.Email, FirstName = userDto.FirstName, LastName = userDto.LastName, Password = userDto.Password};
        _mapperMock.Setup(m => m.Map<User>(userDto)).Returns(userEntity);
        
        var expectedResponse = new UserDto{Id = 1, Email = userDto.Email, FirstName = userDto.FirstName, LastName = userDto.LastName};
        _mapperMock.Setup(m => m.Map<UserDto>(userEntity)).Returns(expectedResponse); 
        
        var result  = await _userService.CreateAsync(userDto);
        
        Assert.NotNull(result);
        
        var userInDb = await _db.Users.FirstOrDefaultAsync(u => u.Email == userDto.Email);
        Assert.NotNull(userInDb);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldThrowException_WhenIdIsInvalid()
    {
        var invalidId = 999;
        var action =  () => _userService.GetByIdAsync(invalidId);
        var exception = await Assert.ThrowsAsync<ApiException>(action);
        Assert.Equal(404, exception.StatusCode);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnUserDto_WhenIdIsValid()
    {
        var userDto = new User
        {
            Id = 1,
            Email = "Test@gmail.com",
            FirstName = "Test",
            LastName = "Test",
            Password = "Test"
        };
        _db.Add(userDto);
        await _db.SaveChangesAsync();
        
        var expectedDto = new UserDto{Email = userDto.Email, FirstName = userDto.FirstName, LastName = userDto.LastName};
        _mapperMock.Setup(m => m.Map<UserDto>(It.IsAny<User>())).Returns(expectedDto);
        
        var result = await _userService.GetByIdAsync(userDto.Id);
        Assert.NotNull(result);
    }

    [Fact]
    public async Task UpdateAsync_ShouldThrowException_WhenIdIsInvalid()
    {
        var invalidId = 999;
        var updateUser = new UserUpdate
        {
            FirstName = "Test-Update",
            LastName = "Test-Update"
        };
      
        var action = () => _userService.UpdateAsync(invalidId, updateUser);
        
        var exception = await Assert.ThrowsAsync<ApiException>(action);
        Assert.Equal(404, exception.StatusCode);
    }

    [Fact]
    public async Task UpdateAsync_ValidUser_WhenIsValid()
    {
        var user = new User
        {
            Id = 1,
            FirstName = "Test",
            LastName = "Test",
            Email = "Test@gmail.com",
            Password = "1234"
        };
        _db.Users.Add(user);
        await _db.SaveChangesAsync();

        var updateUser = new UserUpdate
        {
            Email = "Update-Email",
            FirstName = "Update-Update",
            LastName = "Update-Update"
        };
        
        _mapperMock.Setup(m => m.Map(updateUser, It.IsAny<User>()));
        var expectedDto = new UserDto{Id = 1, Email = "Update-Email", FirstName = "Update-Update", LastName = "Update-Update"};
        _mapperMock.Setup(m => m.Map<UserDto>(It.IsAny<User>())).Returns(expectedDto);
        
        var result = await _userService.UpdateAsync(user.Id, updateUser);
        Assert.NotNull(result);
    }

    [Fact]
    public async Task DeleteAsync_ShouldThrowException_WhenIdIsInvalid()
    {
        var invalidId = 999;
        
        var action = () => _userService.DeleteAsync(invalidId);
        var exception = await Assert.ThrowsAsync<ApiException>(action);
        
        Assert.Equal(404, exception.StatusCode);
    }

    [Fact]
    public async Task DeleteAsync_ValidUser_WhenIsValid()
    {
        var user = new User
        {
            Id = 1,
            FirstName = "Test-Delete-FirstName",
            LastName = "Test-Delete-LastName",
            Email = "Test@gmail.com-Delete-Email",
            Password = "1234-delete"
        };
        _db.Users.Add(user);
        await _db.SaveChangesAsync();
        
        await _userService.DeleteAsync(user.Id);
        
        var result = await _db.Users.FindAsync(user.Id);
        Assert.Null(result);
    }
    
}