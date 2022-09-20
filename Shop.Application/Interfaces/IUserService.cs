using Shop.Domain.Dtos;
using Shop.Domain.Entities;
using Shop.Domain.ResponceModels;
using System.Linq.Expressions;
using Shop.Domain.Models;

namespace Shop.Application.Interfaces
{
	public interface IUserService
	{
		public Task<RefreshToken> Register(UserDto dto);
		public Task AddUser(User user);
		public Task DeleteUser(Guid id);
		public Task<IEnumerable<UserDto>> GetAll();
		public Task<IEnumerable<UserDto>> GetByCondtiton(Expression<Func<User, bool>> expression);
		public Task UpdateById(Guid id, UserDto userDto);
		public Task<UserDto> GetById(Guid id);
		public Task<LoginResponceModel> TryLogin(LoginDto loginDto);
		Task<IEnumerable<User>> GetWithIncludeAsync(Func<User, bool> predicate, params Expression<Func<User, object>>[] includeProperties);
	}
}
