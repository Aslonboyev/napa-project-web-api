using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NapaProject.AppDbContexts;
using NapaProject.Models;
using NapaProject.Services.Exceptions;
using NapaProject.Services.Helpers;
using NapaProject.Services.Interfaces;
using NapaProject.Services.Security;
using NapaProject.Services.ViewModels.Users;
using System.Linq.Expressions;
using System.Net;

namespace NapaProject.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public UserService(AppDbContext appDbContext, IMapper mapper)
        {
            _mapper = mapper;
            _context = appDbContext;
        }

        public async Task DeleteAsync(Expression<Func<User, bool>> expression)
        {
            var result = await _context.Users.FirstOrDefaultAsync(expression);
            if (result is null)
                throw new StatusCodeException(HttpStatusCode.NotFound, message: "User not found");

            _context.Users.Remove(result);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync()
        {
            var user = await _context.Users.FirstOrDefaultAsync(p => p.Id == HttpContextHelper.UserId);

            if (user is null)
                throw new StatusCodeException(HttpStatusCode.NotFound, message: "User not found");

            _context.Users.Remove(user);

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<UserViewModel>> GetAllAsync(Expression<Func<User, bool>>? expression = null)
        {
            if (expression is null)
                expression = p => p.Role == Enums.Role.User;

            return (from user in _context.Users.Where(expression)
                    select _mapper.Map<UserViewModel>(user));
        }

        public async Task<UserViewModel> GetAsync(Expression<Func<User, bool>> expression)
        {
            var user = await _context.Users.FirstOrDefaultAsync(expression);

            if (user == null)
                throw new StatusCodeException(HttpStatusCode.NotFound, message: "User not found");
            

            return _mapper.Map<UserViewModel>(user);
        }

        public async Task<UserViewModel> UpdateAsync(UserPatchViewModel model)
        {
            var user = await _context.Users.FirstOrDefaultAsync(o => o.Id == HttpContextHelper.UserId);

            if (user is null)
                throw new StatusCodeException(HttpStatusCode.NotFound, message: "User not found");

            if (model.Username is not null)
            {
                var username = await _context.Users.FirstOrDefaultAsync(o => o.Username == model.Username);

                if (username is not null && user.Id != username.Id)
                    throw new StatusCodeException(HttpStatusCode.BadRequest, message: "Username have already taken");

                user.Username = model.Username;
            }

            if (model.Password is not null)

                user.Password = PasswordHasher.ChangePassword(model.Password, user.Salt);

            var result = _context.Users.Update(user).Entity;

            await _context.SaveChangesAsync();

            return _mapper.Map<UserViewModel>(result);
        }
    }
}
