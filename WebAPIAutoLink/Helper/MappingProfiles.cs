using WebAPIAutoLink.DTO;
using WebAPIAutoLink.Models;
using AutoMapper;

namespace WebAPIAutoLink.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<Order, OrderDto>();
            CreateMap<OrderDto, Order>();
            CreateMap<Car, CarDto>();
            CreateMap<CarDto, Car>();
            CreateMap<CarStatus, CarStatusDto>();
            CreateMap<CarStatusDto, CarStatus>();
            CreateMap<FleetOwner, FleetOwnerDto>();
            CreateMap<FleetOwnerDto, FleetOwner>(); 
            CreateMap<Location, LocationDto>();
            CreateMap<LocationDto, Location>();
        }
    }
}
