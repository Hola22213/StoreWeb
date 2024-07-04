using AutoMapper;
using ERP;
using ERP.Api.Models;

public class StoreProfile : Profile
{
    public StoreProfile()
    {
        CreateMap<Store, StoreDto>(); 
    }
}
