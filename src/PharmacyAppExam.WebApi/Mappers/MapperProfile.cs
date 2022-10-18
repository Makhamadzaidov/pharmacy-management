using AutoMapper;
using PharmacyAppExam.WebApi.Models;
using PharmacyAppExam.WebApi.ViewModels.Drugs;
using PharmacyAppExam.WebApi.ViewModels.Orders;
using PharmacyAppExam.WebApi.ViewModels.Users;

namespace PharmacyAppExam.WebApi.Mappers
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<User, UserCreateViewModel>()
                .ForMember(dto => dto.Image,
                    expression => expression.MapFrom(entity => entity.ImagePath)).ReverseMap();
            CreateMap<User, UserViewModel>()
                .ForMember(dto => dto.ImageUrl,
                    expression => expression.MapFrom(entity => "https://pharmacy-app-management.herokuapp.com//" + entity.ImagePath)).ReverseMap();

            CreateMap<Drug, DrugCreateViewModel>()
                .ForMember(dto => dto.ImageUrl,
                    expression => expression.MapFrom(entity => entity.ImagePath)).ReverseMap();
            CreateMap<Drug, DrugViewModel>()
                .ForMember(dto => dto.ImageUrl,
                    expression => expression.MapFrom(entity => entity.ImagePath)).ReverseMap();

            CreateMap<Order, OrderCreateViewModel>().ReverseMap();
            CreateMap<Order, OrderViewModel>().ReverseMap();
            CreateMap<IEnumerable<OrderViewModel>, IEnumerable<Order>>();
            CreateMap<Order, OrderViewModel>()
                .ForMember(dto => dto.UserFullName,
                    expression => expression.MapFrom(entity => entity.User.FirstName + " " + entity.User.LastName))
                .ForMember(dto => dto.DrugName,
                    expression => expression.MapFrom(entity => entity.Drug.Name))
                .ForMember(dto => dto.TotalSum,
                    expression => expression.MapFrom(entity => entity.Drug.Price * entity.Quantity));
        }
    }
}
