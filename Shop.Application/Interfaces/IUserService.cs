using Shop.Domain.Dtos;
using Shop.Domain.Entities;
using Shop.Domain.ResponceModels;
using System.Linq.Expressions;
using Shop.Domain.Models;

namespace Shop.Application.Interfaces
{
	/// <summary>
	/// User service interface.
	/// </summary>
	public interface IUserService
	{
		/// <summary>
		/// Deletes user by id.
		/// </summary>
		/// <param name="id"> User Id. </param>
		/// <returns></returns>
		public Task DeleteUser(Guid id);

		/// <summary>
		/// Gets all users.
		/// </summary>
		/// <returns> All users. </returns>
		public Task<IEnumerable<UserDto>> GetAll();

		/// <summary>
		/// Gets users that fits the condition.
		/// </summary>
		/// <param name="expression"> Condition. </param>
		/// <returns> Users that fits the condition. </returns>
		public Task<IEnumerable<UserDto>> GetByCondition(Expression<Func<User, bool>> expression);

		/// <summary>
		/// Update users by id.
		/// </summary>
		/// <param name="id"> User Id. </param>
		/// <param name="userDto"> UserDto. </param>
		/// <returns></returns>
		public Task UpdateById(Guid id, UserDto userDto);

		/// <summary>
		/// Gets user by id.
		/// </summary>
		/// <param name="id"> User Id. </param>
		/// <returns> User </returns>
		public Task<UserDto> GetById(Guid id);

		/// <summary>
		/// Gets user with includes.
		/// </summary>
		/// <param name="predicate"> Condition for filter user. </param>
		/// <param name="includeProperties"> Include props. </param>
		/// <returns> User with includes. </returns>
		Task<IEnumerable<User>> GetWithIncludeAsync(Func<User, bool> predicate, params Expression<Func<User, object>>[] includeProperties);
	}
}
