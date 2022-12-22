using AutoMapper;
using OnlineWallet.Domain.Entities;
using ServiceContracts.OnlineWallet.Commands;
using ServiceContracts.OnlineWallet.Models;

namespace OnlineWallet.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<OnlineWalletDbModel, OnlineWalletVm>();
        CreateMap<CreateOnlineWalletCommand, OnlineWalletDbModel>();
    }
}