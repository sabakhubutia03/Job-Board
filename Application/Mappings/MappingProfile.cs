using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UserCreate, User>(); 
        
        CreateMap<UserUpdate, User>()
            .ForAllMembers(opts => 
                opts.Condition((src, dest, srcMember) => srcMember != null));
        
        CreateMap<User, UserDto>();

        CreateMap<JobCreateDto, Job>();
        CreateMap<JobUpdateDto, Job>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => 
                srcMember != null && (srcMember is int i ? i > 0 : true)));
        CreateMap<Job, JobDto>(); 
        
        CreateMap<CompanyCreateDto, Company>();
        CreateMap<Company, CompanyDto>();
        CreateMap<CompanuUpdateDto, Company>() 
            .ForAllMembers(opts =>
                opts.Condition((src, dest ,srcMember) => srcMember != null));
    }
}