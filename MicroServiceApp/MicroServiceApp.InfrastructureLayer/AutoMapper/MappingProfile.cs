using AutoMapper;
using MicroServiceApp.InfrastructureLayer.Dto;
using MicroServiceApp.InfrastructureLayer.Models;

namespace MicroServiceApp.InfrastructureLayer.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, PutUserDto>();
            CreateMap<PutUserDto, User>();

            CreateMap<User, PostUserDto>();
            CreateMap<PostUserDto, User>();

            CreateMap<Employee, PutEmployeeDto>();
            CreateMap<PutEmployeeDto, Employee>();

            CreateMap<Employee, PostEmployeeDto>();
            CreateMap<PostEmployeeDto, Employee>();

            CreateMap<New, PutNewDto>();
            CreateMap<PutNewDto, New>();

            CreateMap<New, PostNewDto>();
            CreateMap<PostNewDto, New>();

            CreateMap<Img, ImgDto>();
            CreateMap<ImgDto, Img>();

            CreateMap<CarEquipmentForm, CarEquipmentFormDto>();
            CreateMap<CarEquipmentFormDto, CarEquipmentForm>();

            CreateMap<CarEquipment, CarEquipmentDto>();
            CreateMap<CarEquipmentDto, CarEquipment>();

            CreateMap<CarEquipment, PostCarEquipmentDto>();
            CreateMap<PostCarEquipmentDto, CarEquipment>();

            CreateMap<CarEquipment, PutCarEquipmentDto>();
            CreateMap<PutCarEquipmentDto, CarEquipment>();

            CreateMap<PostCarDto, Car>();
            CreateMap<Car, PostCarDto>();

            CreateMap<PutCarDto, Car>();
            CreateMap<Car, PutCarDto>();

            CreateMap<ValueCarEquipment, ValueCarEquipmentDto>();
            CreateMap<ValueCarEquipmentDto, ValueCarEquipment>();
        }
    }
}
