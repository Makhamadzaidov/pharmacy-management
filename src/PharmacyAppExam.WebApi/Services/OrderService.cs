using AutoMapper;
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
            await _dbContext.SaveChangesAsync();

            var orderViewModel = _mapper.Map<OrderViewModel>(order);
            orderViewModel.DrugName = (await _drugRepository.GetAsync(drug => drug.Id == orderCreateViewModel.DrugId))!.Name;

            var user = (await _userRepository.GetAsync(user => user.Id == userId))!;
            orderViewModel.UserFullName = user.LastName + " " + user.FirstName;

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

        public Task<IEnumerable<OrderViewModel>> GetAllAsync(Expression<Func<Order, bool>>? expression = null, 
            PaginationParams? @params = null)
        {
            var orders = _orderRepository.GetAll(expression).ToPagedAsEnumerable(@params).Select(order => _mapper.Map<OrderViewModel>(order));
            return Task.FromResult(orders);   
        }

        public async Task<OrderViewModel?> GetAsync(Expression<Func<Order, bool>> expression)
        {
            var order = await _orderRepository.GetAsync(expression);

            if (order is null)
                throw new StatusCodeException(HttpStatusCode.NotFound, "Order not found");

            return _mapper.Map<OrderViewModel>(order);
        }

        public async Task<bool> UpdateAsync(long id, OrderCreateViewModel orderCreateViewModel)
        {
            var checkorder = await _orderRepository.GetAsync(order => order.Id == id);

            if (checkorder is null)
                throw new StatusCodeException(HttpStatusCode.NotFound, "Order not found");

            var order = _mapper.Map<Order>(orderCreateViewModel);
            order.Id = order.Id;
            await _orderRepository.UpdateAsync(order);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
