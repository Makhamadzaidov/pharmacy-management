using AutoMapper;
using PharmacyAppExam.WebApi.Commons.Exceptions;
using PharmacyAppExam.WebApi.Commons.Extensions;
using PharmacyAppExam.WebApi.Commons.Utils;
using PharmacyAppExam.WebApi.DbContexts;
using PharmacyAppExam.WebApi.Interfaces.IRepositories;
using PharmacyAppExam.WebApi.Interfaces.Services;
using PharmacyAppExam.WebApi.Models;
using PharmacyAppExam.WebApi.Repositories;
using PharmacyAppExam.WebApi.Security;
using PharmacyAppExam.WebApi.ViewModels.Users;
using System.Linq.Expressions;
using System.Net;

namespace PharmacyAppExam.WebApi.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public UserService(AppDbContext dbContext, IMapper mapper, IFileService fileService)
        {
            _dbContext = dbContext;
            _userRepository = new UserRepository(_dbContext);
            _mapper = mapper;
            _fileService = fileService;
        }

        public async Task<bool> DeleteAsync(Expression<Func<User, bool>> expression)
        {
            var user = await _userRepository.GetAsync(expression);

            if (user is null)
                throw new StatusCodeException(HttpStatusCode.NotFound, "User not found!");

            await _fileService.DeleteImageAsync(user.ImagePath);
            await _userRepository.DeleteAsync(user);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<UserViewModel>> GetAllAsync(Expression<Func<User, bool>>? expression = null, 
            PaginationParams? @params = null)
        {
            var users = _userRepository.GetAll(expression).ToPagedAsEnumerable(@params);
            var userViews = new List<UserViewModel>();

            foreach (var user in users)
            {
                var item = _mapper.Map<UserViewModel>(user);
                item.ImageUrl = "https://localhost:7066/" + user.ImagePath;
                userViews.Add(item);
            }
            return userViews;
        }

        public async Task<UserViewModel?> GetAsync(Expression<Func<User, bool>> expression)
        {
            var user = await _userRepository.GetAsync(expression);

            if (user is null)
                throw new StatusCodeException(HttpStatusCode.NotFound, "User not found!");

            var userViewModel = _mapper.Map<UserViewModel>(user);
            userViewModel.ImageUrl = "https://localhost:7066/" + user.ImagePath;
            return userViewModel;
        }

        public async Task<UserViewModel> UpdateAsync(long id, UserCreateViewModel userCreateViewModel)
        {
            var entity = await _userRepository.GetAsync(user => user.Id == id);

            if (entity is null) 
                throw new StatusCodeException(HttpStatusCode.NotFound, "User not found!");

            await _fileService.DeleteImageAsync(entity.ImagePath);
            var imagePath = await _fileService.SaveImageAsync(userCreateViewModel.Image);
            var res = PasswordHasher.Hash(userCreateViewModel.Password);
            var hash = res.Hash;
            var salt = res.Salt;

            var userMap = _mapper.Map<User>(userCreateViewModel);

            userMap.Id = entity.Id;
            userMap.Salt = entity.Salt;
            userMap.ImagePath = imagePath;
            userMap.PasswordHash = hash;

            await _userRepository.UpdateAsync(userMap);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<UserViewModel>(userMap);
        }
    }
}
