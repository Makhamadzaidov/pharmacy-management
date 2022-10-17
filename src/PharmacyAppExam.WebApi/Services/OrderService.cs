using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PharmacyAppExam.WebApi.Commons.Exceptions;
using PharmacyAppExam.WebApi.Commons.Extensions;
using PharmacyAppExam.WebApi.Commons.Utils;
using PharmacyAppExam.WebApi.DbContexts;
using PharmacyAppExam.WebApi.Enums;
using PharmacyAppExam.WebApi.Interfaces.IRepositories;
using PharmacyAppExam.WebApi.Interfaces.Services;
using PharmacyAppExam.WebApi.Models;
using PharmacyAppExam.WebApi.Repositories;
using PharmacyAppExam.WebApi.ViewModels.Orders;
using System.Linq.Expressions;
using System.Net;

namespace PharmacyAppExam.WebApi.Services
{
    public class OrderService : IOrderService
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _dbContext;
        private readonly IOrderRepository _orderRepository;
        private readonly IDrugRepository _drugRepository;
        private readonly IUserRepository _userRepository;

        public OrderService(AppDbContext dbContext, IMapper mapper)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _orderRepository = new OrderRepository(_dbContext);
            _drugRepository = new DrugRepository(_dbContext);
            _userRepository = new UserRepository(_dbContext);

        }
        public async Task<OrderViewModel> CreateAsync(long userId, OrderCreateViewModel orderCreateViewModel)
        {
            if (orderCreateViewModel.PaymentType == PaymentType.ByCard && string.IsNullOrEmpty(orderCreateViewModel.CardNumber))
                throw new StatusCodeException(HttpStatusCode.BadRequest, "Card number is required");

            var entity = _mapper.Map<Order>(orderCreateViewModel);
            entity.UserId = userId;
            var order = await _orderRepository.CreateAsync(entity);
            var drug = await _drugRepository.GetAsync(drug => drug.Id == orderCreateViewModel.DrugId);
            drug!.Quantity -= orderCreateViewModel.Quantity;

            await _drugRepository.UpdateAsync(drug);
            await _dbContext.SaveChangesAsync();

            var orderViewModel = _mapper.Map<OrderViewModel>(order);
            orderViewModel.DrugName = (await _drugRepository.GetAsync(drug => drug.Id == orderCreateViewModel.DrugId))!.Name;

            var user = (await _userRepository.GetAsync(user => user.Id == userId))!;
            orderViewModel.UserFullName = user.LastName + " " + user.FirstName;
            orderViewModel.TotalSum = order.Quantity * drug.Price;

            return orderViewModel;
        }

        public async Task<bool> DeleteAsync(Expression<Func<Order, bool>> expression)
        {
            var order = await _orderRepository.GetAsync(expression);

            if (order is null)
                throw new StatusCodeException(HttpStatusCode.NotFound, "Order not found");

            await _orderRepository.DeleteAsync(order);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<OrderViewModel>> GetAllAsync(Expression<Func<Order, bool>>? expression = null,
            PaginationParams? @params = null)
        {
            var orders = _orderRepository.GetAll(expression).Include(p => p.User).Include(p => p.Drug).ToPagedAsEnumerable(@params);
            var orderViewModels = new List<OrderViewModel>();

            foreach (var order in orders)
            {
                var item = _mapper.Map<OrderViewModel>(order);
                orderViewModels.Add(item);
            }

            return orderViewModels;
        }

        public async Task<OrderViewModel?> GetAsync(long userId, Expression<Func<Order, bool>> expression)
        {
            var order = await _orderRepository.GetAsync(expression);

            if (order is null)
                throw new StatusCodeException(HttpStatusCode.NotFound, "Order not found");

            var drug = await _drugRepository.GetAsync(drug => drug.Id == order.DrugId);

            if (drug is null)
                throw new StatusCodeException(HttpStatusCode.NotFound, "Drug not found");

            var user = await _userRepository.GetAsync(user => user.Id == userId);

            if (user is null)
                throw new StatusCodeException(HttpStatusCode.NotFound, "User not found");

            var orderViewModel = _mapper.Map<OrderViewModel>(order);
            orderViewModel.DrugName = drug.Name;
            orderViewModel.UserFullName = user.FirstName + " " + user.LastName;
            orderViewModel.TotalSum = order.Quantity * drug.Price;

            return orderViewModel;
        }
    }
}
