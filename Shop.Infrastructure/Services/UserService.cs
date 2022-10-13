using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Shop.Application.Interfaces;
using Shop.DataAccess.Repositories.Interfaces;
using Shop.Domain.Dtos;
using Shop.Domain.Entities;
using Shop.Domain.ResponceModels;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Security.Claims;
using Shop.Domain.Models;
using Shop.Infrastructure.Providers;

namespace Shop.Infrastructure.Services
{
	public class UserService : IUserService
	{
		private readonly IMapper _mapper;
		private readonly IUserRepository _userRepository;
		private readonly IConfiguration _configuration;
		private readonly IHttpContextAccessor _httpContext;
		private readonly ICartRepository _cartRepository;

		public UserService(IMapper mapper, IUserRepository userRepository, IConfiguration configuration, IHttpContextAccessor httpContext, ICartRepository cartRepository)
		{
			_mapper = mapper;
			_userRepository = userRepository;
			_configuration = configuration;
			_httpContext = httpContext;
			_cartRepository = cartRepository;
		}

		public async Task AddUser(User user)
		{
			await _userRepository.AddAsync(user);
		}

		public async Task<RefreshToken> Register(UserDto dto)
		{
			var tokenHelper = new TokenProvider(_configuration);
			var refreshToken = tokenHelper.CreateRefreshToken();
			var user = _mapper.Map<User>(dto);

			user.Password = HashPassword(user.Password);
			user.RefreshToken = refreshToken.Token;
			user.TokenCreated = refreshToken.Created;
			user.TokenExpires = refreshToken.Expires;

			await AddUser(user);

			var cartEntity = new Cart() { User = user };
			await _cartRepository.AddAsync(cartEntity);

			return refreshToken;
		}


		public async Task DeleteUser(Guid id)
		{
			await _userRepository.DeleteByIdAsync(id);
		}

		public async Task<IEnumerable<UserDto>> GetAll()
		{
			var users = await _userRepository.GetAllAsync();
			if (!users.Any())
			{
				throw new NullReferenceException("No any users in system");
			}
			return _mapper.Map<IEnumerable<UserDto>>(users);
		}

		public async Task<IEnumerable<UserDto>> GetByCondition(Expression<Func<User, bool>> expression)
		{
			var users = await _userRepository.GetByConditionAsync(expression);
			return _mapper.Map<IEnumerable<UserDto>>(users);
		}

		public async Task UpdateById(Guid id, UserDto userDto)
		{
			var entity = _mapper.Map<User>(userDto);
			await _userRepository.UpdateAsync(entity);
		}

		public async Task<UserDto> GetById(Guid id)
		{
			var entity = await _userRepository.GetByIdAsync(id);
			return _mapper.Map<UserDto>(entity);
		}

		public async Task<LoginResponceModel> TryLogin(LoginDto loginDto)
		{
			var user = (await _userRepository.GetWithIncludeAsync(x => x.Login == loginDto.Login, x => x.Cart)).FirstOrDefault();

			var tokenProvider = new TokenProvider(_configuration);

			var responce = new LoginResponceModel
			{
				Message = "Login or password incorrect",
				IsSuccessful = false
			};

			if (user is default(User))
			{
				return responce;
			}

			var verified = VerifyPassword(loginDto.Password, user);

			if (verified)
			{
				responce.IsSuccessful = true;
				responce.Message = "Login successful";
				responce.Token = tokenProvider.CreateToken(user);
				responce.RefreshToken = tokenProvider.CreateRefreshToken();
			}

			return responce;
		}

		public async Task<IEnumerable<User>> GetWithIncludeAsync(Func<User, bool> predicate, params Expression<Func<User, object>>[] includeProperties)
		{
			return await _userRepository.GetWithIncludeAsync(predicate, includeProperties);
		}

		private static bool VerifyPassword(string password, User? user)
		{
			return BCrypt.Net.BCrypt.Verify(password, user.Password);
		}

		private static string HashPassword(string password)
		{
			var pass = BCrypt.Net.BCrypt.HashPassword(password);
			return pass;
		}
	}
}