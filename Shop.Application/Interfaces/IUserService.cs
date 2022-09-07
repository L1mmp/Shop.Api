using Shop.Domain.Dtos;
using Shop.Domain.Entities;
using Shop.Domain.ResponceModels;
using System.Linq.Expressions;

namespace Shop.Application.Interfaces
{
	public interface IUserService
	{
		public Task AddUser(UserDto userDto);
		public Task DeleteUser(Guid id);
		public Task<IEnumerable<UserDto>> GetAll();
		public Task<IEnumerable<UserDto>> GetByCondtiton(Expression<Func<User, bool>> expression);
		public Task UpdateById(Guid id, UserDto userDto);
		public Task<UserDto> GetById(Guid id);
		public Task<LoginResponceModel> TryLogin(LoginDto loginDto);
	}
}
