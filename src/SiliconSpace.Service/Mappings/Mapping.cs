using AutoMapper;
using SiliconSpace.Domain.Entities;
using SiliconSpace.Service.DTOs.Booking;
using SiliconSpace.Service.DTOs.Coworking;
using SiliconSpace.Service.DTOs.CoworkingZone;
using SiliconSpace.Service.DTOs.Organization;
using SiliconSpace.Service.DTOs.Registration;
using SiliconSpace.Service.DTOs.User;

namespace SiliconSpace.Service.Mappings
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            //User
            CreateMap<User, UserForCreationDto>().ReverseMap();
            CreateMap<User, UserForResultDto>().ReverseMap();
            CreateMap<User, UserForUpdateDto>().ReverseMap();
            CreateMap<UserForCreationDto, RegistrationForCreationDto>().ReverseMap();

            //Organization
            CreateMap<Organization, OrganizationForCreationDto>().ReverseMap();
            CreateMap<Organization, OrganizationForUpdateDto>().ReverseMap();
            CreateMap<Organization, OrganizationForResultDto>().ReverseMap();

            //Booking
            CreateMap<Booking, BookingForCreationDto>().ReverseMap();
            CreateMap<Booking, BookingForResultDto>().ReverseMap();
            CreateMap<Booking, BookingForUpdateDto>().ReverseMap();

            //Coworking
            CreateMap<Coworking, CoworkingForCreationDto>().ReverseMap();
            CreateMap<Coworking, CoworkingForUpdateDto>().ReverseMap();
            CreateMap<Coworking, CoworkingForResultDto>().ReverseMap();

            //CoworkingZone
            CreateMap<CoworkingZone, CoworkingZoneForCreationDto>().ReverseMap();
            CreateMap<CoworkingZone, CoworkingZoneForUpdateDto>().ReverseMap();
            CreateMap<CoworkingZone, CoworkingZoneForResultDto>().ReverseMap();
        }
    }
}
