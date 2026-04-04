using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UserCreate, User>(); 
        
        CreateMap<UserUpdate, User>();
        
        CreateMap<User, UserDto>();
    }
}