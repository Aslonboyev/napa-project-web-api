using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NapaProject.AppDbContexts;
using NapaProject.Models;
using NapaProject.Services.AutoMappers;
using NapaProject.Services.Exceptions;
using NapaProject.Services.Interfaces;
using NapaProject.Services.Security;
using NapaProject.Services.ViewModels.Users;
using System.Net;

namespace NapaProject.Services.Services
{
    public class AccountService : IAccountService
    {
        private readonly AppDbContext _context;
        private readonly IAuthManager _authManager;
        private readonly IMapper _mapper;

        public AccountService(AppDbContext appDbContext, IAuthManager authManager, IMapper mapper)
        {
            _context = appDbContext;
            _authManager = authManager;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = config.CreateMapper();
        }

        public async Task<string?> LogInAsync(UserCreateViewModel viewModel)
        {
            var user = await _context.Users.FirstOrDefaultAsync(o => o.Username == viewModel.Username);

            if (user is null)
                throw new StatusCodeException(HttpStatusCode.NotFound, message: "Username is wrong");

            if (PasswordHasher.Verify(viewModel.Password, user.Salt, user.Password))
                return _authManager.GenerateToken(user);

            throw new StatusCodeException(HttpStatusCode.BadRequest, message: "password is wrong");
        }

        public async Task RegisterAsync(UserCreateViewModel viewModel)
        {
            var username = await _context.Users.FirstOrDefaultAsync(o => o.Username == viewModel.Username);

            if (username is not null)
                throw new StatusCodeException(HttpStatusCode.BadRequest, message: "Username already exist");

            var user = _mapper.Map<User>(viewModel);

            var hashResult = PasswordHasher.Hash(viewModel.Password);

            user.Salt = hashResult.Salt;

            user.Password = hashResult.Hash;

            var result = _context.Users.Add(user);

            await _context.SaveChangesAsync();
        }
    }
}
