using Ardalis.Result;
using AutoMapper;
using DataTransferLib.Models;

namespace DataTransferLib.Mappings;

public class DefaultResponseObjectProfile : Profile
{
    public DefaultResponseObjectProfile()
    {
        CreateMap(typeof(Result<>), typeof(DefaultResponseObject<>));
    }
}