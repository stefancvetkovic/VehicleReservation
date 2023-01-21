using AutoMapper;
using VehicleReservation.Domain.Entities;
using VehicleReservation.Dto;

namespace VehicleReservation.Application.Mappings
{
    public class VehicleProfile : Profile
    {
        public VehicleProfile()
        {
            CreateMap<VehicleDto, Vehicle>().ReverseMap();
        }
    }
}