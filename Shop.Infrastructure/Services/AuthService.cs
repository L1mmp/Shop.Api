using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Shop.Application.Interfaces;
using Shop.DataAccess.Repositories.Interfaces;
using Shop.Domain.Dtos;
using Shop.Domain.Entities;
using Shop.Domain.Models;
using Shop.Domain.ResponceModels;
using Shop.Infrastructure.Providers;

namespace Shop.Infrastructure.Services
{
	public class AuthService : IAuthService
	{
		private readonly IConfiguration _configuration;
		private readonly IMapper _mapper;
		private readonly IUserRepository _userRepository;
		private readonly ICartRepository _cartRepository;
		private readonly IHttpContextAccessor _httpContext;
		private readonly TokenProvider _tokenProvider;

		public AuthService(IConfiguration configuration,
			IMapper mapper,
			IUserRepository userRepository,
			ICartRepository cartRepository,
			IHttpContextAccessor httpContext, TokenProvider tokenProvider)
		{
			_configuration = configuration;
			_mapper = mapper;
			_userRepository = userRepository;
			_cartRepository = cartRepository;
			_httpContext = httpContext;
			_tokenProvider = tokenProvider;
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

			await _userRepository.Add(user);

			var cartEntity = new Cart() { User = user };
			await _cartRepository.Add(cartEntity);

			return refreshToken;
		}

		public async Task<LoginResponceModel> Login(LoginDto dto)
		{
			var user = (await _userRepository.GetWithIncludeAsync(x => x.Login == dto.Login, u => u.Cart)).FirstOrDefault();

			var responce = new LoginResponceModel
			{
				Message = "Login or password incorrect",
				IsSuccessful = false
			};

			if (user is default(User))
			{
				return responce;
			}

			var verified = VerifyPassword(dto.Password, user);

			if (verified)
			{
				var refresh = _tokenProvider.CreateRefreshToken();
				responce.IsSuccessful = true;
				responce.Message = "Login successful";
				responce.Token = _tokenProvider.CreateToken(user);
				responce.RefreshToken = refresh;


				user.RefreshToken = refresh.Token;
				user.TokenCreated = refresh.Created;
				user.TokenExpires = refresh.Expires;
			}

			await _userRepository.Update(user);

			return responce;
		}

		public async Task<RefreshToken> GetUserRefreshToken()
		{
			var login = _httpContext.HttpContext.User.FindFirst(ClaimTypes.Name).Value;
			var user = (await _userRepository.GetByCondition(x => x.Login == login)).FirstOrDefault();

			return new RefreshToken()
			{
				Token = user.RefreshToken,
				Created = user.TokenCreated,
				Expires = user.TokenExpires
			};
		}

		public async Task<RefreshResponceModel> RefreshToken()
		{
			var login = _httpContext.HttpContext.User.FindFirst(ClaimTypes.Name).Value;
			var user = (await _userRepository.GetWithIncludeAsync(x => x.Login == login, u => u.Cart)).FirstOrDefault();

			var token = new RefreshToken()
			{
				Token = user.RefreshToken,
				Created = user.TokenCreated,
				Expires = user.TokenExpires
			};

			var refreshToken = _httpContext.HttpContext.Request.Cookies["refreshToken"];

			var responce = new RefreshResponceModel()
			{
				Message = "Can't refresh token"
			};

			if (!token.Token.Equals(refreshToken))
			{
				responce.Message = "Invalid Refresh Token";
				return responce;
			}

			if (token.Expires <= DateTime.UtcNow)
			{
				responce.Message = "Token expired";
				return responce;
			}

			var newJwt = _tokenProvider.CreateToken(user);
			var newRefreshToken = _tokenProvider.CreateRefreshToken();


			responce.Message = "Token refreshed successfully";
			responce.Jwt = newJwt;
			responce.RefreshToken = newRefreshToken;
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
	}
}
