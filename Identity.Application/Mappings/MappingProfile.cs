using AutoMapper;
using Identity.Domain.Entities;
using ServiceContracts.Identity.Commands;
using ServiceContracts.Identity.Models;

namespace Identity.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<RegisterCommand, UserDbModel>()
            .ForMember(x => x.Email,
                o =>
                    o.MapFrom(p => p.Email));
        CreateMap<UserDbModel, UserVm>();
        CreateMap<UserDbModel, RegisterVm>();
    }
}