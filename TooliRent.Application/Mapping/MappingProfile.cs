using AutoMapper;
using TooliRent.Domain.Models;
using TooliRent.Application.DTOs.Booking;
using TooliRent.Application.DTOs.User;
using TooliRent.Application.DTOs.Tool;
using TooliRent.Application.DTOs.Category;

namespace TooliRent.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // User
            CreateMap<User, UserReadDto>();
            CreateMap<UserCreateDto, User>();
            CreateMap<UserUpdateDto, User>();

            // Tool
            CreateMap<Tool, ToolReadDto>();
            CreateMap<ToolCreateDto, Tool>();
            CreateMap<ToolUpdateDto, Tool>();

            // Category
            CreateMap<Category, CategoryReadDto>();
            CreateMap<CategoryCreateDto, Category>();
            CreateMap<CategoryUpdateDto, Category>();

            // Booking
            CreateMap<Booking, BookingReadDto>();
            CreateMap<BookingCreateDto, Booking>();
            CreateMap<BookingUpdateDto, Booking>();
        }
    }
}
