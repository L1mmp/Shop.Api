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

namespace Shop.Infrastructure.Services
{
	public class UserService : IUserService
	{
		private readonly IMapper _mapper;
		private readonly IBaseRepository<User> _userRepository;
		private readonly IConfiguration _configuration;
		private readonly IHttpContextAccessor _httpContext;
		private readonly IBaseRepository<Cart> _cartRepository;

		public UserService(IMapper mapper, IBaseRepository<User> userRepository, IConfiguration configuration, IHttpContextAccessor httpContext, IBaseRepository<Cart> cartRepository)
		{
			_mapper = mapper;
			_userRepository = userRepository;
			_configuration = configuration;
			_httpContext = httpContext;
			_cartRepository = _cartRepository;
		}

		public async Task AddUser(UserDto userDto)
		{
			var user = _mapper.Map<User>(userDto);

			user.Password = HashPassword(user.Password);

			await _userRepository.Add(user);
		}


		public async Task DeleteUser(Guid id)
		{
			await _userRepository.DeleteById(id);
		}

		public async Task<IEnumerable<UserDto>> GetAll()
		{
			var users = await _userRepository.GetAll();
			if (!users.Any())
			{
				throw new NullReferenceException("No any users in system");
			}
			return _mapper.Map<IEnumerable<UserDto>>(users);
		}

		public async Task<IEnumerable<UserDto>> GetByCondtiton(Expression<Func<User, bool>> expression)
		{
			var users = await _userRepository.GetByCondition(expression);
			return _mapper.Map<IEnumerable<UserDto>>(users);
		}

		public async Task UpdateById(Guid id, UserDto userDto)
		{
			var entity = _mapper.Map<User>(userDto);
			await _userRepository.Update(entity);
		}

		public async Task<UserDto> GetById(Guid id)
		{
			var entity = await _userRepository.GetById(id);
			return _mapper.Map<UserDto>(entity);
		}

		public async Task<LoginResponceModel> TryLogin(LoginDto loginDto)
		{
			var user = _userRepository.GetWithInclude(x => x.Login == loginDto.Login.ToString(), x => x.Cart).FirstOrDefault();

			var responce = new LoginResponceModel
			{
				Message = "Login or password incorrect",
				IsSuccessful = false
			};

			if (user is default(User))
			{
				return responce;
			}

			var verified = VerifyPassword(loginDto.Password.ToString(), user);

			if (verified)
			{
				responce.IsSuccessful = true;
				responce.Message = "Login successful";
				responce.Token = CreateToken(user);
			}

			return responce;
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

		private string CreateToken(User user)
		{
			var claims = new List<Claim>()
			{
				new Claim(ClaimTypes.Name, user.Login),
				new Claim("Id", user.Id.ToString()),
				new Claim("cartId",user.Cart.Id.ToString())
			};

			var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
				_configuration.GetSection("AppSettings:Token").Value));

			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

			var token = new JwtSecurityToken(
				claims: claims,
				expires: DateTime.UtcNow.AddDays(1),
				signingCredentials: creds);

			var jwt = new JwtSecurityTokenHandler().WriteToken(token);

			return jwt;
		}
	}
}