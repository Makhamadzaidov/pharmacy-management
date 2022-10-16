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
            CreateMap<User, UserCreateViewModel>().ReverseMap();
            CreateMap<User, UserViewModel>().ReverseMap();
            CreateMap<Drug, DrugCreateViewModel>().ReverseMap();
            CreateMap<Drug, DrugViewModel>().ReverseMap();
            CreateMap<Order, OrderCreateViewModel>().ReverseMap();
            CreateMap<Order, OrderViewModel>().ReverseMap();
            CreateMap<IEnumerable<OrderViewModel>, IEnumerable<Order>>();
        }
    }
}
