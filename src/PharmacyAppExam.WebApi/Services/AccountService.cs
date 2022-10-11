using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using PharmacyAppExam.WebApi.Commons.Exceptions;
using PharmacyAppExam.WebApi.DbContexts;
using PharmacyAppExam.WebApi.Interfaces.IRepositories;
using PharmacyAppExam.WebApi.Interfaces.Services;
using PharmacyAppExam.WebApi.Models;
using PharmacyAppExam.WebApi.Repositories;
using PharmacyAppExam.WebApi.Security;
using PharmacyAppExam.WebApi.ViewModels.Users;
using System.Net;

namespace PharmacyAppExam.WebApi.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository _userRepository;
        private readonly AppDbContext _dbContext;
        private readonly IFileService _fileService;
        private readonly IAuthService _authService;
        private readonly IEmailService _emailService;
        private readonly IMemoryCache _cache;
        private readonly IMapper _mapper;

        public AccountService(AppDbContext dbContext, IFileService fileService, IAuthService authService,
            IEmailService emailService, IMemoryCache cache, IMapper mapper)
        {
            _dbContext = dbContext;
            _fileService = fileService;
            _authService = authService;
            _emailService = emailService;
            _cache = cache;
            _mapper = mapper;
            _userRepository = new UserRepository(_dbContext);
        }
        public async Task<string> EmailVerify(EmailAddress emailAddress)
        {
            var user = await _userRepository.GetAsync(user => user.Email == emailAddress.Email);

            if (user is null)
                throw new StatusCodeException(HttpStatusCode.NotFound, "User not found");

            if (_cache.TryGetValue(emailAddress.Email, out var exceptedCode))
            {
                if (exceptedCode.Equals(emailAddress.Code))
                {
                    user.EmailConfirmed = true;
                    await _userRepository.UpdateAsync(user);
                    await _dbContext.SaveChangesAsync();

                    return _authService.GenerateToken(user);
                }
                else throw new StatusCodeException(HttpStatusCode.BadRequest, "Code is not valid");
            }
            else
                throw new Exception("Code is expired");
        }

        public async Task<string> LoginAsync(UserLoginModel userLoginModel)
        {
            var user = await _userRepository.GetAsync(user => user.Email == userLoginModel.Email);

            if (user is null)
                throw new StatusCodeException(HttpStatusCode.BadRequest, "Email is not valid");

            if (!PasswordHasher.Verify(userLoginModel.Password, user.Salt, user.PasswordHash))
                throw new StatusCodeException(HttpStatusCode.BadRequest, "Password is not valid");

            return _authService.GenerateToken(user);
        }

        public async Task<bool> RegistrAsync(UserCreateViewModel userCreateViewModel)
        {
            User user = _mapper.Map<User>(userCreateViewModel);
            var checkUser = await _userRepository.GetAsync(user => user.Email == userCreateViewModel.Email);

            if (checkUser is not null)
                throw new StatusCodeException(HttpStatusCode.Conflict, "This email already exists");

            user.ImagePath = await _fileService.SaveImageAsync(userCreateViewModel.Image);
            var hasherResult = PasswordHasher.Hash(userCreateViewModel.Password);
            user.PasswordHash = hasherResult.Hash;
            user.Salt = hasherResult.Salt;

            await _userRepository.CreateAsync(user);
            await _dbContext.SaveChangesAsync();

            var code = new Random().Next(1000, 9999).ToString();
            _cache.Set(userCreateViewModel.Email, code, TimeSpan.FromMinutes(10));

            await _emailService.SendAsync(userCreateViewModel.Email, code);
            return true;
        }
    }
}
